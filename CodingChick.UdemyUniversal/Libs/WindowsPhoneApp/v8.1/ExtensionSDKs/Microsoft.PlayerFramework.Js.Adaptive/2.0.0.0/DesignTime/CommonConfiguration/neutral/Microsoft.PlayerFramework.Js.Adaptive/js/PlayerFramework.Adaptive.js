(function (PlayerFramework, undefined) {
    "use strict";

    // AdaptivePlugin Errors
    var invalidConstruction = "Invalid construction: AdaptivePlugin constructor must be called using the \"new\" operator.";

    var downloaderPluginHttpScheme = "ms-sstr:";
    var downloaderPluginHttpsScheme = "ms-sstrs:";

    // AdaptivePlugin Class
    var AdaptivePlugin = WinJS.Class.derive(PlayerFramework.PluginBase, function (options) {
        if (!(this instanceof PlayerFramework.Plugins.AdaptivePlugin)) {
            throw invalidConstruction;
        }

        this._manager = new Microsoft.Media.AdaptiveStreaming.Helper.AdaptiveStreamingManager();
        this._byteStreamHandlers = [{ fileExtension: ".ism", mimeType: "text/xml" }, { fileExtension: ".ism", mimeType: "application/vnd.ms-sstr+xml" }];
        this._schemeHandlers = [{ scheme: downloaderPluginHttpScheme }, { scheme: downloaderPluginHttpsScheme }];
        this._autoRestrictSize = true;
        this._autoSchemeDownloaderPlugin = true;
        this._instreamCaptionsEnabled = false;

        PlayerFramework.PluginBase.call(this, options);
    }, {
        // Public Properties
        manager: {
            get: function () {
                return this._manager;
            }
        },

        byteStreamHandlers: {
            get: function () {
                return this._byteStreamHandlers;
            },
            set: function (value) {
                var oldValue = this._byteStreamHandlers;
                if (oldValue !== value) {
                    this._byteStreamHandlers = value;
                    this._observablePlugin.notify("byteStreamHandlers", value, oldValue);
                }
            }
        },

        schemeHandlers: {
            get: function () {
                return this._schemeHandlers;
            },
            set: function (value) {
                var oldValue = this._schemeHandlers;
                if (oldValue !== value) {
                    this._schemeHandlers = value;
                    this._observablePlugin.notify("schemeHandlers", value, oldValue);
                }
            }
        },

        instreamCaptionsEnabled: {
            get: function () {
                return this._instreamCaptionsEnabled;
            },
            set: function (value) {
                var oldValue = this._instreamCaptionsEnabled;
                if (oldValue !== value) {
                    this._instreamCaptionsEnabled = value;
                    this._observablePlugin.notify("instreamCaptionsEnabled", value, oldValue);
                }
            }
        },

        autoRestrictSize: {
            get: function () {
                return this._autoRestrictSize;
            },
            set: function (value) {
                var oldValue = this._autoRestrictSize;
                if (oldValue !== value) {
                    this._autoRestrictSize = value;
                    this._observablePlugin.notify("autoRestrictSize", value, oldValue);
                    this._updateMaxSize();
                }
            }
        },

        maxBitrate: {
            get: function () {
                return this.manager.maxBitrate;
            },
            set: function (value) {
                var oldValue = this.manager.maxBitrate;
                if (oldValue !== value) {
                    this.manager.maxBitrate = value;
                    this._observablePlugin.notify("maxBitrate", value, oldValue);
                }
            }
        },

        minBitrate: {
            get: function () {
                return this.manager.minBitrate;
            },
            set: function (value) {
                var oldValue = this.manager.minBitrate;
                if (oldValue !== value) {
                    this.manager.minBitrate = value;
                    this._observablePlugin.notify("minBitrate", value, oldValue);
                }
            }
        },

        startupBitrate: {
            get: function () {
                return this.manager.startupBitrate;
            },
            set: function (value) {
                var oldValue = this.manager.startupBitrate;
                if (oldValue !== value) {
                    this.manager.startupBitrate = value;
                    this._observablePlugin.notify("startupBitrate", value, oldValue);
                }
            }
        },

        autoSchemeDownloaderPlugin: {
            get: function () {
                return this._autoSchemeDownloaderPlugin;
            },
            set: function (value) {
                var oldValue = this._autoSchemeDownloaderPlugin;
                if (oldValue !== value) {
                    this._autoSchemeDownloaderPlugin = value;
                    this._observablePlugin.notify("autoSchemeDownloaderPlugin", value, oldValue);
                }
            }
        },

        downloaderPlugin: {
            get: function () {
                return this.manager.downloaderPlugin;
            },
            set: function (value) {
                var oldValue = this.manager.downloaderPlugin;
                if (oldValue !== value) {
                    this.manager.downloaderPlugin = value;
                    this._observablePlugin.notify("downloaderPlugin", value, oldValue);
                }
            }
        },

        // Private Methods
        _onLoad: function () {
            if (!this.manager) {
                this._manager = new Microsoft.Media.AdaptiveStreaming.Helper.AdaptiveStreamingManager();
            }
        },

        _onUnload: function () {
            this.manager.uninitialize();
            this._manager = null;
        },

        _onActivate: function () {
            this._bindEvent("timeupdate", this.mediaPlayer, this._onMediaPlayerTimeUpdate);
            this._bindEvent("loading", this.mediaPlayer, this._onMediaPlayerLoading);
            this._bindEvent("currentaudiotrackchanging", this.mediaPlayer, this._onMediaPlayerCurrentAudioTrackChanging);
            this._bindEvent("currentcaptiontrackchanging", this.mediaPlayer, this._onMediaPlayerCurrentCaptionTrackChanging);
            this._bindEvent("canplaythrough", this.mediaPlayer, this._onMediaPlayerCanplaythrough);
            this._bindEvent("manifestready", this.manager, this._onManagerManifestReady);
            this._bindEvent("statechanged", this.manager, this._onManagerStateChanged);
            this._bindEvent("timeschanged", this.manager, this._onManagerTimesChanged);
            this._bindEvent("endoflive", this.manager, this._onManagerEndOfLive);
            this._bindEvent("outsidewindowedge", this.manager, this._onManagerOutsideWindowEdge);
            this._bindEvent("datareceived", this.manager, this._onManagerDataReceived);
            this._bindEvent("closed", this.manager, this._onManagerClosed);
            if (PlayerFramework.Utilities.isWinJS1) {
                this._bindEvent("resize", this.mediaPlayer.element, this._onMediaPlayerResize);
            }
            else { // IE11 no longer supports resize event for arbitrary elements. The best we can do is listen to the window resize event.
                this._bindEvent("resize", window, this._onMediaPlayerResize);
            }

            this._updateMaxSize();

            return true;
        },

        _onDeactivate: function () {
            this._unbindEvent("timeupdate", this.mediaPlayer, this._onMediaPlayerTimeUpdate);
            this._unbindEvent("loading", this.mediaPlayer, this._onMediaPlayerLoading);
            this._unbindEvent("currentaudiotrackchanging", this.mediaPlayer, this._onMediaPlayerCurrentAudioTrackChanging);
            this._unbindEvent("currentcaptiontrackchanging", this.mediaPlayer, this._onMediaPlayerCurrentCaptionTrackChanging);
            this._unbindEvent("canplaythrough", this.mediaPlayer, this._onMediaPlayerCanplaythrough);
            this._unbindEvent("manifestready", this.manager, this._onManagerManifestReady);
            this._unbindEvent("statechanged", this.manager, this._onManagerStateChanged);
            this._unbindEvent("timeschanged", this.manager, this._onManagerTimesChanged);
            this._unbindEvent("endoflive", this.manager, this._onManagerEndOfLive);
            this._unbindEvent("outsidewindowedge", this.manager, this._onManagerOutsideWindowEdge);
            this._unbindEvent("datareceived", this.manager, this._onManagerDataReceived);
            this._unbindEvent("closed", this.manager, this._onManagerClosed);
            if (PlayerFramework.Utilities.isWinJS1) {
                this._unbindEvent("resize", this.mediaPlayer.element, this._onMediaPlayerResize);
            }
            else { // IE11 no longer supports resize event for arbitrary elements. The best we can do is listen to the window resize event.
                this._unbindEvent("resize", window, this._onMediaPlayerResize);
            }
        },

        _onMediaPlayerCanplaythrough: function (e) {
            this.manager.mediaReady();
        },

        _onMediaPlayerLoading: function (e) {
            try {
                if (this.downloaderPlugin && this.autoSchemeDownloaderPlugin) {
                    e.detail.src = e.detail.src.replace("http:", downloaderPluginHttpScheme).replace("https:", downloaderPluginHttpsScheme);
                }

                if (!this.manager.isInitialized) { // only do this the first time
                    this.manager.initialize(this.mediaPlayer.mediaExtensionManager);

                    // register byte stream handlers
                    if (this.byteStreamHandlers) {
                        for (var i = 0; i < this.byteStreamHandlers.length; i++) {
                            var byteStreamHandler = this.byteStreamHandlers[i];
                            this.manager.registerByteStreamHandler(byteStreamHandler.fileExtension, byteStreamHandler.mimeType);
                        }
                    }

                    // register scheme handlers
                    if (this.schemeHandlers) {
                        for (var i = 0; i < this.schemeHandlers.length; i++) {
                            var schemeHandler = this.schemeHandlers[i];
                            this.manager.registerSchemeHandler(schemeHandler.scheme);
                        }
                    }
                }

                this.manager.sourceUri = new Windows.Foundation.Uri(e.detail.src);
            } catch (error) {
                /* swallow */
            }
        },

        _onMediaPlayerCurrentAudioTrackChanging: function (e) {
            if (this.manager.isOpen) {
                if (!e.detail.track) {
                    this.manager.selectedAudioStream = null;
                } else if (e.detail.track.stream !== this.manager.selectedAudioStream) {
                    this.manager.selectedAudioStream = e.detail.track.stream;
                }
            }
        },

        _onMediaPlayerCurrentCaptionTrackChanging: function (e) {
            if (this.instreamCaptionsEnabled) {
                if (this.manager.isOpen) {
                    if (!e.detail.track) {
                        this.manager.selectedCaptionStream = null;
                    } else if (e.detail.track.stream !== this.manager.selectedCaptionStream) {
                        this.manager.selectedCaptionStream = e.detail.track.stream;
                    }
                }
            }
        },

        _onManagerDataReceived: function (e) {
            if (this.instreamCaptionsEnabled) {
                var captionStream = this.manager.selectedCaptionStream;
                if (captionStream && captionStream.manifestStream.name == e.stream.name) // make sure we're receiving data for the same caption that is selected
                {
                    var track = this.mediaPlayer.currentCaptionTrack;
                    // push data into the caption so it can notify the renderer that new data exists.
                    track.augmentPayload(e.data, PlayerFramework.Utilities.convertTicksToMilliseconds(e.startTime), PlayerFramework.Utilities.convertTicksToMilliseconds(e.endTime));
                }
            }
        },

        _onMediaPlayerTimeUpdate: function (e) {
            this.manager.refreshState(this.mediaPlayer.currentTime * 1000);
        },

        _onMediaPlayerResize: function (e) {
            this._updateMaxSize();
        },

        _onManagerManifestReady: function (e) {
            this.mediaPlayer.isLive = this.manager.isLive;

            var audioTracks = [];
            if (this.manager.availableAudioStreams) {
                for (var i = 0; i < this.manager.availableAudioStreams.length; i++) {
                    var audioStream = this.manager.availableAudioStreams[i];
                    var audioTrack = { stream: audioStream, label: audioStream.name, language: audioStream.language, enabled: audioStream === this.manager.selectedAudioStream };
                    audioTracks.push(audioTrack);
                }
            }
            this.mediaPlayer.audioTracks = audioTracks;

            if (this.instreamCaptionsEnabled) {
                var captionTracks = [];
                if (this.manager.availableCaptionStreams) {
                    for (var i = 0; i < this.manager.availableCaptionStreams.length; i++) {
                        var captionStream = this.manager.availableCaptionStreams[i];
                        var captionTrack = new PlayerFramework.DynamicTextTrack(captionStream);
                        captionTracks.push(captionTrack);
                    }
                }
                this.mediaPlayer.captionTracks = captionTracks;
            }
        },

        _onManagerStateChanged: function (e) {
            this.mediaPlayer.signalStrength = this.manager.currentBitrate / this.manager.highestBitrate;
            this.mediaPlayer.mediaQuality = this.manager.currentHeight >= 720 ? PlayerFramework.MediaQuality.highDefinition : PlayerFramework.MediaQuality.standardDefinition;
        },

        _onManagerTimesChanged: function (e) {
            this.mediaPlayer.startTime = PlayerFramework.Utilities.convertTicksToSeconds(this.manager.startTime);
            this.mediaPlayer.endTime = PlayerFramework.Utilities.convertTicksToSeconds(this.manager.endTime);
            this.mediaPlayer.liveTime = PlayerFramework.Utilities.convertTicksToSeconds(this.manager.livePosition);
        },

        _onManagerEndOfLive: function (e) {
            this.mediaPlayer.liveTime = null;
            this.mediaPlayer.isLive = false;
        },

        _onManagerOutsideWindowEdge: function (e) {
            if (this.mediaPlayer.advertisingState !== PlayerFramework.AdvertisingState.loading && this.mediaPlayer.advertisingState !== PlayerFramework.AdvertisingState.linear) {
                this.mediaPlayer.playResume();
            }
        },

        _onManagerClosed: function (e) {
            this.mediaPlayer.audioTracks = null;
            this.mediaPlayer.liveTime = null;
            this.mediaPlayer.isLive = false;
            this.mediaPlayer.signalStrength = 0;
            this.mediaPlayer.mediaQuality = PlayerFramework.MediaQuality.standardDefinition;
        },

        _updateMaxSize: function () {
            this.manager.maxSize = this.autoRestrictSize ? PlayerFramework.Utilities.measureElement(this.mediaPlayer.element) : null;
        }
    });

    // AdaptivePlugin Mixins
    WinJS.Class.mix(PlayerFramework.MediaPlayer, {
        adaptivePlugin: {
            value: null,
            writable: true,
            configurable: true
        }
    });

    // AdaptivePlugin Exports
    WinJS.Namespace.define("PlayerFramework.Plugins", {
        AdaptivePlugin: AdaptivePlugin
    });

})(PlayerFramework);

