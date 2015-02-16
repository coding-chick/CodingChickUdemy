/*!
  Copyright (C) Microsoft. All rights reserved.
  This library is supported for use in Windows Store apps only.
*/
/// <disable>EnableStrictMode.EnforceModulePattern</disable>
"use strict";
var ORMMA_STATE_UNKNOWN = "unknown";
var ORMMA_STATE_HIDDEN = "hidden";
var ORMMA_STATE_DEFAULT = "default";
var ORMMA_STATE_EXPANDED = "expanded";
var ORMMA_STATE_RESIZED = "resized";
var ORMMA_STATE_SUSPENDED = "suspended";

var ORMMA_EVENT_ERROR = "error";
var ORMMA_EVENT_HEADING_CHANGE = "headingChange";
var ORMMA_EVENT_KEYBOARD_CHANGE = "keyboardChange";
var ORMMA_EVENT_LOCATION_CHANGE = "locationChange";
var ORMMA_EVENT_NETWORK_CHANGE = "networkChange";
var ORMMA_EVENT_ORIENTATION_CHANGE = "orientationChange";
var ORMMA_EVENT_VIEWABLE_CHANGE = "viewableChange";
var ORMMA_EVENT_READY = "ready";
var ORMMA_EVENT_RESPONSE = "response";
var ORMMA_EVENT_SCREEN_CHANGE = "screenChange";
var ORMMA_EVENT_SHAKE = "shake";
var ORMMA_EVENT_SIZE_CHANGE = "sizeChange";
var ORMMA_EVENT_STATE_CHANGE = "stateChange";
var ORMMA_EVENT_TILT_CHANGE = "tiltChange";
var MAPLE_EVENT_CLICK = "click";

var ORMMA_FEATURE_SCREEN = "screen";
var ORMMA_FEATURE_ORIENTATION = "orientation";
var ORMMA_FEATURE_LOCATION = "location";
var ORMMA_FEATURE_SMS = "sms";
var ORMMA_FEATURE_PHONE = "phone";
var ORMMA_FEATURE_EMAIL = "email";
var ORMMA_FEATURE_HEADING = "heading";
var ORMMA_FEATURE_SHAKE = "shake";
var ORMMA_FEATURE_TILT = "tilt";
var ORMMA_FEATURE_NETWORK = "network";
var ORMMA_FEATURE_CALENDAR = "calendar";
var ORMMA_FEATURE_CAMERA = "camera";
var ORMMA_FEATURE_MAP = "map";
var ORMMA_FEATURE_AUDIO = "audio";
var ORMMA_FEATURE_VIDEO = "video";
var ORMMA_FEATURE_LEVEL1 = "level-1";
var ORMMA_FEATURE_LEVEL2 = "level-2";
var ORMMA_FEATURE_LEVEL3 = "level-3";
var ORMMA_API_VERSION = "1.1.0";
var MRAID_API_VERSION = "1.0";
var MAPLE_MAX_STATE_DATA_SIZE = 65536;


// These are callbacks which will be used by the C# client when information
// is requested by rich media.
function setScreenSize(width, height) {
    /// <summary>
    /// Helper method for C#/Javascript interaction. Forwards data on to associated ormma method.
    /// </summary>
    /// <param name="width" type="String">the screen width</param>
    /// <param name="height" type="String">the screen height</param>
    var convertWidth = parseInt(width);
    convertWidth = convertWidth === NaN ? -1 : convertWidth;
    var convertHeight = parseInt(height);
    convertHeight = convertHeight === NaN ? -1 : convertHeight;

    Ormma._setScreenSize(convertWidth, convertHeight);
}

function invokeInit() {
    /// <summary>
    /// Called when the c# layer has initialized and raises the ready event/calls ORMMAReady.
    /// </summary>
    Ormma._init();
}

function reportError(action, msg) {
    /// <summary>
    /// Helper method for C#/Javascript interaction. Forwards data on to associated ormma method.
    /// </summary>
    /// <param name="action" type="String">the Ormma action that resulted in the error, empty if not known</param>
    /// <param name="msg" type="String">the msg associated with the error</param>
    Ormma._throwError(action, msg);
}

function setOrmmaState(state) {
    /// <summary>
    /// Helper method for C#/Javascript interaction. Forwards data on to associated ormma method.
    /// </summary>
    /// <param name="state" type="String">the state of the Ormma control, can be unknown, hidden, default, expanded, resized</param>
    Ormma._setState(state);
}

function setOrmmaLocale(locale) {
    /// <summary>
    /// Helper method for C#/Javascript interaction. Forwards data on to associated ormma method.
    /// </summary>
    /// <param name="locale" type="String">the locale of the Ormma control</param>
    Ormma._setLocale(locale);
}

function setSize(width, height) {
    /// <summary>
    /// Helper method for C#/Javascript interaction. Forwards data on to associated ormma method.
    /// </summary>
    /// <param name="width" type="String">the control width</param>
    /// <param name="height" type="String">the control height</param>
    var convertWidth = parseInt(width);
    convertWidth = convertWidth === NaN ? -1 : convertWidth;
    var convertHeight = parseInt(height);
    convertHeight = convertHeight === NaN ? -1 : convertHeight;

    Ormma._setSize(convertWidth, convertHeight);
}

function setMaxSize(width, height) {
    /// <summary>
    /// helper method for C#/Javascript interaction, forwards data onto
    /// associated ormma method
    /// </summary>
    /// <param name="width" type="String">the control width</param>
    /// <param name="height" type="String">the control height</param>
    var convertWidth = parseInt(width);
    convertWidth = convertWidth === NaN ? -1 : convertWidth;
    var convertHeight = parseInt(height);
    convertHeight = convertHeight === NaN ? -1 : convertHeight;

    Ormma._setMaxSize(convertWidth, convertHeight);
}

function setOrientation(orientation) {
    /// <summary>
    /// helper method for C#/Javascript interaction, forwards data onto
    /// associated ormma method
    /// </summary>
    /// <param name="orientation" type="Number">the orientation of the phone, 
    /// -1 - device orientation unknown
    /// 0 - portrait, 
    /// 90 - clockwise to landscape
    /// 180 - portrait upside down
    /// 270 - counter clockwise to landscape
    /// </param>   
    var convertOrientation = parseInt(orientation);
    convertOrientation = convertOrientation === NaN ? -1 : convertOrientation;
    Ormma._setOrientation(convertOrientation);
}

function fireShake() {
    /// <summary>
    /// Helper method for C#/Javascript interaction. Forwards data on to associated ormma method.
    /// </summary>
    Ormma._shake();
}

function updateTiltCoords(x, y, z) {
    /// <summary>
    /// Helper method for C#/Javascript interaction. Forwards data on to associated ormma method.
    /// </summary>
    /// <param name="x" type="Number">the x acceleration value</param>
    /// <param name="y" type="Number">the y acceleration value</param>
    /// <param name="z" type="Number">the z acceleration value</param>
    Ormma._tiltChange({ x: x, y: y, z: z });
}

function fireViewable(isViewable) {
    /// <summary>
    /// Helper method for C#/Javascript interaction. Forwards data on to associated ormma method.
    /// </summary>
    var isViewableBool = stringToBoolean(isViewable);
    Ormma._viewableChange(isViewableBool);
}

function setCapability(capability, value) {
    /// <summary>
    /// Helper method for C#/Javascript interaction. Forwards data on to associated ormma method.
    /// </summary>
    /// <param name="capability" type="String">the capability to enable/disable</param>
    /// <param name="value" type="String">"true" to enable "false" to disable</param>
    Ormma._setCapability(capability, stringToBoolean(value));
}

function stringToBoolean(value) {
    /// <summary>
    /// Converts a string to its boolean value.
    /// </summary>
    /// <param name="value" type="String">the value to convert</param>
    /// <returns type="Boolean">true if value is "true" (case-insensitive), otherwise false.</returns>
    if (value.toLowerCase() === "true") {
        return true;
    }
    return false;
}

function fireResponse(url, response) {
    /// <summary>
    /// Helper method for C#/Javascript interaction. Forwards data on to associated ormma method.
    /// </summary>

    // The data coming from the C# code is not escaped, so do it now.
    var data = { url: escape(url), response: escape(response) };
    Ormma._sendResponse(data);
}

function setNetwork(networkType) {
    /// <summary>
    /// Helper method for C#/Javascript interaction. Forwards data on to associated ormma method.
    /// </summary>
    Ormma._setNetwork(networkType);
}

function setSdkVersion(sdkVersion, client, runtimeType) {
    /// <summary>
    /// Helper method for C#/Javascript interaction. Forwards data on to associated ormma method.
    /// </summary>
    Ormma._setSdkVersion(sdkVersion, client, runtimeType);
}

function setAdInstanceState(adInstanceState) {
    /// <summary>
    /// Helper method for C#/Javascript interaction. Forwards data on to associated ormma method.
    /// </summary>
    Ormma._setAdInstanceState(adInstanceState);
}

function fireClickEvent() {
    /// <summary>
    /// Helper method for C#/Javascript interaction. Forwards data on to associated ormma method.
    /// </summary>
    Ormma._clicked();
}

