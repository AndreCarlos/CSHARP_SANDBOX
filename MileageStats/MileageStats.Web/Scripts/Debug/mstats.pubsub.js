//===================================================================================
// Microsoft patterns & practices
// Silk : Web Client Guidance
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
/*jslint onevar: true, undef: true, newcap: true, regexp: true, plusplus: true, bitwise: true, devel: true, maxerr: 50 */
/*global jQuery, setInterval, clearInterval */

(function (mstats, $) {
    /**
    * PubSub is the messaging system used by 
    * the widgets to listen for and fire events
    */
    mstats.pubsub = (function () {
        var queue = [],
            that = {};

        /**
        * Executes all callbacks associated with eventName using the 
        * context provided in the subscription.
        *
        * @param {string} eventName  The name of the event to publish
        *                            Uses a dot notation for consistency
        * @param {object} data       Any data to be passed to the event
        *                            handler. Custom to the event.
        */

        that.publish = function (eventName, data) {
            var context, intervalId, idx = 0;
            if (queue[eventName]) {
                intervalId = setInterval(function () {
                    if (queue[eventName][idx]) {
                        try {
                            context = queue[eventName][idx].context || this;
                            queue[eventName][idx].callback.call(context, data);
                        } catch (e) {
                            // log the message for developers
                            mstats.log('An error occurred in one of the callbacks for the event "' + eventName + '"');
                            mstats.log('The error was: "' + e + '"');
                        }

                        idx += 1;
                    } else {
                        clearInterval(intervalId);
                    }
                }, 0);

            }
        };
        /**
        * Stores an event subscription. Subsequent event subscriptions
        * are always added (not overwritten). Use unsubscribe to remove
        * event subscriptions.
        *
        * @param {string} eventName  The name of the event to publish
        *                            Uses a dot notation for consistency
        * @param {function} callback The function to be called when the 
        *                            event is published.
        * @param {object} context    The context to execute the callback
        */
        that.subscribe = function (eventName, callback, context) {
            if (!queue[eventName]) {
                queue[eventName] = [];
            }
            queue[eventName].push({
                callback: callback,
                context: context
            });
        };
        /**
        * Removes an event subscription.
        *
        * @param {string} eventName  The name of the event to remove
        * @param {function} callback The function associated witht the
        *                            event. Used to ensure the correct
        *                            event is being removed.
        * @param {object} context    The context associated with the 
        *                            callback. Used to ensure the correct
        *                            event is being removed.
        */
        that.unsubscribe = function (eventName, callback, context) {
            if (queue[eventName]) {
                queue[eventName].pop({
                    callback: callback,
                    context: context
                });
            }
        };

        return that;
    } ());

} (this.mstats = this.mstats || {}, jQuery));
