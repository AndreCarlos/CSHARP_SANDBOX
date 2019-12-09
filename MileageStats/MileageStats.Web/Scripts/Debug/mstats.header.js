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

    $.widget('mstats.header', {

        options: {
            title: '',
            displayName: ''
        },
            
        _create: function () {
            this._adjustNavigation();
        },

        _adjustNavigation: function () {
            var state = $.bbq.getState() || {  },
                url = this.element.data('url'),
                newUrlBase = mstats.getRelativeEndpointUrl(url);

            state.layout = 'dashboard';
            this.element.find('#dashboard-link').attr('href', $.param.fragment(newUrlBase, state));

            state.layout = 'charts';
            this.element.find('#charts-link').attr('href', $.param.fragment(newUrlBase, state));
        },

        _setOption: function (key, value) {
            switch (key) {
                case 'title':
                    this.element.find('[data-title]').text(value);
                    break;
                case 'displayName':
                    this.element.find('[data-display-name]').text(value);
                    break;
            }
            $.Widget.prototype._setOption.apply(this, arguments);
        }
    });

}(this.mstats, jQuery));