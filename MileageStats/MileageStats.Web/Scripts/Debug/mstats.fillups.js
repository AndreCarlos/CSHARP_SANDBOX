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
    $.widget('mstats.fillups', {
        // default options
        options: {
            // Default to $.ajax when sendRequest is undefined.
            // The extra function indirection allows $.ajax substitution because 
            // the widget framework is using the real $.ajax during options initialization.
            sendRequest: function (ajaxOptions) { $.ajax(ajaxOptions); },

            // option providing the event publisher used for communicating with the status widget.
            publish: function () { mstats.log('The publish option on fillups has not been set'); },

            // invalidateData allows the vehicleList to invalidate data
            // stored in the data cache.  
            invalidateData: function () { mstats.log('no data invalidation function provided.'); },

            selectedVehicleId: 0,
            selectedFillupId: 0,
            templateId: ''
        },
            
        data: null,

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
                if ($(this).data('action') === 'select-fillup') {
                    that._setOption('selectedFillupId', $(this).data('fillup-id'));
                    event.preventDefault();
                }
            });
        },

        // Gets the fillup data (via the sendRequest option) from the dataUrl option endpoint
        // This method also applies the data to the template provided in option.tempalteId
        _getFillupData: function () {
            var that = this;
            this._hideFillups();
            this.options.sendRequest({
                url: this.options.dataUrl + '/' + this.options.selectedVehicleId,
                success: function (data) {
                    if (!$(that.options.templateId).length) {
                        that._showFillups();
                        mstats.log('Fillups: Cannot apply templates as there is no template defined.');
                        return;
                    }

                    that.data = data;
                    that._updateSelectedFillup();
                    that._applyTemplate();

                    // now animate them into view
                    that._showFillups();
                },
                error: function () {
                    that._hideFillups();
                    that._showErrorMessage();
                }
            });
        },

        _applyTemplate: function () {
            var options = this.options,
                $template = $(options.templateId);
            
            if ($template.length && this.data) {
                this.element.html($template.tmpl(this.data));
            }
        },

        _showFillups: function () {
            this.element.find('.content').show();
        },

        _hideFillups: function () {
            this.element.find('.content').hide();
        },

        _updateSelectedFillup: function () {
            var i, 
                fillup,
                data = this.data,
                fillups = data.Fillups,
                selectedFillupId = this.options.selectedFillupId;
            
            if (fillups && fillups.length) {
                if (selectedFillupId > 0) {
                    data.SelectedFillup = fillups[0];
                    for (i = 0; i < fillups.length; i += 1) {
                        fillup = fillups[i];
                        if (fillup.FillupEntryId === selectedFillupId) {
                            data.SelectedFillup = fillup;
                            break;
                        }
                    }
                } else {
                    data.SelectedFillup = fillups[0];
                }
            }
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
                case 'selectedFillupId':
                    this._updateSelectedFillup();
                    this._applyTemplate();
                    break;
            }
        },

        refreshData: function () {
            this._getFillupData();
        },

        requeryData: function () {
            this.options.invalidateData(this.options.dataUrl);
            this.refreshData();
        }
    });

} (this.mstats, jQuery));
