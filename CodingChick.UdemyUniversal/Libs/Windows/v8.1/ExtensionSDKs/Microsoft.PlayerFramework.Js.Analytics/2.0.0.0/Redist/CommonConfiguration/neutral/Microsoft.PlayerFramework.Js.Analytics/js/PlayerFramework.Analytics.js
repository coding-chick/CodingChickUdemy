(function (PlayerFramework, undefined) {
    "use strict";

    // ErrorAnalyticsPlugin Errors
    var invalidConstruction = "Invalid construction: ErrorLogger constructor must be called using the \"new\" operator.";

    // ErrorAnalyticsPlugin Class
    var ErrorLogger = WinJS.Class.define(function () {
        if (!(this instanceof PlayerFramework.Analytics.ErrorLogger)) {
            throw invalidConstruction;
        }

        this._preventUnhandledErrors = false;
        this._maxErrorLength = null;

        // wire up application error event
        this._bindEvent("error", WinJS.Application, this._onApplicationError);
    }, {

        preventUnhandledErrors: {
            get: function () {
                return this._preventUnhandledErrors;
            },
            set: function (value) {
                this._environmentMonitor = value;
            }
        },

        maxErrorLength: {
            get: function () {
                return this._maxErrorLength;
            },
            set: function (value) {
                this._maxErrorLength = value;
            }
        },

        // private methods

        _onApplicationError: function (e) {
            var errorMessage = "";
            if (e.detail.error.stack) errorMessage = e.detail.error.stack;
            else if (e.detail.error) errorMessage = e.detail.error;
            this.logError(errorMessage, "UnhandledException");
            return this.preventUnhandledErrors;
        },

        // public methods
        
        logError: function (error, applicationArea) {
            var errorLog = new Microsoft.Media.Analytics.ErrorLog(error, applicationArea);
            errorLog.maxErrorLength = this.maxErrorLength;
            Microsoft.Media.Analytics.LoggingService.current.log(errorLog);
        },

        dispose: function () {
            this._unbindEvent("error", WinJS.Application, this._onApplicationError);
        },

    });

    // ErrorLogger Mixins
    WinJS.Class.mix(ErrorLogger, PlayerFramework.Utilities.eventBindingMixin);

    // ErrorLogger Exports
    WinJS.Namespace.define("PlayerFramework.Analytics", {
        ErrorLogger: ErrorLogger
    });

})(PlayerFramework);