(function () {

    // Wrap the AdClient's data in this object
    function AdClientData() {
        this.listeners = [];
    }

    var _adClient = new AdClientData();

    // The main ad controller object
    window.ORMMA = window.ormma = window.Ormma = window.MAPLE = window.maple = window.Maple = {

        // The maximum size the control can grow to when resizing in the format { width: 480, height: 80 }. This can be modified by the developer.
        _maxSize: {},

        // Holds the current ad placement size.
        _dimensions: {},

        // Holds the default dimension in the format { width: 480, height: 80, x: 0, y: 0 }
        _defaultDimensions: {},

        // The available screen size for expanded ads
        _screenSize: null,

        // Set on initialization by ad control, used to determine if there is support
        // for detecting tilt.
        _tiltCapability: false,

        // Set on initialization by ad control, used to determine if there is support
        // for detecting shake.
        _shakeCapability: false,

        // Stores properties used during expanding.
        // Supported properties are width, height, useCustomClose, isModal, lockOrientation.
        _expandProperties: {
            width: 480,
            height: 800,
            useCustomClose: false,
            lockOrientation: false,
            isModal: true
        },

        // Holds the current shake property values, currently unsupported
        _shakeProperties: {},

        // Holds the current resize property values, currently unsupported
        _resizeProperties: {
            transition: "none"
        },

        // Holds the current state of the control
        _state: ORMMA_STATE_DEFAULT,

        // Holds the current locale of the control
        _locale: null,

        // Holds the last known location of the device as JSON.
        // Example: {lat: 32, lon: -44, acc: 10}
        _location: null,

        // -1 - device orientation unknown
        // 0 - portrait, 
        // 90 - clockwise to landscape
        // 180 - portrait upside down
        // 270 - counter clockwise to landscape
        _orientation: -1,

        // Holds the last recorded tilt coordinates
        _lastTiltCoords: { x: 0, y: 0, z: 0 },

        // Holds the last recorded network state
        _lastNetworkState: ORMMA_STATE_UNKNOWN,

        // Holds the SDK info
        _sdkInfo: null,

        // Stores the ad instance state
        _adInstanceState: null,

        // Stores the mapping of the IE11+ msOrientation property to the ormma returned values. Also stores the
        // IE11+ msOrientation property name.
        _msOrientationModes: {
            portaitPrimary: { ieProperty: "portrait-primary", orientationDegrees: 0 },
            landscapePrimary: { ieProperty: "landscape-primary", orientationDegrees: 270 },
            portaitSecondary: { ieProperty: "portrait-secondary", orientationDegrees: 180 },
            landscapeSecondary: { ieProperty: "landscape-secondary", orientationDegrees: 90 },
            unknown: { ieProperty: "", orientationDegrees: -1 }
        },

        adAnchorReady: function () {
            /// <summary>
            /// called by the ad/renderer when it has finished rendering. This was added
            /// to help the WebView workarounds so that the AdvertisingWebBrowser knows 
            /// when to take a screenshot to use as the anchor image
            /// </summary>
            _notify("rendered");
        },

        adError: function (action, message) {
            /// <summary>
            /// reports to ad sdk that the ad has an error and should be invalidated/thrown away
            /// </summary>
            /// <param name="action" type="String">action that caused the error</param>
            /// <param name="message" type="String">details of the error</param>

            var a;
            if (typeof (action) === "string" && action !== null) {
                a = action;
            } else {
                a = "unknown action";
            }

            var m;
            if (typeof (message) === "string" && message !== null) {
                m = message;
            } else {
                m = "an unknown error occurred.";
            }

            _notify("aderror:action=" + a + "&msg=" + m);
        },

        storeAdInstanceState: function (adInstanceState) {
            /// <summary>
            /// stores data about the state of the ad that can be retreived later
            /// useful for passing data between default and expanded.
            /// </summary>
            /// <param name="adInstanceState" type="String">state data to store</param>
            if (typeof (adInstanceState) === "string") {

                if (adInstanceState.length <= MAPLE_MAX_STATE_DATA_SIZE) {
                    this._adInstanceState = adInstanceState;
                    _notify("storeadinstancestate:" + this._adInstanceState);
                } else {
                    this._throwError("storeAdInstanceState", "attempting to store state data greater than allowed length of " + MAPLE_MAX_STATE_DATA_SIZE + ", attempted store length " + adInstanceState.length);
                }
            } else {
                this._throwError("storeAdInstanceState", "attempting to store state data that is not a string");
            }
        },

        getAdInstanceState: function () {
            /// <summary>
            /// Gets the current ad instance state data, or returns undefined if no state has been set.
            /// </summary>
            /// <return name="adInstanceState" type="String">ad instance state data as a string</return>
            if (this._adInstanceState !== null) {
                return this._adInstanceState;
            }
        },

        addEventListener: function (evt, callback) {
            /// <summary>
            /// Adds an listener for an event
            /// </summary>
            /// <param name="evt" type="String">The name of the event to listen for</param>
            /// <param name="callback" type="Function">Name of function or an anonymous function to execute when the event occurs</param>
            if (_adClient.listeners[evt] === null || typeof (_adClient.listeners[evt]) === "undefined") {
                _adClient.listeners[evt] = [];
            }

            if (evt === ORMMA_EVENT_TILT_CHANGE && !this._tiltCapability) {
                this._throwError("tilt", "tilt capability is not supported, event listener not added");
                return;
            } else if (evt === ORMMA_EVENT_SHAKE && !this._shakeCapability) {
                this._throwError("shake", "shake capability is not supported, event listener not added");
                return;
            }

            // add listener before calling _notify since some events will fire immediately
            _logme("adding listener for " + evt);
            _adClient.listeners[evt].push(callback);

            if (evt === ORMMA_EVENT_TILT_CHANGE) {
                _notify("tilt:listener=start");
            } else if (evt === ORMMA_EVENT_SHAKE) {
                _notify("shake:listener=start");
            } else if (evt === ORMMA_EVENT_ORIENTATION_CHANGE) {
                _notify("getorientation:listener=start");
            } else if (evt === ORMMA_EVENT_VIEWABLE_CHANGE) {
                _notify("viewableChange:listener=start");
            }
        },

        removeEventListener: function (evt, listener) {
            /// <summary>
            /// Use this method to unsubscribe a specific handler method from a specific event. Event listeners should
            /// always be removed when they are no longer useful to avoid errors. If no listener function is provided,
            /// then all functions listening to the event will be removed.
            /// </summary>
            /// <param name="evt" type="String">name of event to unsubscribe from</param>
            /// <param name="listener" type="Function">the function to unsubscribe</param>
            _logme("removing listener for " + evt);
            if (_adClient.listeners[evt] !== null && typeof (_adClient.listeners[evt]) !== "undefined") {
                var callbacks = _adClient.listeners[evt];
                var ix;
                for (ix = 0; ix < callbacks.length; ix++) {
                    if (callbacks[ix] === listener) {
                        callbacks.splice(ix, 1);
                        break;
                    }
                }

                // Only send the stop message if no more client listeners are present
                if (_adClient.listeners[evt] === null || typeof (_adClient.listeners[evt]) === "undefined" || _adClient.listeners[evt].length === 0) {
                    if (evt === ORMMA_EVENT_TILT_CHANGE) {
                        if (!this._tiltCapability) {
                            this._throwError("tilt", "tilt capability is not supported, no event listener");
                            return;
                        }
                        _logme("stopping tilt device for event: " + evt + " (no more client listeners)");
                        _notify("tilt:listener=stop");
                    } else if (evt === ORMMA_EVENT_SHAKE) {
                        if (!this._shakeCapability) {
                            this._throwError("shake", "shake capability is not supported, no event listener");
                            return;
                        }
                        _logme("stopping shake device for event: " + evt + " (no more client listeners)");
                        _notify("shake:listener=stop");
                    } else if (evt === ORMMA_EVENT_ORIENTATION_CHANGE) {
                        _logme("stopping orientation changed event: " + evt + " (no more client listeners)");
                        _notify("getorientation:listener=stop");
                    } else if (evt === ORMMA_EVENT_VIEWABLE_CHANGE) {
                        _logme("stopping viewable changed event: " + evt + " (no more client listeners)");
                        _notify("viewableChange:listener=stop");
                    } else if (evt === MAPLE_EVENT_CLICK) {
                        _logme("stopping clickEvent : " + evt + " (no more client listeners)");
                    }
                }
            }
        },

        hasListenerForClickEvent: function () {
            /// <summary>
            /// Returns true if there are any listeners subscribed to the "click" event, otherwise returns false.
            /// This is a workaround for detecting the renderer's support of the "click" event by the bootstrap 
            /// to allow/reject the renderer based on version required by SDK.
            /// </summary>
            if (typeof (_adClient.listeners[MAPLE_EVENT_CLICK]) !== "undefined"
                && _adClient.listeners[MAPLE_EVENT_CLICK] !== null
                && _adClient.listeners[MAPLE_EVENT_CLICK].length > 0) {
                return true;
            }
            else {
                return false;
            }
        },

        close: function () {
            /// <summary>
            /// Closes the ad when it is in the expanded or resized state and issues stateChange
            /// event, hides when in the default state, and does nothing in the hidden state
            /// </summary>
            _logme("sending close request");
            _notify("close");
        },

        createEvent: function (date, title, body) {
            /// <summary>
            /// Currently not supported
            /// </summary>
            _logme("[not impl] createEvent called: " + date + ", " + title + ", " + body);
        },

        expand: function (url) {
            /// <summary>
            /// Opens a new web view/iframe in the expanded state
            /// </summary>
            /// <param name="url" type="String">The url to navigate the expanded control to</param>
            if (!(this.getState() === ORMMA_STATE_DEFAULT || this.getState() === ORMMA_STATE_RESIZED)) {
                reportError("expand", "can only expand from resized or default states");
                return;
            }

            if (typeof (url) === "undefined" || url === null) {
                url = "";
            }

            // Send expandProperties to the ad control first
            if (typeof (this._expandProperties) === "object") {
                /// <disable>JS2045.ReviewEmptyBlocks</disable>
                if (_isEmbeddedBrowser()) {
                    // Do nothing, should already be set
                } else {
                    var propStr = JSON.stringify(this._expandProperties);
                    _notify("setexpandproperties:" + propStr);
                }
                /// <enable>JS2045.ReviewEmptyBlocks</enable>
            }

            _logme("sending expand request");

            if (_isEmbeddedBrowser()) {
                _notify("expand:url=" + encodeURIComponent(url));
            } else {
                // For WWAs we want to send the payload as JSON since it's easy to parse in JavaScript.
                var msg = { url: url };
                _notify("expand:" + JSON.stringify(msg));
            }
        },

        getDefaultPosition: function () {
            /// <summary>
            /// Gets the default expand position.
            /// </summary>
            /// <returns type="Object">
            /// the default position in the format {x : 0, y : 0, height : 800, width : 480}
            /// </returns>
            return this._defaultDimensions;
        },

        getExpandProperties: function () {
            /// <summary>
            /// Gets the current expand properties.
            /// </summary>
            /// <returns type="Object">the current expand properties in the format:
            /// { width : "nn", height : "nn", useCustomClose : "true|false", isModal : "true|false", lockOrientation : "true|false", useBackground : "true|false (deprecated)", backgroundColor : "#rrggbb (deprecated)", backgroundOpacity : "n.n (deprecated)" }
            /// </returns>
            return this._expandProperties;
        },

        getHeading: function () {
            /// <summary>
            /// Gets the current heading/compass direction.
            /// Currently unsupported.
            /// </summary>
            /// <returns type="Number" integer="true">always returns -1 - no heading known</returns>
            _logme("[not impl] getHeading called");
            return -1;
        },

        getKeyboard: function () {
            /// <summary>
            /// Currently not supported
            /// </summary>
            _logme("[not impl] getKeyboard called");
            return false;
        },

        getLocation: function () {
            /// <summary>
            /// Currently not supported
            /// </summary>
            _logme("[not impl] getKeyboard called");
            return this._location;
        },

        getMaxSize: function () {
            /// <summary>
            /// returns the max size the adcontrol can expand to
            /// </summary>
            /// <returns type="Object"/>
            return this._maxSize;
        },

        getNetwork: function () {
            /// <summary>
            /// returns the most recent network status of the device
            /// </summary>
            /// <returns type="String">the most recent network status of the device</returns>
            return this._lastNetworkState;
        },

        getOrientation: function () {
            /// <summary>
            /// returns the orientation of the device
            /// </summary>
            /// <returns type="Number"/>

            // convert the orientation to the ormma value, set if different.
            var newOrientation = this._msOrientationToOrmmaDegrees(window.screen.msOrientation);
            this._orientation = this._orientation === newOrientation ? this._orientation : newOrientation;
            //postToLocal("log:getOrientation called orientation:" + newOrientation);
            return this._orientation;
        },

        getPlacementType: function (none) {
            /// <summary>
            /// Currently not supported
            /// </summary>
            /// <returns type="String"/>
            _logme("[not impl] getPlacementType called");
            return "unknown";
        },

        getScreenSize: function () {
            /// <summary>
            /// Gets the device screen size
            /// </summary>
            /// <returns type="Object">the device screen size in the format {width: 480, height: 800} or {} if not known</returns>
            if (this._screenSize === null) {
                // Subtract 100 from the screen size to allow space for close bands
                this._screenSize = { width: screen.availWidth, height: screen.availHeight - 100 };
            }
            return this._screenSize;
        },

        getShakeProperties: function () {
            /// <summary>
            /// Currently not supported
            /// </summary>
            /// <returns type="Object"/>
            return this._shakeProperties;
        },

        getSize: function () {
            /// <summary>
            /// Gets the current size of the ad
            /// </summary>
            /// <returns type="Object">the ad size in the format {width: 480, height: 800} or {} if not known</returns>
            if (typeof (this._dimensions) === "undefined") {
                this._dimensions = {};
            }
            return this._dimensions;
        },

        getState: function () {
            /// <summary>
            /// Gets the current state of the ad, can be
            /// </summary>
            /// <returns type="String">the current state of the ad, can be hidden, default, expanded, resized</returns>
            return this._state;
        },

        getLocale: function () {
            /// <summary>
            /// Gets the current locale of the ad, can be null/empty
            /// </summary>
            /// <returns type="String">the current locale of the ad</returns>
            if (this._locale !== null) {
                return this._locale;
            }
        },

        getTilt: function () {
            /// <summary>
            /// Returns the last known tilt coordinates.
            /// </summary>
            /// <returns type="Object"/>
            _logme("calling getTilt");

            if (!this._tiltCapability) {
                this._throwError("tilt", "tilt capability is not supported");
                return;
            }
            _notify("tilt:gettilt=refresh");
            return this._lastTiltCoords;
        },

        getViewable: function () {
            /// <summary>
            /// Currently not supported
            /// </summary>
            _logme("[not impl] getViewable called");
        },

        hide: function () {
            /// <summary>
            /// moves control from the default state to the hidden state
            /// </summary>
            _logme("calling hide");
            _notify("hide");
        },

        isViewable: function () {
            /// <summary>
            /// Currently not supported.
            /// Logic is implemented but this function should not return anything since we cannot guarantee an up-to-date viewable state.
            /// Use the viewableChange event instead.
            /// </summary>
            _logme("[not impl] isViewable called");
        },

        makeCall: function (number) {
            /// <summary>
            /// Initiates a call to the specified number
            /// </summary>
            /// <param name="number" type="String">
            /// the the number to call, it may start with a "+" and contain "-" separators e.g. "+1-555-555-5555"
            /// </param>
            _logme("sending call request: " + number);
            _notify("call:recipient=" + number);
        },

        open: function (url, controls) {
            /// <summary>
            /// Navigates to a web page in an external browser ie the devices default web page
            /// </summary>
            /// <param name="url" type="String">The url to navigate to</param>
            /// <param name="controls" type="Object">this parameter is not supported</param>
            _logme("sending website request: " + url);
            var msg;
            if (_isEmbeddedBrowser()) {
                msg = "url=" + encodeURIComponent(url);
            } else {
                msg = JSON.stringify({ url: url });
            }

            _notify("web:" + msg);
        },

        openMap: function (poi, fullscreen) {
            /// <summary>
            /// Displays a map. Not currently supported.
            /// </summary>
            /// <param name="poi" type="String">parameter must describe a point on a map</param>
            /// <param name="fullscreen" type="Boolean">whether the map displays within the current view or within a new full screen view</param>
            _logme("[not impl] openMap called: " + poi);
        },

        request: function (url, display) {
            /// <summary>
            /// Requests a URI resource through the Ormma API
            /// </summary>
            var requestParams = {
                url: url,
                display: (typeof (display) !== "undefined" && display !== null ? display : "ignore")
            };
            _notify("request:" + JSON.stringify(requestParams));
            return false;
        },

        resize: function (width, height) {
            /// <summary>
            /// Resizes the adControl to the spcified height and width.
            /// <param name="width" type="Number">the max width that can be used on resize</param>
            /// <param name="height" type="Number">the max height that can be used on resize</param>
            /// </summary>
            if (width > this._maxSize.width || width < 0) {
                this._throwError("resize", "width is greater than max allowed width (" + this._maxSize.width + ") or less than zero.");
                return;
            }

            if (height > this._maxSize.height || width < 0) {
                this._throwError("resize", "height is greater than max allowed height  (" + this._maxSize.height + ") or less than zero.");
                return;
            }

            _logme("calling resize:width=" + width + "&height=" + height);

            if (_isEmbeddedBrowser()) {
                _notify("resize:width=" + width + "&height=" + height);
            } else {
                // For WWAs we want to send the payload as JSON since it's easy to parse in JavaScript.
                var msg = { width: width, height: height };
                _notify("resize:" + JSON.stringify(msg));
            }
        },

        sendMail: function (recipient, subject, body) {
            /// <summary>
            /// Initiates sending an email
            /// </summary>
            /// <param name="recipient" type="String">The recipient email address</param>
            /// <param name="subject" type="String">The email subject</param>
            /// <param name="body" type="String">The body of the email</param>
            _logme("sending mail request: " + recipient);
            _notify("mail:recipient=" + recipient + "&subject=" + subject + "&body=" + body);
        },

        sendSMS: function (recipient, body) {
            /// <summary>
            /// Initiates sending an SMS
            /// </summary>
            /// <param name="recipient" type="String">The recipient sms telephone number</param>
            /// <param name="body" type="String">The body of the sms</param>
            _logme("sending sms request: " + recipient);
            _notify("sms:recipient=" + recipient + "&body=" + body);
        },

        setExpandProperties: function (properties) {
            /// <summary>
            /// Sets the expand properties
            /// </summary>
            /// <param name="properties" type="Object">
            /// contains the properties to set, if a property is not present defaults are used:
            /// { width: "nn", height: "nn", useCustomClose:  "true|false",  isModal: "true|false", lockOrientation: "true|false", useBackground: "true|false (deprecated)", backgroundColor: "#rrggbb (deprecated)", backgroundOpacity: "n.n (deprecated)" }
            /// </param>

            this._expandProperties = typeof (properties) !== "object" ? {} : properties;

            // MRAID doesn't have getScreenSize so borrowing from Ormma object is needed
            var screenSize = (typeof (this.getScreenSize) === "function" ? this.getScreenSize() : window.ormma.getScreenSize());

            this._expandProperties.width = _isValidExpandPropertiesDimension(this._expandProperties.width) ? this._expandProperties.width : screenSize.width;
            this._expandProperties.height = _isValidExpandPropertiesDimension(this._expandProperties.height) ? this._expandProperties.height : screenSize.height;
            this._expandProperties.useCustomClose = typeof (this._expandProperties.useCustomClose) === "undefined" ? false : this._expandProperties.useCustomClose;
            this._expandProperties.lockOrientation = typeof (this._expandProperties.lockOrientation) === "undefined" ? false : this._expandProperties.lockOrientation;
            this._expandProperties.isModal = true;

            if (_isEmbeddedBrowser()) {
                // Send as name/value pairs
                _logme("setting expand properties: width=" + this._expandProperties.width + "&height=" + this._expandProperties.height + "&usecustomclose=" + this._expandProperties.useCustomClose + "&lockorientation=" + this._expandProperties.lockOrientation);
                _notify("setexpandproperties:width=" + this._expandProperties.width + "&height=" + this._expandProperties.height + "&usecustomclose=" + this._expandProperties.useCustomClose + "&lockorientation=" + this._expandProperties.lockOrientation);
            } else {
                // Send as JSON
                var propStr = JSON.stringify(this._expandProperties);
                _logme("setting expand properties: " + propStr);
                _notify("setexpandproperties:" + propStr);
            }
        },

        setResizeProperties: function (properties) {
            /// <summary>
            /// Currently not supported
            /// </summary>
            this._resizeProperties = typeof (properties) !== "object" ? {} : properties;
            _logme("setting resize properties: " + JSON.stringify(this._resizeProperties));
        },

        setShakeProperties: function (properties) {
            /// <summary>
            /// Sets the properties that control how shake detection works.
            /// </summary>
            this._shakeProperties = typeof (properties) !== "object" ? {} : properties;
            _logme("setting shake properties: " + JSON.stringify(this._shakeProperties));
        },

        setUserEngaged: function (isEngaged) {
            /// <summary>
            /// Sets whether the user is current engaged with the ad, e.g. has playing a video or a game.
            /// </summary>
            /// <param name="isEngaged" type="Boolean">whether the user is engaged with ad</param>
            this._isUserEngaged = typeof (isEngaged) === "boolean" ? isEngaged : false;
            _logme("setting user engaged: " + this._isUserEngaged);
            _notify("setuserengaged:engaged=" + this._isUserEngaged);
        },

        show: function () {
            /// <summary>
            /// Moves control from the hidden to the default state
            /// </summary>
            _logme("calling show");
            _notify("show");
        },

        storePicture: function (url) {
            /// <summary>
            /// Stores a picture, such as a coupon, to the users media library
            /// </summary>
            /// <param name="url" type="String">Location of the image to store</param>
            _logme("sending storePicture request: " + url);
            _notify("storePicture:url=" + encodeURIComponent(url));
        },

        supports: function (feature) {
            /// <summary>
            /// Checks if an ORMMA feature is supported
            /// </summary>
            /// <param name="feature" type="String">the feature to check support for</param>
            /// <returns type="Boolean">true if feature is supported, false otherwise</returns>
            switch (feature) {
                case ORMMA_FEATURE_SCREEN:
                case ORMMA_FEATURE_ORIENTATION:
                case ORMMA_FEATURE_LEVEL1:
                case ORMMA_FEATURE_LEVEL2:
                case ORMMA_FEATURE_NETWORK:
                    return true;
                case ORMMA_FEATURE_SHAKE:
                    return this._shakeCapability;
                case ORMMA_FEATURE_TILT:
                    return this._tiltCapability;
                case ORMMA_FEATURE_PHONE:
                case ORMMA_FEATURE_SMS:
                case ORMMA_FEATURE_EMAIL:
                case ORMMA_FEATURE_LOCATION:
                case ORMMA_FEATURE_HEADING:
                case ORMMA_FEATURE_CALENDAR:
                case ORMMA_FEATURE_AUDIO:
                case ORMMA_FEATURE_VIDEO:
                case ORMMA_FEATURE_LEVEL3:
                default:
                    return false;
            }
        },

        useCustomClose: function (flag) {
            /// <summary>
            /// if true the adControl will allow use of the full screen, if false adControl will 
            /// provide mechanism to close the ad (black bands)
            /// </summary>
            /// <param name="flag" type="boolean">if true don't priovide a close button/mechanism, otherwise provide close button</param>
            if (typeof (flag) === "boolean") {
                _logme("calling usecustomclose:" + flag);
                _notify("usecustomclose:" + flag);
            }
            else {
                reportError("useCustomClose", "parameter 'flag' is not a boolean value");
            }
        },

        playVideo: function (url) {
            /// <summary>
            /// not currently supported 
            /// </summary>
        },

        playAudio: function (url) {
            /// <summary>
            /// not currently supported
            /// </summary>
        },


        // The functions below allow additional features not supported by the Ormma API.

        addPhoneContact: function (phone) {
            /// <summary>
            /// launches the add phone contact dialog
            /// </summary>
            /// <param name="phone" type="String">the phone number to add to the contact</param>
            _logme("adding phone contact : " + phone);
            _notify("addphonecontact:phonenumber=" + phone);
        },

        addEmailContact: function (email) {
            /// <summary>
            /// Launches the add email contact dialog
            /// </summary>
            /// <param name="phone" type="String">the email address to add to the contact</param>
            _logme("adding email contact : " + email);
            _notify("addemailcontact:email=" + email);
        },

        fanOnFacebook: function (url) {
            /// <summary>
            /// Opens facebook fan page in an external web browser
            /// </summary>
            /// <param name="url" type="String">the url of the fan page</param>
            _logme("sending fanOnFacebook request: " + url);
            this.open(url);
        },

        fanOnTwitter: function (url) {
            /// <summary>
            /// Opens twitter an external web browser
            /// </summary>
            /// <param name="url" type="String">the url of the fan page</param>
            _logme("sending fanOnTwitter request: " + url);
            this.open(url);
        },

        openMarketplace: function (applicationId) {
            /// <summary>
            /// Opens the application marketplace
            /// </summary>
            /// <param name="applicationId" type="String">the application id(guid) to navigate to in the marketplace</param>
            _logme("opening marketplace appId: " + applicationId);
            _notify("marketplace:appid=" + applicationId);
        },

        getVersion: function () {
            ///	<summary>
            /// Returns version number of API specification
            ///	</summary>
            /// <returns type="String">version number of the ORMMA API specification</returns>
            return ORMMA_API_VERSION;
        },

        getSdkVersion: function () {
            ///	<summary>
            /// Returns version number of SDK
            ///	</summary>
            /// <returns type="String">version number of the SDK</returns>
            if (this._sdkInfo !== null) {
                return this._sdkInfo;
            }
        },

        _clicked: function () {
            ///	<summary>
            /// raises the clicked event, this will notify the ad renderer that a user
            /// has clicked on the ad. This will only be raised by the Xaml AdControl when
            /// the developer has set UseStaticAnchor=true
            ///	</summary>
            _fireEvent(MAPLE_EVENT_CLICK, null);
        },

        // These calls are be necessary for the C# control to send data and change events to the JS library.
        // They should not be called by the ad.
        _tiltChange: function (data) {
            /// <summary>
            /// Fires the tilt change event.
            /// </summary>
            this._lastTiltCoords = data;
            _fireEvent(ORMMA_EVENT_TILT_CHANGE, data);
        },

        _shake: function () {
            /// <summary>
            /// Fires the shake event.
            /// </summary>
            _fireEvent(ORMMA_EVENT_SHAKE);
        },

        _viewableChange: function (data) {
            /// <summary>
            /// Fires the viewable event.
            /// </summary>
            _fireEvent(ORMMA_EVENT_VIEWABLE_CHANGE, data);
        },

        _orientationChange: function (change) {
            /// <summary>
            /// Currently not supported
            /// </summary>
        },

        _networkChange: function (change) {
            /// <summary>
            /// Currently not supported
            /// </summary>
        },

        _headingChange: function (heading) {
            /// <summary>
            /// Currently not supported
            /// </summary>
        },

        _locationChanged: function (lat, lon, accuracy) {
            /// <summary>
            /// Currently not supported
            /// </summary>
        },

        _sendResponse: function (data) {
            /// <summary>
            /// Generates a response event.
            /// </summary>
            /// <param name="data" type="String">response data for event</param>
            _fireEvent(ORMMA_EVENT_RESPONSE, data);
        },

        _throwError: function (action, message) {
            /// <summary>
            /// Generates an error event.
            /// </summary>
            /// <param name="action" type="String">the action which caused the error</param>
            /// <param name="message" type="String">the error message</param>
            var data = { message: message, action: action };
            _fireEvent(ORMMA_EVENT_ERROR, data);
        },

        _init: function () {
            /// <summary>
            /// This function tells the ORMMA implementation to notify the ad that ORMMA is ready.
            /// </summary>
            _logme("Ormma is ready");
            _fireEvent(ORMMA_EVENT_READY, null);

            // ORMMA spec says to cal this window-scope function when ready.
            if (typeof (window.ORMMAReady) === "function") {
                _logme("Ormma calling ORMMAReady()");
                window.ORMMAReady();
            }
        },

        _setOrientation: function (orientation) {
            /// <summary>
            /// Helper method for setting the orientation by ad control
            /// </summary>
            /// <param name="orientation" type="Number">the orientation of the phone, 
            /// -1  - device orientation unknown
            /// 0   - portrait, 
            /// 90  - clockwise to landscape
            /// 180 - portrait upside down
            /// 270 - counter clockwise to landscape
            /// </param>  
            var oldOrientation = this._orientation;
            this._orientation = orientation;
            if (oldOrientation !== this._orientation) {
                _fireEvent(ORMMA_EVENT_ORIENTATION_CHANGE, this._orientation);
            }
        },

        _setState: function (state) {
            /// <summary>
            /// Helper method for setting the state by ad control
            /// </summary>
            /// <param name="state" type="String">the current state of the ad control</param>
            var oldState = this._state;
            this._state = state;
            if (oldState !== state) {
                _fireEvent(ORMMA_EVENT_STATE_CHANGE, this._state);
            }
        },

        _setLocale: function (locale) {
            /// <summary>
            /// Helper method for setting the locale by ad control
            /// </summary>
            /// <param name="locale" type="String">the current locale of the ad control</param>
            this._locale = locale;
        },

        _setSize: function (width, height) {
            /// <summary>
            /// Helper method for setting the size by ad control
            /// </summary>
            /// <param name="width" type="Number">the current state of the ad control</param>
            /// <param name="height" type="Number">the current state of the ad control</param>
            if (typeof (this._dimensions) === "undefined") {
                this._dimensions = {};
            }

            var currWidth = typeof (this._dimensions.width) === "undefined" ? 0 : this._dimensions.width;
            var currHeight = typeof (this._dimensions.height) === "undefined" ? 0 : this._dimensions.height;

            this._dimensions.width = width;
            this._dimensions.height = height;

            if (currWidth !== width || currHeight !== height) {
                _fireEvent(ORMMA_EVENT_SIZE_CHANGE, this._dimensions);
            }
        },

        _setSdkVersion: function (sdkVersion, client, runtimeType) {
            /// <summary>
            /// Helper method for setting the SDK version string
            /// </summary>
            /// <param name="sdkVersion" type="String">the sdk version string</param>
            /// <param name="client" type="String">the sdk client string</param>
            /// <param name="runtimeType" type="String">the type of runtime currently loaded</param>
            this._sdkInfo = {
                sdkVersion: sdkVersion,
                client: client,
                runtimeType: runtimeType,
                appName: navigator.appName,
                appVersion: navigator.appVersion,
                userAgent: navigator.userAgent,
                platform: navigator.platform
            };
        },

        _setCapability: function (capability, value) {
            /// <summary>
            /// Helper method for setting capabilities
            /// </summary>
            /// <param name="capability" type="String">the cabability to set</param>
            /// <param name="value" type="Boolean">true if the capability is available, false otherwise</param>

            if (typeof (value) !== "boolean") {
                return;
            }

            if (capability === ORMMA_FEATURE_TILT) {
                this._tiltCapability = value;
            } else if (capability === ORMMA_FEATURE_SHAKE) {
                this._shakeCapability = value;
            }
        },

        _setMaxSize: function (width, height) {
            /// <summary>
            /// Helper method for setting the size from c#
            /// </summary>
            /// <param name="width" type="String">the max width that can be used on resize</param>
            /// <param name="height" type="String">the max height that can be used on resize</param>
            if (typeof (this._maxSize) === "undefined") {
                this._maxSize = {};
            }

            this._maxSize.width = width;
            this._maxSize.height = height;
        },

        _setScreenSize: function (width, height) {
            /// <summary>
            /// Sets the device screen size
            /// </summary>
            /// <param name="width" type="Number">the width of the screen</param>
            /// <param name="height" type="Number">the height of the screen</param>
            if (this._screenSize === null || typeof (this._screenSize) === "undefined") {
                this._screenSize = {};
            }

            var currWidth = typeof (this._screenSize.width) === "undefined" ? 0 : this._screenSize.width;
            var currHeight = typeof (this._screenSize.height) === "undefined" ? 0 : this._screenSize.height;

            this._screenSize.width = width;
            this._screenSize.height = height;

            if (currWidth !== this._screenSize.width || currHeight !== this._screenSize.height) {
                _fireEvent(ORMMA_EVENT_SCREEN_CHANGE, this._screenSize);
            }
        },

        _setNetwork: function (networkState) {
            /// <summary>
            /// Sets the network state
            /// </summary>
            /// <param name="networkState" type="String">new network state</param>

            _logme("setting network state: " + networkState);
            if (typeof (this._lastNetworkState) === "undefined") {
                this._lastNetworkState = "";
            }

            if (networkState !== this._lastNetworkState) {
                this._lastNetworkState = networkState;
                _fireEvent(ORMMA_EVENT_NETWORK_CHANGE, {
                    online: (networkState !== "offline"),
                    connection: networkState
                });
            }
        },

        _setAdInstanceState: function (adInstanceState) {
            /// <summary>
            /// Sets the ad instance state, used for c# to js interactions.
            /// </summary>
            /// <param name="adInstanceState" type="String">the data to store</param>
            this._adInstanceState = adInstanceState;
        },

        /// Converts the msOrientation returned value to Ormma degrees.
        _msOrientationToOrmmaDegrees: function (msOrientation) {
            switch (msOrientation) {
                case this._msOrientationModes.landscapePrimary.ieProperty:
                    return this._msOrientationModes.landscapePrimary.orientationDegrees;
                case this._msOrientationModes.landscapeSecondary.ieProperty:
                    return this._msOrientationModes.landscapeSecondary.orientationDegrees;
                case this._msOrientationModes.portaitSecondary.ieProperty:
                    return this._msOrientationModes.portaitSecondary.orientationDegrees;
                case this._msOrientationModes.portaitPrimary.ieProperty:
                    return this._msOrientationModes.portaitPrimary.orientationDegrees;
                default:
                    return this._msOrientationModes.unknown.orientationDegrees;
            }
        }

    };

    function _logme(msg) {
        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="msg" type="String">the width of the screen</param>

        //if (_isEmbeddedBrowser()) {
        //    window.external.notify("$log - " + msg);
        //} 
    }

    function _isValidExpandPropertiesDimension(val) {
        /// <summary>
        /// Checks if the expand properties value is valid.
        /// </summary>
        /// <returns type="Boolean">true if item is valid, false otherwise
        /// </returns>
        if (typeof (val) === "undefined" || val === null || val <= 0) {
            return false;
        } else {
            return true;
        }
    }

    function _isEmbeddedBrowser() {
        return (typeof (window.external) === "object" && typeof (window.external.notify) !== "undefined");
    }

    function _notify(msg) {
        /// <summary>
        /// Sends a message to the ad control.
        /// </summary>
        /// <param name="msg" type="String">The message to pass to the ad control.</param>

        // Embedded browsers (WebBrowser on Windows Phone and WebView in Windows 8) use the
        // notify mechanism for sending messages to the ad control.
        // WWAs use the postToLocal function to post the message to the outer iframe.
        if (_isEmbeddedBrowser()) {
            window.external.notify(msg);
        } else {
            postToLocal(msg);
        }
    }

    function _fireEvent(eventType, data) {
        /// <summary>
        /// Generic private function to throw events to listeners. In this implementation, all events are MessageEvents.
        /// </summary>
        /// <param name="eventType" type="String">The type of event being thrown.</param>
        /// <param name="data" type="Object">The data for the event (can be JSON or string depending on the event type).</param>
        _logme("raising event " + eventType + " with data " + data);

        if (_adClient.listeners[eventType] !== null && typeof (_adClient.listeners[eventType]) !== "undefined") {
            try {
                // Make a copy of the listeners array in case it is modified by the listeners
                // themselves (e.g. listener removes itself from list).
                var callbacks = _adClient.listeners[eventType].slice();
                var ix;
                switch (eventType) {
                    case ORMMA_EVENT_ERROR:
                        for (ix = 0; ix < callbacks.length; ix++) {
                            callbacks[ix](data.message, data.action);
                        }
                        break;
                    case ORMMA_EVENT_NETWORK_CHANGE:
                        for (ix = 0; ix < callbacks.length; ix++) {
                            callbacks[ix](data.online, data.connection);
                        }
                        break;
                    case ORMMA_EVENT_ORIENTATION_CHANGE:
                        for (ix = 0; ix < callbacks.length; ix++) {
                            callbacks[ix](data);
                        }
                        break;
                    case ORMMA_EVENT_READY:
                        for (ix = 0; ix < callbacks.length; ix++) {
                            callbacks[ix]();
                        }
                        break;
                    case ORMMA_EVENT_SCREEN_CHANGE:
                        for (ix = 0; ix < callbacks.length; ix++) {
                            callbacks[ix](data.width, data.height);
                        }
                        break;
                    case ORMMA_EVENT_SHAKE:
                        for (ix = 0; ix < callbacks.length; ix++) {
                            callbacks[ix]();
                        }
                        break;
                    case ORMMA_EVENT_SIZE_CHANGE:
                        for (ix = 0; ix < callbacks.length; ix++) {
                            callbacks[ix](data.width, data.height);
                        }
                        break;
                    case ORMMA_EVENT_STATE_CHANGE:
                        for (ix = 0; ix < callbacks.length; ix++) {
                            callbacks[ix](data);
                        }
                        break;
                    case ORMMA_EVENT_TILT_CHANGE:
                        for (ix = 0; ix < callbacks.length; ix++) {
                            callbacks[ix](data.x, data.y, data.z);
                        }
                        break;
                    case ORMMA_EVENT_VIEWABLE_CHANGE:
                        for (ix = 0; ix < callbacks.length; ix++) {
                            callbacks[ix](data);
                        }
                        break;
                    case ORMMA_EVENT_RESPONSE:
                        for (ix = 0; ix < callbacks.length; ix++) {
                            callbacks[ix](unescape(data.url), unescape(data.response));
                        }
                        break;
                    case MAPLE_EVENT_CLICK:
                        for (ix = 0; ix < callbacks.length; ix++) {
                            callbacks[ix]();
                        }
                        break;
                        // The rest are not supported.
                    case ORMMA_EVENT_HEADING_CHANGE:
                    case ORMMA_EVENT_KEYBOARD_CHANGE:
                    case ORMMA_EVENT_LOCATION_CHANGE:
                    default:
                        return false;
                }
            }
            catch (err) {
                _logme("exception thrown while firing event: " + err.message);
            }
        } else {
            _logme("no listeners for event " + eventType);
        }
    }

    window.mraid = window.MRAID = {

        // MRAID 1.0 methods
        addEventListener: function (evt, callback) {
            if (this._mraidSupportedEvts === null) {
                this._mraidSupportedEvts = this._initEventList();
            }

            // If the event is supported in MRAID API then forward to ORMMA, otherwise ignore.
            if (this._mraidSupportedEvts[evt]) {
                window.ormma.addEventListener(evt, callback);
            }
        },
        close: window.ormma.close,
        expand: window.ormma.expand,
        getExpandProperties: window.ormma.getExpandProperties,
        getPlacementType: window.ormma.getPlacementType,
        getState: function () { return window.ormma._state; },
        getVersion: function () { return MRAID_API_VERSION; },
        isViewable: window.ormma.isViewable,
        open: window.ormma.open,
        removeEventListener: function (evt, callback) {
            if (this._mraidSupportedEvts === null) {
                this._mraidSupportedEvts = this._initEventList();
            }

            // If the event is supported in MRAID API then forward to ORMMA, otherwise ignore.
            if (this._mraidSupportedEvts[evt]) {
                window.ormma.removeEventListener(evt, callback);
            }
        },
        setExpandProperties: window.ormma.setExpandProperties,
        useCustomClose: window.ormma.useCustomClose,

        // MRAID 1.0 events are "error", "ready", "stateChange" and "viewableChange"
        _mraidSupportedEvts: null,

        // Initializer for events supported in MRAID
        _initEventList: function () {
            var evtArray = [];
            evtArray[ORMMA_EVENT_ERROR] =
                evtArray[ORMMA_EVENT_READY] =
                evtArray[ORMMA_EVENT_STATE_CHANGE] =
                evtArray[ORMMA_EVENT_VIEWABLE_CHANGE] = true;
            return evtArray;
        }
    };
})();

