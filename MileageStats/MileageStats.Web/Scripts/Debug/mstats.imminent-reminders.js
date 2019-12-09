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
/*global jQuery */
(function (mstats, $) {
    $.widget('mstats.imminentRemindersPane', {
        options: {
            // Default to $.ajax when sendRequest is undefined.
            // The extra function indirection allows $.ajax substitution because 
            // the widget framework is using the real $.ajax during options initialization.
            sendRequest: function (ajaxOptions) { $.ajax(ajaxOptions); },

            // invalidateData allows the imminentReminders to invalidate data
            // stored in the data cache.  
            invalidateData: function () { mstats.log('The invalidateData option on imminentReminders has not been set'); },

            // option providing the event publisher used for communicating with the status widget.
            publish: function() { mstats.log('The publish option on imminentReminders has not been set'); }
        },

        _create: function () {
            this._getImminentReminderData();
        },

        _applyTemplate: function (data) {
            if (!$(this.options.templateId).length) {
                mstats.log('Cannot apply templates as there is no template defined.');
                return;
            }

            // Wrapped to make it easier to template with header data.
            var wrappedData = { ReminderList: data };
            this.element.find('#summary-reminders-content')
                .html($(this.options.templateId).tmpl(wrappedData, {
                    createRemindersLink: function (vehicleId) {
                        var state = $.bbq.getState() || {},
                            newUrlBase = mstats.getBaseUrl();
                        state.layout = 'reminders';
                        state.vid = vehicleId;

                        return $.param.fragment(newUrlBase, state);
                    }
                }));
        },

        _getImminentReminderData: function () {
            var that = this;
            this._hideReminders();

            this.options.sendRequest({
                url: this.options.dataUrl,
                success: function (data) {

                    that._applyTemplate(data);
                    that._showReminders();

                    // ensure that setOption is called with the provided options
                    that.option(that.options);
                },
                error: function () {
                    that._hideReminders();
                    that._showErrorMessage();
                }
            });
        },

        _showReminders: function () {
            this.element.find('#summary-reminders-content').show();
        },

        _hideReminders: function () {
            this.element.find('#summary-reminders-content').hide();
        },

        refreshData: function () {
            this._getImminentReminderData();
        },

        requeryData: function () {
            this.options.invalidateData(this.options.dataUrl);
            this.refreshData();
        },

        /********************************************************
        * Status Methods               
        ********************************************************/
        _publishStatus: function (status) {
            this.options.publish(mstats.events.status, status);
        },

        // hide the vehicles list and show the status widget in the error state
        _showErrorMessage: function () {
            this._publishStatus({
                type: 'loadError',
                message: 'An error occurred while loading the summary reminders data.  Please try again.',
                duration: 10000
            });
        }
    });

}(this.mstats, jQuery));