(function (PlayerFramework, undefined) {
    "use strict";

    // MediaPlayerAdapter Errors
    var invalidConstruction = "Invalid construction: MediaPlayerAdapter constructor must be called using the \"new\" operator.",
        invalidMediaPlayer = "Invalid argument: MediaPlayerAdapter expects a MediaPlayer as the first argument.";

    var analyticsTrackingEventArea = "analytics";

    // MediaPlayerAdapter Class
    var MediaPlayerAdapter = WinJS.Class.define(function (mediaPlayer) {
        if (!(this instanceof PlayerFramework.Analytics.MediaPlayerAdapter)) {
            throw invalidConstruction;
        }

        if (!(mediaPlayer instanceof PlayerFramework.MediaPlayer)) {
            throw invalidMediaPlayer;
        }

        this._mediaPlayer = mediaPlayer;
        this._playbackRate = this._mediaPlayer.defaultPlaybackRate;
        this._isScrubbing = false;
        
        this._nativeInstance = new Microsoft.PlayerFramework.Js.Analytics.MediaPlayerAdapterBridge();
        this._nativeInstance.setPlaybackRate(this._mediaPlayer.playbackRate);
        this._nativeInstance.setIsFullScreen(this._mediaPlayer.isFullScreen);
        this._nativeInstance.setIsLive(this._mediaPlayer.isLive);
        this._nativeInstance.setIsBuffering(false);
        if (this._mediaPlayer.currentAudioTrack) {
            this._nativeInstance.setAudioTrackId(this._mediaPlayer.currentAudioTrack.language);
        }
        if (this._mediaPlayer.currentCaptionTrack) {
            this._nativeInstance.setCaptionTrackId(this._mediaPlayer.currentCaptionTrack.label);
        }

        this._bindEvent("positionrequested", this._nativeInstance, this._onPositionRequested);
        this._bindEvent("durationrequested", this._nativeInstance, this._onDurationRequested);

        this._bindEvent("fullscreenchange", this._mediaPlayer, this._onMediaPlayerFullScreenChange);
        this._bindEvent("waiting", this._mediaPlayer, this._onMediaPlayerWaiting);
        this._bindEvent("playing", this._mediaPlayer, this._onMediaPlayerPlaying);
        this._bindEvent("islivechange", this._mediaPlayer, this._onMediaPlayerIsLiveChange);
        this._bindEvent("ratechange", this._mediaPlayer, this._onMediaPlayerRateChange);
        this._bindEvent("currentaudiotrackchange", this._mediaPlayer, this._onMediaPlayerAudioTrackChange);
        this._bindEvent("currentcaptiontrackchange", this._mediaPlayer, this._onMediaPlayerCaptionTrackChange);
        this._bindEvent("advertisingstatechange", this._mediaPlayer, this._onMediaPlayerAdvertisingStateChange);
        this._bindEvent("error", this._mediaPlayer, this._onMediaPlayerError);
        this._bindEvent("canplaythrough", this._mediaPlayer, this._onMediaPlayerCanPlayThrough);
        this._bindEvent("started", this._mediaPlayer, this._onMediaPlayerStarted);
        this._bindEvent("emptied", this._mediaPlayer, this._onMediaPlayerEmptied);
        this._bindEvent("ending", this._mediaPlayer, this._onMediaPlayerEnding);
        this._bindEvent("play", this._mediaPlayer, this._onMediaPlayerPlay);
        this._bindEvent("pause", this._mediaPlayer, this._onMediaPlayerPause);
        this._bindEvent("seek", this._mediaPlayer, this._onMediaPlayerSeek);
        this._bindEvent("scrub", this._mediaPlayer, this._onMediaPlayerScrub);
        this._bindEvent("scrubbed", this._mediaPlayer, this._onMediaPlayerScrubbed);
        this._bindEvent("eventtracked", this._mediaPlayer.playTimeTrackingPlugin, this._onMediaPlayerPlayTimeEventTracked);
        this._bindEvent("eventtracked", this._mediaPlayer.positionTrackingPlugin, this._onMediaPlayerPositionEventTracked);
    }, {
        // Public Properties
        nativeInstance: {
            get: function () {
                return this._nativeInstance;
            }
        },

        // Public Methods
        dispose: function () {
            this._unbindEvents();
            this._mediaPlayer = null;
            this._nativeInstance = null;
        },

        // Private Methods
        _onPositionRequested: function (e) {
            e.result = this._mediaPlayer.currentTime * 1000;
        },
        
        _onDurationRequested: function (e) {
            e.result = this._mediaPlayer.duration * 1000;
        },

        _onMediaPlayerFullScreenChange: function (e) {
            this._nativeInstance.setIsFullScreen(this._mediaPlayer.isFullScreen);
        },

        _onMediaPlayerWaiting: function (e) {
            this._nativeInstance.setIsBuffering(true);
        },

        _onMediaPlayerPlaying: function (e) {
            this._nativeInstance.setIsBuffering(false);
        },

        _onMediaPlayerIsLiveChange: function (e) {
            this._nativeInstance.setIsLive(this._mediaPlayer.isLive);
        },

        _onMediaPlayerRateChange: function (e) {
            if (!this._isScrubbing) {
                this._nativeInstance.setPlaybackRate(this._mediaPlayer.playbackRate);
            }
        },

        _onMediaPlayerAudioTrackChange: function (e) {
            if (this._mediaPlayer.currentAudioTrack) {
                this._nativeInstance.setAudioTrackId(this._mediaPlayer.currentAudioTrack.language);
            }
            else {
                this._nativeInstance.setAudioTrackId(null);
            }
        },

        _onMediaPlayerCaptionTrackChange: function (e) {
            if (this._mediaPlayer.currentCaptionTrack) {
                this._nativeInstance.setCaptionTrackId(this._mediaPlayer.currentCaptionTrack.label);
            }
            else {
                this._nativeInstance.setCaptionTrackId(null);
            }
        },

        _onMediaPlayerAdvertisingStateChange: function (e) {
            switch (this._mediaPlayer.advertisingState) {
                case PlayerFramework.AdvertisingState.linear:
                    this._nativeInstance.onClipStarted("");
                    break;
                default:
                    this._nativeInstance.onClipEnded("");
                    break;
            }
        },

        _onMediaPlayerError: function (e) {
            var error = e.detail.error;
            var message = PlayerFramework.Utilities.getMediaErrorMessage(error);
            this._nativeInstance.onStreamFailed(message);
        },

        _onMediaPlayerCanPlayThrough: function (e) {
            this._nativeInstance.setSource(new Windows.Foundation.Uri(this._mediaPlayer.src));
            this._nativeInstance.onStreamLoaded();
        },

        _onMediaPlayerStarted: function (e) {
            this._nativeInstance.onStreamStarted();
            this._nativeInstance.onPlaying();
        },

        _onMediaPlayerEmptied: function (e) {
            this._nativeInstance.onStreamClosed();
        },

        _onMediaPlayerEnding: function (e) {
            this._nativeInstance.onStreamEnded();
        },
        
        _onMediaPlayerPlay: function (e) {
            this._nativeInstance.onPlaying();
        },

        _onMediaPlayerPause: function (e) {
            this._nativeInstance.onPaused();
        },

        _onMediaPlayerSeek: function (e) {
            this._nativeInstance.onSeeked(e.detail.previousTime * 1000, e.detail.time * 1000);
        },

        _onMediaPlayerScrub: function (e) {
            this._isScrubbing = true;
            this._nativeInstance.onScrubStarted();
        },

        _onMediaPlayerScrubbed: function (e) {
            this._nativeInstance.onScrubCompleted(e.detail.time * 1000);
            this._isScrubbing = false;
        },

        _onMediaPlayerPlayTimeEventTracked: function (e) {
            var trackingEvent = e.detail.trackingEvent;
            if (trackingEvent.area === PlayerFramework.Plugins.AnalyticsPlugin.trackingEventArea) {
                if (!isNaN(trackingEvent.playTimePercentage))
                    this._nativeInstance.onPlayTimePercentageReached(trackingEvent.playTimePercentage);
                else
                    this._nativeInstance.onPlayTimeReached(trackingEvent.playTime * 1000);
            }
        },

        _onMediaPlayerPositionEventTracked: function (e) {
            var trackingEvent = e.detail.trackingEvent;
            if (trackingEvent.area === PlayerFramework.Plugins.AnalyticsPlugin.trackingEventArea) {
                if (!isNaN(trackingEvent.positionPercentage))
                    this._nativeInstance.onPositionPercentageReached(trackingEvent.positionPercentage);
                else
                    this._nativeInstance.onPositionReached(trackingEvent.position * 1000);
            }
        }
    });

    // MediaPlayerAdapter Mixins
    WinJS.Class.mix(MediaPlayerAdapter, PlayerFramework.Utilities.eventBindingMixin);

    // MediaPlayerAdapter Exports
    WinJS.Namespace.define("PlayerFramework.Analytics", {
        MediaPlayerAdapter: MediaPlayerAdapter
    });

})(PlayerFramework);

