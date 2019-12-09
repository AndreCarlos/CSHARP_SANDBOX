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

/* Note: jsLint and Visual Studio disagree on switch and case statement indenting.  
 * We are using the Visual Studio indenting, so there are spacing errors when jsLint is run. 
 */

(function (mstats, $) {
    $.widget('mstats.reminders', {
        // default options
        options: {
            // Default to $.ajax when sendRequest is undefined.
            // The extra function indirection allows $.ajax substitution because 
            // the widget framework is using the real $.ajax during options initialization.
            sendRequest: function (ajaxOptions) { $.ajax(ajaxOptions); },

            // option providing the event publisher used for communicating with the status widget.
            publish: function () { mstats.log('The publish option for reminders has not been set'); },

            // invalidateData allows the vehicleList to invalidate data
            // stored in the data cache.  
            invalidateData: function () { mstats.log('The invalidateData option for reminders has not been set'); },

            selectedVehicleId: 0,
            selectedReminderId: 0,
            templateId: ''
        },
        
        data: {},
            
        // Creates the widget, taking over the UI contained in it, the links in it, 
        // and adding the necessary behaviors.
        _create: function () {
            this._bindNavigation();

            // ensure that setOption is called with the provided options
            // since the widget framework does not do this.
            this._setOptions(this.options);
        },

        _bindNavigation: function () {
            var that = this;
            
            this.element.delegate('[data-action]', 'click.' + this.name, function (event) {

                var $this = $(this),
                    action = $this.data('action'),
                    reminderId = $this.data('reminder-id');

                switch (action) {
                    case 'select-reminder':
                        that._setOption('selectedReminderId', reminderId);
                        event.preventDefault();
                        break;

                    case 'reminder-fulfill-selected':
                        that.fulfillReminder($this.data('action-url'));
                        event.preventDefault();
                        break;
                }
            });
        },

        _fulfillReminder: function (fulfillmentUrl) {
            var that = this;

            this._showFulfillingMessage();

            this.options.sendRequest({
                url: fulfillmentUrl,
                dataType: 'json',
                cache: false,
                success: function () {
                    that._showFulfilledMessage();
                    that.options.publish(mstats.events.vehicle.reminders.fulfilled, {});
                },
                error: function () {
                    that._showFulfillErrorMessage();
                }
            });
        },

        // Gets the Reminder data (via the sendRequest option) from the dataUrl option endpoint
        // This method also applies the data to the template provided in option.tempalteId
        _getReminderData: function () {
            var that = this;
            
            this._hideReminders();
            this.options.sendRequest({
                url: that._createRequestUrl(),
                success: function (data) {
                    if (!$(that.options.templateId).length) {
                        that._showReminders();
                        mstats.log('reminders: Cannot apply templates as there is no template defined.');
                        return;
                    }

                    that.data = data;
                    that._updateSelectedReminder();
                    that._applyTemplate();

                    // now animate them into view
                    that._showReminders();
                },
                error: function () {
                    that._hideReminders();
                    that._showErrorMessage();
                }
            });
        },

        _updateSelectedReminder: function () {
            var selectedReminderId = this.options.selectedReminderId,
                data = this.data,
                reminder,
                i;
            if (data.Reminders && data.Reminders.length) {
                if (selectedReminderId > 0) {
                    for (i = 0; i < data.Reminders.length; i += 1) {
                        reminder = data.Reminders[i];
                        if (reminder.ReminderId === selectedReminderId) {
                            data.SelectedReminder = reminder;
                            break;
                        }
                    }
                } else {
                    data.SelectedReminder = data.Reminders[0];
                    this._setOption('selectedReminderId', data.SelectedReminder.ReminderId);
                }
            }
        },

        _applyTemplate: function () {
            var options = this.options,
                $template = $(options.templateId);

            if ($template.length && this.data) {
                this.element.html($template.tmpl(this.data));
            }
        },

        _showReminders: function () {
            this.element.find('.content').show();
        },

        _hideReminders: function () {
            this.element.find('.content').hide();
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
                message: 'An error occurred while loading the requested data.  Please try again.',
                duration: 10000
            });
        },

        _showFulfillingMessage: function () {
            this._publishStatus({
                type: 'saving',
                message: 'Fulfilling the selected reminder ...',
                duration: 5000
            });
        },

        _showFulfilledMessage: function () {
            this._publishStatus({
                type: 'saved',
                message: 'Reminder Fulfilled.',
                duration: 5000
            });
        },

        _showFulfillErrorMessage: function () {
            this._publishStatus({
                type: 'saveError',
                message: 'An error occurred while fulfilling the selected reminder.  Please try again.',
                duration: 10000
            });
        },

        // handle setting options 
        _setOption: function (key, value) {
            $.Widget.prototype._setOption.apply(this, arguments);
            if (value <= 0) {
                return;
            }
            switch (key) {
                case 'selectedVehicleId':
                    this.refreshData();
                    break;
                case 'selectedReminderId':
                    this._updateSelectedReminder();
                    this._applyTemplate();
                    break;
            }
        },

        _createRequestUrl: function () {
            return this.options.dataUrl + '/' + this.options.selectedVehicleId;
        },

        fulfillReminder: function (fulfillmentUrl) {
            var shouldfulfill = confirm('Are you sure you want to fulfill this reminder?');
            if (shouldfulfill) {
                this._fulfillReminder(fulfillmentUrl);
            }
        },

        refreshData: function () {
            this._setOption('selectedReminderId', -1);
            this._setOption('data', {});
            this._getReminderData();
        },

        requeryData: function () {
            this.options.invalidateData(this._createRequestUrl());
            this.refreshData();
        }
    });

} (this.mstats, jQuery));
