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
    module('MileageStats Vehicle Details Widget', {
        setup: function () {
            this.savedAjax = $.ajax;
            $('#qunit-fixture').append('<div id="details-pane" class="tab opened section">' +
                '<a class="trigger" href="/Vehicle/Details/2"> </a>' +
                '<div class="content">' +
                        '<div class="header">' +
                            '<form action="/Vehicle/Edit/2" method="get">' +
                                '<button data-action="vehicle-edit-selected" class="button generic small" type="submit">' +
                                    '<img src="/Content/button-edit.png" title="Edit Vehicle" alt="Edit" />' +
                                '</button>' +
                            '</form>' +
                            '<form action="/Vehicle/Delete/2" method="post">' +
                                '<button data-action="vehicle-delete-selected" class="button generic small" type="submit">' +
                                    '<img src="/Content/button-delete.png" title="Delete Vehicle" alt="Delete" />' +
                                '</button>' +
                            '</form>' +
                        '</div>' +
                        '<div class="statistics aside">' +
                            '<div>' +
                                '<h1>Last 12 Months</h1>' +
                                '<div id="vehicle-charts">' +
                                    '<div class="display-label">Average Fuel Efficiency</div>' +
                                    '<div class="display-chart fuelEfficiencyChart">' +
                                        '<img src="/Vehicle/FuelEfficiencyChart/1/2" title="Fuel Efficiency Chart" alt="Fuel Efficiency Chart"/>' +
                                    '</div>' +
                                    '<div class="display-label">Total Distance Travelled</div>' +
                                    '<div class="display-chart distanceChart">' +
                                        '<img src="/Vehicle/TotalDistanceChart/1/2" title="Total Distances Chart" alt="Total Distances Chart"/>' +
                                    '</div>' +
                                    '<div class="display-label">Total Cost</div>' +
                                    '<div class="display-chart costChart">' +
                                        '<img src="/Vehicle/TotalCostChart/1/2" title="Total Cost Chart" alt="Total Cost Chart"/>' +
                                    '</div>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                        '<div class="display article">' +
                            '<div class="display-label">' +
                                    '<label for="Vehicle_Name">Vehicle Name</label>' +
                                '</div>' +
                                '<div class="display-field name wrap">Soccer Mom&#39;s Ride</div>' +
                                '<div class="display-label"><label for="Vehicle_Year">Model Year</label></div>' +
                                '<div class="display-field year">2010</div>' +
                                '<div class="display-label"><label for="Vehicle_MakeName">Make</label></div>' +
                                '<div class="display-field makeName">Honda</div>' +
                                '<div class="display-label"><label for="Vehicle_ModelName">Model</label></div>' +
                                '<div class="display-field modelName">Accord</div>' +
                                '<div class="display-label"><label for="Vehicle_Odometer">Odometer</label></div>' +
                                '<div class="display-field odometer">15950 miles</div>' +
                            '</div>' +
                            '<div class="article reminder-list">' +
                                '<div class="list nav">' +
                                        '<a class="list-item overdue" href="/Reminder/Details/6">' +
                                            '<h1>Check Wiper Fluid</h1>' +
                                            '<p class="title">Due 5 Apr 2011</p>' +
                                        '</a>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>');
        },
        teardown: function () {
            $.ajax = this.savedAjax;
            $('#details-pane').vehicleDetails('destroy');
        }
    });

    /****************************************************************
    * Data Loading Tests
    ****************************************************************/
    test('while loading data, then the widget ensures the contents are hidden', function () {
        expect(1);
        var details = $('#details-pane').vehicleDetails({
            sendRequest: function (options) {
                ok($('.content:hidden').length > 0, 'contents are hidden');
                options.success({});
            }
        });

        // force a data refresh
        details.vehicleDetails('option', 'selectedVehicleId', 1);
    });

    test('when loading data is complete, then the contents are shown', function () {
        expect(1);
        var details = $('#details-pane').vehicleDetails({
            sendRequest: function (options) { options.success({}); }
        });

        // force a data refresh
        details.vehicleDetails('option', 'selectedVehicleId', 1);

        setTimeout(function () { // this allows the animation to finish
            ok($('.content:hidden').length === 0, 'contents are shown');
            start();
        }, 2500);

        stop(3000);
    });

    test('when loading data errors out, then the widget ensures the contents are hidden', function () {
        expect(1);
        var details = $('#details-pane').vehicleDetails({
            sendRequest: function (options) {
                ok($('.content:hidden').length > 0, 'contents are hidden');
                options.error({});
            },
            templateId: 'testTemplate'
        });

        // force a data refresh
        details.vehicleDetails('option', 'selectedVehicleId', 1);
    });

    test('when loading data errors out, then triggers error status', function () {
        expect(2);
        var eventType = 'loadError',
            details = $('#details-pane').vehicleDetails({
                templateId: '#testTemplate',
                sendRequest: function (options) { options.error({}); },
                publish: function (event, status) {
                    if (status.type === eventType) {
                        ok(status, 'status object passed to publisher');
                        equal(status.type, eventType, 'status is of type : ' + eventType);
                    }
                }
            });

        // force a data refresh
        details.vehicleDetails('option', 'selectedVehicleId', 1);
    });

    test('when selected vehicle is set, then widget calls sendRequest with dataUrl and selected vehicle id', function () {
        expect(1);

        var vehicleDataUrl = '/GetVehicleData',
            mockGetData = function (options) {
                equal(options.url, vehicleDataUrl + '/1', 'Url was passed in');
            },
            details = $('#details-pane').vehicleDetails({
                sendRequest: mockGetData,
                dataUrl: vehicleDataUrl
            });

        // force a data refresh
        details.vehicleDetails('option', 'selectedVehicleId', 1);
    });

    test('when refreshData is called, then sendRequest is called', function () {
        expect(1);

        $('#details-pane').vehicleDetails({
            sendRequest: function (options) { options.success({}); }
        });

        $('#details-pane').vehicleDetails('option', 'sendRequest', function () {
            ok(true, 'sendRequest was called properly');
        });

        $('#details-pane').vehicleDetails('refreshData');
    });

    /****************************************************************
    * Delete Tests
    ****************************************************************/
    test('when deleting vehicle is complete, then triggers status', function () {
        expect(2);
        var eventType = 'saved',
            details = $('#details-pane').vehicleDetails({
                sendRequest: function (options) { options.success({}); },
                publish: function (event, status) {
                    if (event === mstats.events.status) {
                        if (status.type === eventType) {
                            ok(status, 'status object passed to publisher');
                            equal(status.type, eventType, 'status is of type : ' + eventType);
                        }
                    }
                }
            });

        confirm = function () { return true; };

        // force a data refresh
        details.vehicleDetails('option', 'selectedVehicleId', 1);
        details.vehicleDetails('deleteVehicle');

    });

    test('when deleting vehicle errors out, then triggers error status', function () {
        expect(2);
        var eventType = 'saveError',
            details = $('#details-pane').vehicleDetails({
                templateId: '#testTemplate',
                sendRequest: function (options) { options.error({}); },
                publish: function (event, status) {
                    if (status.type === eventType) {
                        ok(status, 'status object passed to publisher');
                        equal(status.type, eventType, 'status is of type : ' + eventType);
                    }
                }
            });

        confirm = function () { return true; };
        // force a data refresh
        details.vehicleDetails('option', 'selectedVehicleId', 1);
        details.vehicleDetails('deleteVehicle');
    });

    test('when delete clicked and confirmed, then calls confirm and sendRequest', function () {
        expect(2);

        var details = $('#details-pane').vehicleDetails({
            sendRequest: function (options) { 
                ok(true, 'sendRequest called');
                start(); },
            templateId: '#testTemplate'
        });

        confirm = function () {
            ok(true, 'confirm called');
            return true;
        };

        $('[data-action=vehicle-delete-selected]').first().click();
        stop(1500);
    });

    test('when delete clicked and cancelled, then calls confirm and does not call sendRequest', function () {
        expect(1);

        var details = $('#details-pane').vehicleDetails({
            templateId: '#testTemplate',
            sendRequest: function (options) {
                ok(false, 'sendRequest should not have been called');
            }
        });

        confirm = function () {
            ok(true, 'confirm called');
            return false;
        };

        $('[data-action=vehicle-delete-selected]').first().click();
    });

    test('when delete clicked and no sendRequest set, then calls $.ajax', function () {
        expect(2);
        $.ajax = function (options) {
            ok(true, '$.ajax called');
        };
        var details = $('#details-pane').vehicleDetails({
            templateId: '#testTemplate'
        });

        confirm = function () {
            ok(true, 'confirm called');
            return true;
        };

        $('[data-action=vehicle-delete-selected]').first().click();
    });

    test('when delete succeeds, then vehicle deleted event is triggered', function () {
        expect(2);
        var details = $('#details-pane').vehicleDetails({
            templateId: '#testTemplate',
            publish: function (e, status) {
                if (e === mstats.events.vehicle.deleted) {
                    ok(true, 'vehicle deleted event was fired');
                    start();
                }
            },
            sendRequest: function (options) { options.success({}); }
        });

        confirm = function () {
            ok(true, 'confirm called');
            return true;
        };

        $('[data-action=vehicle-delete-selected]').first().click();
        stop(1000);
    });

    /****************************************************************
    * Data Templating Tests
    ****************************************************************/
    module('MileageStats Vehicle Details Widget Template Tests', {
        setup: function () {
            $('#qunit-fixture').append(
                '<div id="details-pane" class="tab opened section"><div class="content"/></div>' +
                    '<script id="testTemplate" type="text/x-jquery-tmpl"><p>contents go here</p></script>' +
                    '<script id="testTemplate2" type="text/x-jquery-tmpl"><p>other contents</p></script>'
            );
            this.savedAjax = $.ajax;
        },
        teardown: function () {
            $.ajax = this.savedAjax;
            $('#details-pane').vehicleDetails('destroy');
        }
    });

    test('when selected vehicle id is set and sendRequest not specified, then widget calls $.ajax', function () {
        expect(1);

        $.ajax = function () {
            ok(true, '$.ajax was called');
        };

        var details = $('#details-pane').vehicleDetails();

        // force a data refresh
        details.vehicleDetails('option', 'selectedVehicleId', 1);
    });

    test('when selected vehicle is set, then widget calls sendRequest', function () {
        expect(1);

        var details = $('#details-pane').vehicleDetails({
            sendRequest: function (options) {
                ok(true, 'sendRequest was called');
            }
        });

        // force a data refresh
        details.vehicleDetails('option', 'selectedVehicleId', 1);
    });

    test('when widget data is retrieved, then widget contents are replaced by the template', function () {
        expect(1);

        var details = $('#details-pane').vehicleDetails({
            sendRequest: function (options) { options.success({}); },
            templateId: '#testTemplate',
            header: { 
                option: function () { return 'some-value' },
                header: function () { /* this is here to simulate header being a widget */ } 
                }
        });

        // force a data refresh
        details.vehicleDetails('option', 'selectedVehicleId', 1);

        equal($.trim($('#details-pane').html()), $.trim($('#testTemplate').html().toLowerCase()), 'Template applied');
    });

    test('when refreshData is called, then sendRequest is called', function () {
        expect(1);

        $('#details-pane').vehicleDetails({
            sendRequest: function (options) { options.success({}); }
        });

        $('#details-pane').vehicleDetails('option', 'sendRequest', function () {
            ok(true, 'sendRequest was called properly');
        });

        $('#details-pane').vehicleDetails('refreshData');
    });

    test('when refreshData is called, then cached data is not invalidated', function () {
        expect(0);

        $('#details-pane').vehicleDetails({
            sendRequest: function (options) { options.success({}); },
            invalidateData: function () {
                ok(false, 'invalidateData was called properly');
            }
        });

        $('#details-pane').vehicleDetails('refreshData');
    });

    test('when requeryData is called, then cached data is invalidated', function () {
        expect(1);

        $('#details-pane').vehicleDetails({
            sendRequest: function (options) { options.success({}); },
            invalidateData: function () {
                ok(true, 'invalidateData was called properly');
            }
        });

        $('#details-pane').vehicleDetails('requeryData');
    });

    test('when requeryData is called, then sendRequest is invoked', function () {
        expect(1);

        $('#details-pane').vehicleDetails({
            sendRequest: function (options) { options.success({}); },
            invalidateData: function () { }
        });

        $('#details-pane').vehicleDetails('option', 'sendRequest', function () {
            ok(true, 'sendRequest was called properly');
        });

        $('#details-pane').vehicleDetails('requeryData');
    });

    test('when refreshData is called, then template is re-applied', function () {
        expect(2);

        var details = $('#details-pane').vehicleDetails({
            sendRequest: function (options) { options.success({}); },
            templateId: '#testTemplate',
            header: { header: function () { /* simulate header being a widget */} }
        });

        // force a data refresh
        details.vehicleDetails('option', 'selectedVehicleId', 1);

        equal($.trim($('#details-pane').html()), $.trim($('#testTemplate').html().toLowerCase()), 'Template applied');

        $('#details-pane').vehicleDetails('option', 'templateId', '#testTemplate2');
        $('#details-pane').vehicleDetails('refreshData');

        equal($.trim($('#details-pane').html()), $.trim($('#testTemplate2').html().toLowerCase()), 'Template applied');
    });
} (jQuery));