(function (PlayerFramework, undefined) {
    "use strict";

    // AdaptiveAnalyticsPlugin Errors
    var invalidConstruction = "Invalid construction: AdaptiveAnalyticsPlugin constructor must be called using the \"new\" operator.";

    // AdaptiveAnalyticsPlugin Class
    var AdaptiveAnalyticsPlugin = WinJS.Class.derive(PlayerFramework.PluginBase, function (options) {
        if (!(this instanceof PlayerFramework.Plugins.AdaptiveAnalyticsPlugin)) {
            throw invalidConstruction;
        }

        this._adaptiveMonitorFactory = null;

        PlayerFramework.PluginBase.call(this, options);
    }, {
        
        _setOptions: function (options) {
            PlayerFramework.Utilities.setOptions(this, options, {
                isEnabled: true
            });
        },
        
        _onActivate: function () {
            if (this.mediaPlayer.analyticsPlugin) {
                if (this.mediaPlayer.adaptivePlugin) {
                    this._adaptiveMonitorFactory = new Microsoft.Media.AdaptiveStreaming.Analytics.AdaptiveMonitorFactory(this.mediaPlayer.adaptivePlugin.manager);
                    this.mediaPlayer.analyticsPlugin.adaptiveMonitor = this._adaptiveMonitorFactory.adaptiveMonitor;
                    return true;
                }
            }
            return false;
        },

        _onDeactivate: function () {
            this.mediaPlayer.analyticsPlugin.adaptiveMonitor = null;
            this._adaptiveMonitorFactory.close();
            this._adaptiveMonitorFactory = null;
        }
    });

    // AdaptiveAnalyticsPlugin Mixins
    WinJS.Class.mix(PlayerFramework.MediaPlayer, {
        AdaptiveAnalyticsPlugin: {
            value: null,
            writable: true,
            configurable: true
        }
    });

    // AdaptiveAnalyticsPlugin Exports
    WinJS.Namespace.define("PlayerFramework.Plugins", {
        AdaptiveAnalyticsPlugin: AdaptiveAnalyticsPlugin
    });

})(PlayerFramework);

