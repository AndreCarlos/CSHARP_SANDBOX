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
    $.widget('mstats.statisticsPane', {

        options: {
            // Default to $.ajax when sendRequest is undefined.
            // The extra function indirection allows $.ajax substitution because 
            // the widget framework is using the real $.ajax during options initialization.
            sendRequest: function (ajaxOptions) { $.ajax(ajaxOptions); },

            // invalidateData allows the vehicleList to invalidate data
            // stored in the data cache.  
            invalidateData: function () { mstats.log('The invalidateData option on statisticsPane has not been set'); },

            // option providing the event publisher used for communicating with the status widget.
            publish: function () { mstats.log('The publish option on summaryPane has not been set'); }

        },

        _create: function () {
            this._getStatisticsData();
        },

        _applyTemplate: function (data) {
            var options = this.options,
                $template = $(options.templateId);

            if (!$template.length) {
                mstats.log('Cannot apply templates as there is no template defined.');
                return;
            }

            this.element.find('#statistics-content')
                .html($template.tmpl(data));
        },

        _getStatisticsData: function () {
            var that = this;
            
            this.options.sendRequest({
                url: this.options.dataUrl,
                success: function (data) {
                    that._applyTemplate(data);
                    that._showStatistics();
                },

                error: function () {
                    that._hideStatistics();
                    that._showErrorMessage();
                }
            });
        },

        // Show the statistics pane 
        _showStatistics: function () {
            this.element.find('#statistics-content').show();
        },

        // hide the statistics pane 
        _hideStatistics: function () {
            this.element.find('#statistics-content').hide();
        },

        // show the status widget in the error state
        _showErrorMessage: function () {
            this.options.publish(mstats.events.status, {
                type: 'loadingError',
                message: 'An error occurred while loading your overall statistics.  Please try again.',
                duration: 10000
            });
        },

        refreshData: function () {
            this._getStatisticsData();
        },

        requeryData: function () {
            this.options.invalidateData(this.options.dataUrl);
            this.refreshData();
        }
    });

} (this.mstats, jQuery));