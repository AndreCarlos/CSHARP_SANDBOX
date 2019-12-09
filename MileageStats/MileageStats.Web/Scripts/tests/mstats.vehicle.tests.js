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
(function ($) {

    module('Vehicle widget tests', {
        setup: function () {
            $('#qunit-fixture').append('<div id="vehicles" class="article">' +
                '<div id="vehicle-list-content">' +
                '<div class="wrapper">' +
                    '<div class="vehicle" data-vehicle-id="4">' +
                        '<div class="content">' +
                            '<a data-action="vehicle-details-selected" href="/Vehicle/Details/4">' +
                                '<div class="header">' +
                                    '<div class="overlay"></div>' +
                                    '<div class="data-model">' +
                                        '<span class="year">2002</span>' +
                                        '<span class="make">Wrangler</span>' +
                                        '<span class="model">Jeep</span>' +
                                    '</div>' +
                                    '<div class="data-name" data-field="vehicle-name">Mud Lover</div>' +
                                    '<div class="glass"></div>' +
                                '</div>' +
                            '</a>' +
                            '<div class="actions">' +
                                '<a class="avatar" data-action="vehicle-details-selected">' +
                                    '<img src="/Vehicle/Photo/" alt="Default Vehicle Photo" />' +
                                '</a>' +
                                '<div class="nav">' +
                                    '<a href="/Vehicle/Details/4" data-action="vehicle-details-selected" alt="details">' +
                                        '<div class="hover"></div>' +
                                        '<div class="active"></div>' +
                                        '<img alt="Details" src="@Url.Content("~/Content/command-details.png")" />' +
                                        '<div class="glass"></div>' +
                                    '</a>' +
                                    '<a href="/Fillup/List/4" data-action="vehicle-fillups-selected" alt="fillups">' +
                                        '<div class="hover"></div>' +
                                        '<div class="active"></div>' +
                                        '<img alt="Fill ups" src="@Url.Content("~/Content/command-fillups.png")" />' +
                                        '<div class="glass"></div>' +
                                    '</a>' +
                                    '<a href="/Reminder/List/4" data-action="vehicle-reminders-selected" alt="reminders">' +
                                        '<div class="hover"></div>' +
                                        '<div class="active"></div>' +
                                        '<img alt="Reminders" src="@Url.Content("~/Content/command-reminders.png")" />' +
                                        '<div class="glass"></div>' +
                                    '</a>' +
                                '</div>' +
                            '</div>' +
                            '<div class="statistics footer">' +
                                '<div class="statistic mile-per-gallon">' +
                                    '<div class="quantity">${mstats.makeMPGDisplay(Statistics.Last12Months.AverageFuelEfficiency)}</div>' +
                                    '<div class="unit">mpg</div>' +
                                '</div>' +
                                '<div class="statistic cost-per-mile">' +
                                    '<div class="quantity">${mstats.makeCostToDriveDisplay(Statistics.Last12Months.AverageCostToDrive)}&cent;</div>' +
                                    '<div class="unit">per mile</div>' +
                                '</div>' +
                                '<div class="statistic cost-per-month">' +
                                    '<div class="quantity">$${mstats.makeCostPerMonthDisplay(Statistics.Last12Months.AverageCostPerMonth)}</div>' +
                                    '<div class="unit">per month</div>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
                '</div>' +
                '</div>');
        },
        teardown: function () {
            $('.vehicle').vehicle('destroy');
        }
    });

    test('when widget is attached to the .mstats-vehicle element, then vehicle details link is changed to update the URL hash', function () {
        expect(3);

        $('.vehicle').vehicle();

        var link = $('[data-action=vehicle-details-selected]').attr('href'),
            rootUrl = link.substring(0, link.indexOf('#')),
            querystring = $.param.fragment(link),
            state = $.deparam.querystring(querystring),
            layoutHash = state.layout,
            vehicledIdHash = state.vid;

        equal(rootUrl, '/Dashboard', 'base url updated so no redirect occurs');
        equal(layoutHash, 'details', 'screen set properly');
        equal(vehicledIdHash, '4', 'vehicle Id set properly');
    });

    test('when widget is attached to the .mstats-vehicle element, then vehicle fillups link is changed to update the URL hash', function () {
        expect(3);

        $('.vehicle').vehicle();

        var link = $('[data-action=vehicle-fillups-selected]').attr('href'),
            rootUrl = link.substring(0, link.indexOf('#')),
            querystring = $.param.fragment(link),
            state = $.deparam.querystring(querystring),
            layoutHash = state.layout,
            vehicledIdHash = state.vid;

        equal(rootUrl, '/Dashboard', 'base url updated so no redirect occurs');
        equal(layoutHash, 'fillups', 'screen set properly');
        equal(vehicledIdHash, '4', 'vehicle Id set properly');
    });

    test('when widget is attached to the .mstats-vehicle element, then vehicle reminders link is changed to update the URL hash', function () {
        expect(3);

        $('.vehicle').vehicle();

        var link = $('[data-action=vehicle-reminders-selected]').attr('href'),
            rootUrl = link.substring(0, link.indexOf('#')),
            querystring = $.param.fragment(link),
            state = $.deparam.querystring(querystring),
            layoutHash = state.layout,
            vehicledIdHash = state.vid;

        equal(rootUrl, '/Dashboard', 'base url updated so no redirect occurs');
        equal(layoutHash, 'reminders', 'screen set properly');
        equal(vehicledIdHash, '4', 'vehicle Id set properly');
    });

    test('when collapse is called, then .statistics will have a height of zero', function () {
        expect(1);
        var v = $('.vehicle').vehicle();
        v.vehicle('collapse');
        setTimeout(function () {
            equal($('.statistics').height(), 0, 'the statistics class has a height of zero');
            start();
        }, 1100);
        stop();
    });

    test('when widget is initialized collapsed, then the statistics class has a height of zero', function () {
        expect(1);
        $('.vehicle').vehicle({'collapsed': true});
        setTimeout(function () {
            equal($('.statistics').height(), 0, 'the statistics class has a height of zero');
            start();
        }, 1100);
        stop();
    });

    test('when expand is called, then .statistics will have a height of 115px', function () {
        expect(1);
   
        var v = $('.vehicle').vehicle({'collapsed': true});

        setTimeout(function () {
            forceCompletionOfAllAnimations();
            start();
        }, 800);
        stop();

        v.vehicle('expand');
        setTimeout(function () {
            forceCompletionOfAllAnimations();
            equal($('.statistics').height(), 115, 'the statistics class has a height of 115px');
            start();
        }, 2000);
        stop();
    });

    test('when initialized, then the id property will be a positive number', function () {
        expect(1);
        var v = $('.vehicle').vehicle();
        equal(v.vehicle('option', 'id'), 4, 'the id property is being set on initialization');
    });

}(jQuery));