(function (PlayerFramework, undefined) {
    "use strict";

    // AnalyticsPlugin Errors
    var invalidConstruction = "Invalid construction: AnalyticsPlugin constructor must be called using the \"new\" operator.";

    // AnalyticsPlugin Class
    var AnalyticsPlugin = WinJS.Class.derive(PlayerFramework.PluginBase, function (options) {
        if (!(this instanceof PlayerFramework.Plugins.AnalyticsPlugin)) {
            throw invalidConstruction;
        }

        this._collector = null;
        this._playerMonitor = null;
        this._environmentMonitor = null;
        this._edgeServerMonitor = null;
        this._adaptiveMonitor = null;
        this._analyticsConfig = null;
        this._sessionData = [];
        this._mediaData = [];
        this._mediaPlayerAdapter = null;

        PlayerFramework.PluginBase.call(this, options);
    }, {
        // Public Properties
        environmentMonitor: {
            get: function () {
                return this._environmentMonitor;
            },
            set: function (value) {
                this._environmentMonitor = value;
            }
        },

        edgeServerMonitor: {
            get: function () {
                return this._edgeServerMonitor;
            },
            set: function (value) {
                this._edgeServerMonitor = value;
            }
        },

        adaptiveMonitor: {
            get: function () {
                return this._adaptiveMonitor;
            },
            set: function (value) {
                this._adaptiveMonitor = value;
            }
        },

        analyticsConfig: {
            get: function () {
                return this._analyticsConfig;
            },
            set: function (value) {
                this._analyticsConfig = value;
            }
        },
        
        analyticsCollector: {
            get: function () {
                return this._collector;
            }
        },

        sessionData: {
            get: function () {
                return this._sessionData;
            },
            set: function (value) {
                this._sessionData = value;
            }
        },

        mediaData: {
            get: function () {
                return this._mediaData;
            },
            set: function (value) {
                this._mediaData = value;
            }
        },

        // Private Methods
        _setOptions: function (options) {
            PlayerFramework.Utilities.setOptions(this, options, {
                isEnabled: true,
                analyticsConfig: new Microsoft.Media.Analytics.AnalyticsConfig()
            });
        },

        _onActivate: function () {
            // by default, we always add the AnalyticsCollector as a logging source
            this._collector = new Microsoft.Media.Analytics.AnalyticsCollector();
            // add session specific data
            this._addAdditionalData(this.sessionData);
            Microsoft.Media.Analytics.LoggingService.current.loggingSources.append(this._collector);

            // initialize the AnalyticsCollector. The analytics collector relies on other objects to pass it info.
            this._mediaPlayerAdapter = new PlayerFramework.Analytics.MediaPlayerAdapter(this.mediaPlayer);
            this._playerMonitor = this._mediaPlayerAdapter.nativeInstance;

            this._bindEvent("loadstart", this.mediaPlayer, this._onMediaPlayerLoadStart);
            this._bindEvent("emptied", this.mediaPlayer, this._onMediaPlayerEmptied);

            return true;
        },

        _onDeactivate: function () {
            if (this._collector.isAttached) {
                this._collector.detach();
            }
            this._unbindEvent("loadstart", this.mediaPlayer, this._onMediaPlayerLoadStart);
            this._unbindEvent("emptied", this.mediaPlayer, this._onMediaPlayerEmptied);
            // remove session specific data
            this._removeAdditionalData(this.sessionData);
            this._collector = null;
            this._mediaPlayerAdapter.dispose();
            this._mediaPlayerAdapter = null;
            this._playerMonitor = null;
        },

        _addAdditionalData: function (data) {
            if (data) {
                for (var i = 0; i < data.length; i++) {
                    var item = data[i];
                    this._collector.addtionalData.insert(item.key, item.value);
                }
            }
        },

        _removeAdditionalData: function (data) {
            if (data) {
                for (var i = 0; i < data.length; i++) {
                    var item = data[i];
                    this._collector.addtionalData.remove(item.key);
                }
            }
        },
        
        _onMediaPlayerLoadStart: function () {
            this._collector.configuration = this.analyticsConfig;

            // attach the AnalyticsCollector
            if (!this._collector.isAttached) {
                this._collector.attach(this._playerMonitor, this.adaptiveMonitor, this.environmentMonitor, this.edgeServerMonitor);
            }
            // remove media specific data
            this._addAdditionalData(this.mediaData);
        },
        
        _onMediaPlayerEmptied: function () {
            // remove media specific data
            this._removeAdditionalData(this.mediaData);
            // detach the AnalyticsCollector
            if (this._collector.isAttached) {
                this._collector.detach();
            }

            this._collector.configuration = null;
        },

        // Public Methods
        log: function (log) {
            this._collector.sendLog(log);
        }
    });

    AnalyticsPlugin.trackingEventArea = "analytics";

    // AnalyticsPlugin Mixins
    WinJS.Class.mix(PlayerFramework.MediaPlayer, {
        AnalyticsPlugin: {
            value: null,
            writable: true,
            configurable: true
        }
    });

    // AnalyticsPlugin Exports
    WinJS.Namespace.define("PlayerFramework.Plugins", {
        AnalyticsPlugin: AnalyticsPlugin
    });

})(PlayerFramework);

