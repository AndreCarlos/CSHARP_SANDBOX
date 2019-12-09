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
/*jslint onevar: true, undef: true, newcap: true, regexp: true, 
plusplus: true, bitwise: true, devel: true, maxerr: 50 */
/*global window: true, jQuery:true */

(function (mstats, $) {

    $.widget('mstats.registration', {

        options: {
            // Default to $.ajax when sendRequest is undefined.
            // The extra function indirection allows $.ajax substitution because 
            // the widget framework is using the real $.ajax during options initialization.
            sendRequest: function (ajaxOptions) { $.ajax(ajaxOptions); },

            // option providing the event publisher used for communicating with 
            // the status widget.
            publish: function () { mstats.log('The publish option on registration has not been set'); },
            
            displayNameChanged: null
        },

        _create: function () {
            this._bindNavigation();
        },

        _bindNavigation: function () {
            var that = this;

            this.element.delegate('form', 'submit.registration', function (event) {
                that.saveProfile();
                event.preventDefault();
            });
        },

        saveProfile: function () {
            var that = this,
                elem = this.element,
                formData = {
                    UserId: elem.find('#UserId').val(),
                    AuthorizationId: elem.find('#AuthorizationId').val(),
                    DisplayName: elem.find('#DisplayName').val(),
                    Country: elem.find('#Country').val(),
                    PostalCode: elem.find('#PostalCode').val()
                };

            this._showSavingMessage();
            
            this.options.sendRequest({
                url: this.options.dataUrl,
                data: formData,
                cache: false,
                success: function () {
                    that._startHidingWidget();
                    that._showSavedMessage();
                    // we update the username after successfully the updating the profile
                    that._trigger('displayNameChanged', null, { displayName: formData.DisplayName } );
                },
                error: function () {
                    that._showSavingErrorMessage();
                }
            });
        },

        /********************************************************
        * Status Methods               
        ********************************************************/
        _publishStatus: function (status) {
            this.options.publish(mstats.events.status, status);
        },

        // show the status widget in the saving state
        _showSavingMessage: function () {
            this._publishStatus({
                type: 'saving',
                message: 'Saving registration information...',
                duration: 5000
            });
        },

        // show the status widget in the saved state
        _showSavedMessage: function () {
            this._publishStatus({
                type: 'saved',
                message: 'Registration saved',
                duration: 5000
            });
        },

        // show the status widget in the error state after a failed save
        _showSavingErrorMessage: function () {
            this._publishStatus({
                type: 'saveError',
                message: 'An error occurred while saving your registration. ' +
                         'Please try again.',
                duration: 5000
            });
        },

        // hide the form in the widget
        _startHidingWidget: function () {
            this.element
                .find('#registration-content')
                    .slideUp('slow')
                    .end()
                .delay(2000)
                .slideUp('slow');
        }
    });

} (this.mstats, jQuery));