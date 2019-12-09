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
/*global jQuery, setTimeout, clearTimeout */
(function (mstats, $) {
    
    var priorities = {
        saveError: 1,
        saved: 2,
        saving: 3,
        loadError: 4
    };

    $.widget('mstats.status', {
        options: {
            duration: 3000,
            subscribe: function () { mstats.log('The subscribe option on status has not been set'); }
        },

        _create: function () {
            // handle global status events
            this.options.subscribe(mstats.events.status, this._statusSubscription, this);
        },

        currentStatus: null,

        _statusSubscription: function (status) {
            var that = this,
                current = this.currentStatus;

            status.priority = this._getPriority(status);

            // cancel displaying the current message if its priority is lower than
            // the new message. (the lower the int the higher priority)
            if (current && (status.priority < current.priority)) {
                clearTimeout(current.timer);
            }

            current = status;

            this.element.text(status.message).show();

            // set the message for the duration
            current.timer = setTimeout(function () {
                that.element.fadeOut();
                that.currentStatus = null;
            }, status.duration || this.options.duration);
        },

        _getPriority: function (status) {
            return priorities[status.type];
        },

        destroy: function () {
            if (this.currentStatus) {
                clearTimeout(this.currentStatus.timer);
            }
            $.Widget.prototype.destroy.call(this);
        }

    });

} (this.mstats = this.mstats || {}, jQuery));