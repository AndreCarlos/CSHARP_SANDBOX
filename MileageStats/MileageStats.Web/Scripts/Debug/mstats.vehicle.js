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

    var shortAnimationLength = 600,
        longAnimationLength = 800;
    
    $.widget('mstats.vehicle', {

        options: {
            id: 0,
            collapsed: false
        },

        _create: function () {
            this._setOption('id', this.element.data('vehicle-id'));
            this._adjustNavigation();

            if (this.options.collapsed) {
                this._forceCollapse();
            }
        },

        _forceCollapse: function () {
            this.element
                .find('.mile-per-gallon')
                    .find('.quantity').hide().end()
                    .find('.unit').hide().end()
                    .end()
                .find('.cost-per-mile')
                    .find('.quantity').hide().end()
                    .find('.unit').hide().end()
                    .end()
                .find('.cost-per-month')
                    .find('.quantity').hide().end()
                    .find('.unit').hide().end()
                    .end()
                .find('.statistics')
                    .height(0)
                    .closest(':mstats-vehicle')
                        .addClass('compact');
        },

        // Take over link navigation in the widget to enable a
        // single page interface.  This will keep all links point to the dashboard
        // with the proper url hash set for the action.
        _adjustNavigation: function () {
            var that = this;
            
            this.element.find('[data-action]').each(function () {
                var $this = $(this),
                    action = $this.data('action'),
                    vehicleId = that.options.id,
                    state = $.bbq.getState() || {},
                    newUrlBase = mstats.getBaseUrl();

                state.vid = vehicleId;
                switch (action) {
                    case 'vehicle-details-selected':
                        state.layout = 'details';
                        break;
                    case 'vehicle-fillups-selected':
                        state.layout = 'fillups';
                        break;
                    case 'vehicle-reminders-selected':
                        state.layout = 'reminders';
                        break;
                    case 'vehicle-add-selected':
                        state.layout = 'addVehicle';
                        state.vid = undefined;
                        break;
                }
                $this.attr('href', $.param.fragment(newUrlBase, state));
            });
        },

        collapse: function () {
            if (this.options.collapsed) {
                return;
            }

            this.element
                .find('.mile-per-gallon')
                    .find('.quantity').fadeOut(shortAnimationLength).end()
                    .find('.unit').fadeOut(shortAnimationLength).end()
                    .end()
                .find('.cost-per-mile')
                    .find('.quantity').fadeOut(shortAnimationLength).end()
                    .find('.unit').fadeOut(shortAnimationLength).end()
                    .end()
                .find('.cost-per-month')
                    .find('.quantity').fadeOut(shortAnimationLength).end()
                    .find('.unit').fadeOut(shortAnimationLength).end()
                    .end()
                .find('.statistics')
                    .animate({
                        height: 0
                    }, {
                        duration: longAnimationLength,
                        easing: 'linear',
                        complete: function () {
                            $(this).closest(':mstats-vehicle')
                                .addClass('compact');
                        }
                    });

            this._setOption('collapsed', true);
        },

        expand: function () {
            if (!this.options.collapsed) {
                return;
            }

            this.element
                .removeClass('compact')
                .find('.statistics')
                    .animate(
                        { height: 115 },
                        { duration: longAnimationLength, easing: 'linear' }
                    )
                    .find('.mile-per-gallon')
                        .find('.quantity').fadeIn(shortAnimationLength).end()
                        .find('.unit').fadeIn(shortAnimationLength).end()
                        .end()
                    .find('.cost-per-mile')
                        .find('.quantity').fadeIn(shortAnimationLength).end()
                        .find('.unit').fadeIn(shortAnimationLength).end()
                        .end()
                    .find('.cost-per-month')
                        .find('.quantity').fadeIn(shortAnimationLength).end()
                        .find('.unit').fadeIn(shortAnimationLength).end()
                        .end();

            this._setOption('collapsed', false);
        }

    });
} (this.mstats, jQuery));