// SIG // Begin signature block
// SIG // MIIanwYJKoZIhvcNAQcCoIIakDCCGowCAQExCzAJBgUr
// SIG // DgMCGgUAMGcGCisGAQQBgjcCAQSgWTBXMDIGCisGAQQB
// SIG // gjcCAR4wJAIBAQQQEODJBs441BGiowAQS9NQkAIBAAIB
// SIG // AAIBAAIBAAIBADAhMAkGBSsOAwIaBQAEFMCb4g+MNbNF
// SIG // RWQ58oB7hxlOUQieoIIVgjCCBMMwggOroAMCAQICEzMA
// SIG // AAArOTJIwbLJSPMAAAAAACswDQYJKoZIhvcNAQEFBQAw
// SIG // dzELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0
// SIG // b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1p
// SIG // Y3Jvc29mdCBDb3Jwb3JhdGlvbjEhMB8GA1UEAxMYTWlj
// SIG // cm9zb2Z0IFRpbWUtU3RhbXAgUENBMB4XDTEyMDkwNDIx
// SIG // MTIzNFoXDTEzMTIwNDIxMTIzNFowgbMxCzAJBgNVBAYT
// SIG // AlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQH
// SIG // EwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29y
// SIG // cG9yYXRpb24xDTALBgNVBAsTBE1PUFIxJzAlBgNVBAsT
// SIG // Hm5DaXBoZXIgRFNFIEVTTjpDMEY0LTMwODYtREVGODEl
// SIG // MCMGA1UEAxMcTWljcm9zb2Z0IFRpbWUtU3RhbXAgU2Vy
// SIG // dmljZTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoC
// SIG // ggEBAKa2MA4DZa5QWoZrhZ9IoR7JwO5eSQeF4HCWfL65
// SIG // X2JfBibTizm7GCKlLpKt2EuIOhqvm4OuyF45jMIyexZ4
// SIG // 7Tc4OvFi+2iCAmjs67tAirH+oSw2YmBwOWBiDvvGGDhv
// SIG // sJLWQA2Apg14izZrhoomFxj/sOtNurspE+ZcSI5wRjYm
// SIG // /jQ1qzTh99rYXOqZfTG3TR9X63zWlQ1mDB4OMhc+LNWA
// SIG // oc7r95iRAtzBX/04gPg5f11kyjdcO1FbXYVfzh4c+zS+
// SIG // X+UoVXBUnLjsfABVRlsomChWTOHxugkZloFIKjDI9zMg
// SIG // bOdpw7PUw07PMB431JhS1KkjRbKuXEFJT7RiaJMCAwEA
// SIG // AaOCAQkwggEFMB0GA1UdDgQWBBSlGDNTP5VgoUMW747G
// SIG // r9Irup5Y0DAfBgNVHSMEGDAWgBQjNPjZUkZwCu1A+3b7
// SIG // syuwwzWzDzBUBgNVHR8ETTBLMEmgR6BFhkNodHRwOi8v
// SIG // Y3JsLm1pY3Jvc29mdC5jb20vcGtpL2NybC9wcm9kdWN0
// SIG // cy9NaWNyb3NvZnRUaW1lU3RhbXBQQ0EuY3JsMFgGCCsG
// SIG // AQUFBwEBBEwwSjBIBggrBgEFBQcwAoY8aHR0cDovL3d3
// SIG // dy5taWNyb3NvZnQuY29tL3BraS9jZXJ0cy9NaWNyb3Nv
// SIG // ZnRUaW1lU3RhbXBQQ0EuY3J0MBMGA1UdJQQMMAoGCCsG
// SIG // AQUFBwMIMA0GCSqGSIb3DQEBBQUAA4IBAQB+zLB75S++
// SIG // 51a1z3PbqlLRFjnGtM361/4eZbXnSPObRogFZmomhl7+
// SIG // h1jcxmOOOID0CEZ8K3OxDr9BqsvHqpSkN/BkOeHF1fnO
// SIG // B86r5CXwaa7URuL+ZjI815fFMiH67holoF4MQiwRMzqC
// SIG // g/3tHbO+zpGkkSVxuatysJ6v5M8AYolwqbhKUIzuLyJk
// SIG // pajmTWuVLBx57KejMdqQYJCkbv6TAg0/LCQNxmomgVGD
// SIG // ShC7dWNEqmkIxgPr4s8L7VY67O9ypwoM9ADTIrivInKz
// SIG // 58ScCyiggMrj4dc5ZjDnRhcY5/qC+lkLeryoDf4c/wOL
// SIG // Y7JNEgIjTy2zhYQ74qFH6M8VMIIE7DCCA9SgAwIBAgIT
// SIG // MwAAALARrwqL0Duf3QABAAAAsDANBgkqhkiG9w0BAQUF
// SIG // ADB5MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGlu
// SIG // Z3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMV
// SIG // TWljcm9zb2Z0IENvcnBvcmF0aW9uMSMwIQYDVQQDExpN
// SIG // aWNyb3NvZnQgQ29kZSBTaWduaW5nIFBDQTAeFw0xMzAx
// SIG // MjQyMjMzMzlaFw0xNDA0MjQyMjMzMzlaMIGDMQswCQYD
// SIG // VQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4G
// SIG // A1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0
// SIG // IENvcnBvcmF0aW9uMQ0wCwYDVQQLEwRNT1BSMR4wHAYD
// SIG // VQQDExVNaWNyb3NvZnQgQ29ycG9yYXRpb24wggEiMA0G
// SIG // CSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDor1yiIA34
// SIG // KHy8BXt/re7rdqwoUz8620B9s44z5lc/pVEVNFSlz7SL
// SIG // qT+oN+EtUO01Fk7vTXrbE3aIsCzwWVyp6+HXKXXkG4Un
// SIG // m/P4LZ5BNisLQPu+O7q5XHWTFlJLyjPFN7Dz636o9UEV
// SIG // XAhlHSE38Cy6IgsQsRCddyKFhHxPuRuQsPWj/ov0DJpO
// SIG // oPXJCiHiquMBNkf9L4JqgQP1qTXclFed+0vUDoLbOI8S
// SIG // /uPWenSIZOFixCUuKq6dGB8OHrbCryS0DlC83hyTXEmm
// SIG // ebW22875cHsoAYS4KinPv6kFBeHgD3FN/a1cI4Mp68fF
// SIG // SsjoJ4TTfsZDC5UABbFPZXHFAgMBAAGjggFgMIIBXDAT
// SIG // BgNVHSUEDDAKBggrBgEFBQcDAzAdBgNVHQ4EFgQUWXGm
// SIG // WjNN2pgHgP+EHr6H+XIyQfIwUQYDVR0RBEowSKRGMEQx
// SIG // DTALBgNVBAsTBE1PUFIxMzAxBgNVBAUTKjMxNTk1KzRm
// SIG // YWYwYjcxLWFkMzctNGFhMy1hNjcxLTc2YmMwNTIzNDRh
// SIG // ZDAfBgNVHSMEGDAWgBTLEejK0rQWWAHJNy4zFha5TJoK
// SIG // HzBWBgNVHR8ETzBNMEugSaBHhkVodHRwOi8vY3JsLm1p
// SIG // Y3Jvc29mdC5jb20vcGtpL2NybC9wcm9kdWN0cy9NaWND
// SIG // b2RTaWdQQ0FfMDgtMzEtMjAxMC5jcmwwWgYIKwYBBQUH
// SIG // AQEETjBMMEoGCCsGAQUFBzAChj5odHRwOi8vd3d3Lm1p
// SIG // Y3Jvc29mdC5jb20vcGtpL2NlcnRzL01pY0NvZFNpZ1BD
// SIG // QV8wOC0zMS0yMDEwLmNydDANBgkqhkiG9w0BAQUFAAOC
// SIG // AQEAMdduKhJXM4HVncbr+TrURE0Inu5e32pbt3nPApy8
// SIG // dmiekKGcC8N/oozxTbqVOfsN4OGb9F0kDxuNiBU6fNut
// SIG // zrPJbLo5LEV9JBFUJjANDf9H6gMH5eRmXSx7nR2pEPoc
// SIG // sHTyT2lrnqkkhNrtlqDfc6TvahqsS2Ke8XzAFH9IzU2y
// SIG // RPnwPJNtQtjofOYXoJtoaAko+QKX7xEDumdSrcHps3Om
// SIG // 0mPNSuI+5PNO/f+h4LsCEztdIN5VP6OukEAxOHUoXgSp
// SIG // Rm3m9Xp5QL0fzehF1a7iXT71dcfmZmNgzNWahIeNJDD3
// SIG // 7zTQYx2xQmdKDku/Og7vtpU6pzjkJZIIpohmgjCCBbww
// SIG // ggOkoAMCAQICCmEzJhoAAAAAADEwDQYJKoZIhvcNAQEF
// SIG // BQAwXzETMBEGCgmSJomT8ixkARkWA2NvbTEZMBcGCgmS
// SIG // JomT8ixkARkWCW1pY3Jvc29mdDEtMCsGA1UEAxMkTWlj
// SIG // cm9zb2Z0IFJvb3QgQ2VydGlmaWNhdGUgQXV0aG9yaXR5
// SIG // MB4XDTEwMDgzMTIyMTkzMloXDTIwMDgzMTIyMjkzMlow
// SIG // eTELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0
// SIG // b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1p
// SIG // Y3Jvc29mdCBDb3Jwb3JhdGlvbjEjMCEGA1UEAxMaTWlj
// SIG // cm9zb2Z0IENvZGUgU2lnbmluZyBQQ0EwggEiMA0GCSqG
// SIG // SIb3DQEBAQUAA4IBDwAwggEKAoIBAQCycllcGTBkvx2a
// SIG // YCAgQpl2U2w+G9ZvzMvx6mv+lxYQ4N86dIMaty+gMuz/
// SIG // 3sJCTiPVcgDbNVcKicquIEn08GisTUuNpb15S3GbRwfa
// SIG // /SXfnXWIz6pzRH/XgdvzvfI2pMlcRdyvrT3gKGiXGqel
// SIG // cnNW8ReU5P01lHKg1nZfHndFg4U4FtBzWwW6Z1KNpbJp
// SIG // L9oZC/6SdCnidi9U3RQwWfjSjWL9y8lfRjFQuScT5EAw
// SIG // z3IpECgixzdOPaAyPZDNoTgGhVxOVoIoKgUyt0vXT2Pn
// SIG // 0i1i8UU956wIAPZGoZ7RW4wmU+h6qkryRs83PDietHdc
// SIG // pReejcsRj1Y8wawJXwPTAgMBAAGjggFeMIIBWjAPBgNV
// SIG // HRMBAf8EBTADAQH/MB0GA1UdDgQWBBTLEejK0rQWWAHJ
// SIG // Ny4zFha5TJoKHzALBgNVHQ8EBAMCAYYwEgYJKwYBBAGC
// SIG // NxUBBAUCAwEAATAjBgkrBgEEAYI3FQIEFgQU/dExTtMm
// SIG // ipXhmGA7qDFvpjy82C0wGQYJKwYBBAGCNxQCBAweCgBT
// SIG // AHUAYgBDAEEwHwYDVR0jBBgwFoAUDqyCYEBWJ5flJRP8
// SIG // KuEKU5VZ5KQwUAYDVR0fBEkwRzBFoEOgQYY/aHR0cDov
// SIG // L2NybC5taWNyb3NvZnQuY29tL3BraS9jcmwvcHJvZHVj
// SIG // dHMvbWljcm9zb2Z0cm9vdGNlcnQuY3JsMFQGCCsGAQUF
// SIG // BwEBBEgwRjBEBggrBgEFBQcwAoY4aHR0cDovL3d3dy5t
// SIG // aWNyb3NvZnQuY29tL3BraS9jZXJ0cy9NaWNyb3NvZnRS
// SIG // b290Q2VydC5jcnQwDQYJKoZIhvcNAQEFBQADggIBAFk5
// SIG // Pn8mRq/rb0CxMrVq6w4vbqhJ9+tfde1MOy3XQ60L/svp
// SIG // LTGjI8x8UJiAIV2sPS9MuqKoVpzjcLu4tPh5tUly9z7q
// SIG // QX/K4QwXaculnCAt+gtQxFbNLeNK0rxw56gNogOlVuC4
// SIG // iktX8pVCnPHz7+7jhh80PLhWmvBTI4UqpIIck+KUBx3y
// SIG // 4k74jKHK6BOlkU7IG9KPcpUqcW2bGvgc8FPWZ8wi/1wd
// SIG // zaKMvSeyeWNWRKJRzfnpo1hW3ZsCRUQvX/TartSCMm78
// SIG // pJUT5Otp56miLL7IKxAOZY6Z2/Wi+hImCWU4lPF6H0q7
// SIG // 0eFW6NB4lhhcyTUWX92THUmOLb6tNEQc7hAVGgBd3TVb
// SIG // Ic6YxwnuhQ6MT20OE049fClInHLR82zKwexwo1eSV32U
// SIG // jaAbSANa98+jZwp0pTbtLS8XyOZyNxL0b7E8Z4L5UrKN
// SIG // MxZlHg6K3RDeZPRvzkbU0xfpecQEtNP7LN8fip6sCvsT
// SIG // J0Ct5PnhqX9GuwdgR2VgQE6wQuxO7bN2edgKNAltHIAx
// SIG // H+IOVN3lofvlRxCtZJj/UBYufL8FIXrilUEnacOTj5XJ
// SIG // jdibIa4NXJzwoq6GaIMMai27dmsAHZat8hZ79haDJLmI
// SIG // z2qoRzEvmtzjcT3XAH5iR9HOiMm4GPoOco3Boz2vAkBq
// SIG // /2mbluIQqBC0N1AI1sM9MIIGBzCCA++gAwIBAgIKYRZo
// SIG // NAAAAAAAHDANBgkqhkiG9w0BAQUFADBfMRMwEQYKCZIm
// SIG // iZPyLGQBGRYDY29tMRkwFwYKCZImiZPyLGQBGRYJbWlj
// SIG // cm9zb2Z0MS0wKwYDVQQDEyRNaWNyb3NvZnQgUm9vdCBD
// SIG // ZXJ0aWZpY2F0ZSBBdXRob3JpdHkwHhcNMDcwNDAzMTI1
// SIG // MzA5WhcNMjEwNDAzMTMwMzA5WjB3MQswCQYDVQQGEwJV
// SIG // UzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMH
// SIG // UmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBv
// SIG // cmF0aW9uMSEwHwYDVQQDExhNaWNyb3NvZnQgVGltZS1T
// SIG // dGFtcCBQQ0EwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAw
// SIG // ggEKAoIBAQCfoWyx39tIkip8ay4Z4b3i48WZUSNQrc7d
// SIG // GE4kD+7Rp9FMrXQwIBHrB9VUlRVJlBtCkq6YXDAm2gBr
// SIG // 6Hu97IkHD/cOBJjwicwfyzMkh53y9GccLPx754gd6udO
// SIG // o6HBI1PKjfpFzwnQXq/QsEIEovmmbJNn1yjcRlOwhtDl
// SIG // KEYuJ6yGT1VSDOQDLPtqkJAwbofzWTCd+n7Wl7PoIZd+
// SIG // +NIT8wi3U21StEWQn0gASkdmEScpZqiX5NMGgUqi+YSn
// SIG // EUcUCYKfhO1VeP4Bmh1QCIUAEDBG7bfeI0a7xC1Un68e
// SIG // eEExd8yb3zuDk6FhArUdDbH895uyAc4iS1T/+QXDwiAL
// SIG // AgMBAAGjggGrMIIBpzAPBgNVHRMBAf8EBTADAQH/MB0G
// SIG // A1UdDgQWBBQjNPjZUkZwCu1A+3b7syuwwzWzDzALBgNV
// SIG // HQ8EBAMCAYYwEAYJKwYBBAGCNxUBBAMCAQAwgZgGA1Ud
// SIG // IwSBkDCBjYAUDqyCYEBWJ5flJRP8KuEKU5VZ5KShY6Rh
// SIG // MF8xEzARBgoJkiaJk/IsZAEZFgNjb20xGTAXBgoJkiaJ
// SIG // k/IsZAEZFgltaWNyb3NvZnQxLTArBgNVBAMTJE1pY3Jv
// SIG // c29mdCBSb290IENlcnRpZmljYXRlIEF1dGhvcml0eYIQ
// SIG // ea0WoUqgpa1Mc1j0BxMuZTBQBgNVHR8ESTBHMEWgQ6BB
// SIG // hj9odHRwOi8vY3JsLm1pY3Jvc29mdC5jb20vcGtpL2Ny
// SIG // bC9wcm9kdWN0cy9taWNyb3NvZnRyb290Y2VydC5jcmww
// SIG // VAYIKwYBBQUHAQEESDBGMEQGCCsGAQUFBzAChjhodHRw
// SIG // Oi8vd3d3Lm1pY3Jvc29mdC5jb20vcGtpL2NlcnRzL01p
// SIG // Y3Jvc29mdFJvb3RDZXJ0LmNydDATBgNVHSUEDDAKBggr
// SIG // BgEFBQcDCDANBgkqhkiG9w0BAQUFAAOCAgEAEJeKw1wD
// SIG // RDbd6bStd9vOeVFNAbEudHFbbQwTq86+e4+4LtQSooxt
// SIG // YrhXAstOIBNQmd16QOJXu69YmhzhHQGGrLt48ovQ7DsB
// SIG // 7uK+jwoFyI1I4vBTFd1Pq5Lk541q1YDB5pTyBi+FA+mR
// SIG // KiQicPv2/OR4mS4N9wficLwYTp2OawpylbihOZxnLcVR
// SIG // DupiXD8WmIsgP+IHGjL5zDFKdjE9K3ILyOpwPf+FChPf
// SIG // wgphjvDXuBfrTot/xTUrXqO/67x9C0J71FNyIe4wyrt4
// SIG // ZVxbARcKFA7S2hSY9Ty5ZlizLS/n+YWGzFFW6J1wlGys
// SIG // OUzU9nm/qhh6YinvopspNAZ3GmLJPR5tH4LwC8csu89D
// SIG // s+X57H2146SodDW4TsVxIxImdgs8UoxxWkZDFLyzs7BN
// SIG // Z8ifQv+AeSGAnhUwZuhCEl4ayJ4iIdBD6Svpu/RIzCzU
// SIG // 2DKATCYqSCRfWupW76bemZ3KOm+9gSd0BhHudiG/m4LB
// SIG // J1S2sWo9iaF2YbRuoROmv6pH8BJv/YoybLL+31HIjCPJ
// SIG // Zr2dHYcSZAI9La9Zj7jkIeW1sMpjtHhUBdRBLlCslLCl
// SIG // eKuzoJZ1GtmShxN1Ii8yqAhuoFuMJb+g74TKIdbrHk/J
// SIG // mu5J4PcBZW+JC33Iacjmbuqnl84xKf8OxVtc2E0bodj6
// SIG // L54/LlUWa8kTo/0xggSJMIIEhQIBATCBkDB5MQswCQYD
// SIG // VQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4G
// SIG // A1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0
// SIG // IENvcnBvcmF0aW9uMSMwIQYDVQQDExpNaWNyb3NvZnQg
// SIG // Q29kZSBTaWduaW5nIFBDQQITMwAAALARrwqL0Duf3QAB
// SIG // AAAAsDAJBgUrDgMCGgUAoIGiMBkGCSqGSIb3DQEJAzEM
// SIG // BgorBgEEAYI3AgEEMBwGCisGAQQBgjcCAQsxDjAMBgor
// SIG // BgEEAYI3AgEVMCMGCSqGSIb3DQEJBDEWBBRUynaOkFYx
// SIG // qscGxrKzMZKrORiB2TBCBgorBgEEAYI3AgEMMTQwMqAS
// SIG // gBAAbwByAG0AbQBhAC4AagBzoRyAGmh0dHA6Ly93d3cu
// SIG // bWljcm9zb2Z0LmNvbS8gMA0GCSqGSIb3DQEBAQUABIIB
// SIG // AFNskA1qHBm5sMtNaU1TaiYeXlOqPuQEkvLplOEsWjNA
// SIG // Fpq0cHS6wPh3zy/qKLOhTKn04lUljtp/VWTS/ZUXbhHl
// SIG // omfDzybiNxE/tGlpERgVO3iqQ0Q4SAY7EJWweMusd1Fv
// SIG // 30IpbUL5L+s+M+MYGPx9MLj28SHgVdGXpXnHDCVcxZiC
// SIG // svJxXPNoJ7mxQEzpNg3QIBeCvDlGQQjxzi8wiRLxvNv5
// SIG // 7IX2FLVLRzsncG5QgY47OecGDFcj6Kziw2gCQi1CPc7O
// SIG // guy5cp/vgCuqYCJIzGXZlPHvpevXfnMw26VENHZ9jgg7
// SIG // XTyU97EoyyXywas91Qei1qhhjs0pIT+qSFuhggIoMIIC
// SIG // JAYJKoZIhvcNAQkGMYICFTCCAhECAQEwgY4wdzELMAkG
// SIG // A1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAO
// SIG // BgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29m
// SIG // dCBDb3Jwb3JhdGlvbjEhMB8GA1UEAxMYTWljcm9zb2Z0
// SIG // IFRpbWUtU3RhbXAgUENBAhMzAAAAKzkySMGyyUjzAAAA
// SIG // AAArMAkGBSsOAwIaBQCgXTAYBgkqhkiG9w0BCQMxCwYJ
// SIG // KoZIhvcNAQcBMBwGCSqGSIb3DQEJBTEPFw0xMzA5MTMx
// SIG // ODQ3NTdaMCMGCSqGSIb3DQEJBDEWBBQ4NqmUoVkMlfPh
// SIG // FMtMTS1wz3sZ6DANBgkqhkiG9w0BAQUFAASCAQA2v+aA
// SIG // u1sD3TFdmTq4w1/uWbSFzNhHHrrEtQe4FbpUaCf35Que
// SIG // wtTAw1OseM5NO3Kp/2S12imVNVXPrO6V7T0gd5/3Y+1t
// SIG // 1IhI+5vF3DxOOc1IvJpCIGpJJakLgsJYYLZrIcuZ9x3n
// SIG // OyJ0JGhccOHuRlEV6ES+89nGSliG8we+9EeGRs1CNY+x
// SIG // 6wexJiC8akX0JVhV0rLBL/CY9lGdFS/eH5haTOXw3FrW
// SIG // SJ+wDlnZ6NacM70ExcHPiMqpZD1U1eZOUtxWpWKReWCO
// SIG // yFzL5UNg8Q1O1Bs7sAq97pr0sOM36ZKDq5fRYbgQ5cWt
// SIG // jlachYikkMPXvaTV7vU3nAVv2r7U
// SIG // End signature block
