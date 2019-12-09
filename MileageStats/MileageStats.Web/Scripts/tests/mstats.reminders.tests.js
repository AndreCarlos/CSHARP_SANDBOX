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
    module('MileageStats Vehicle Reminders Widget', {
        setup: function () {
            this.savedAjax = $.ajax;
            $('#qunit-fixture').append('<div id="reminders-pane" class="tab opened section">' +
                    '<a class="trigger" href="/Reminder/List/2"></a>' +
                    '<div class="content">' +
                        '<div class="header">' +
                            '<form action="/Reminder/Add/2" method="get">' +
                                '<button data-action="reminder-add-selected" class="button generic small" type="submit">' +
                                    '<img src="/Content/button-add.png" title="Add Reminder" alt="Add" />' +
                                '</button>' +
                            '</form>' +
                        '</div>' +
                        '<div class="aside">' +
                            '<div class="list nav">' +
                                '<a class="list-item selected overdue" href="/Reminder/Details/6">' +
                                    '<h1>Check Wiper Fluid</h1>' +
                                    '<p class="title">Due on 23 Mar 2011</p>' +
                                '</a>' +
                                '<a class="list-item  " href="/Reminder/Details/8">' +
                                    '<h1>Vacuum Car</h1>' +
                                    '<p class="title">Due on 17 Apr 2011</p>' +
                                    '</a>' +
                                '<a class="list-item  overdue" href="/Reminder/Details/5">' +
                                    '<h1>Oil Change</h1>' +
                                    '<p class="title">Due at 15268 miles</p>' +
                                '</a>' +
                            '</div>' +
                        '</div>' +
                        '<div class="display article">' +
                            '<div class="display-label">' +
                                '<label for="Title">Title</label>' +
                            '</div>' +
                            '<div class="display-field title wrap">Check Wiper Fluid</div>' +
                            '<div class="display-label"><label for="DueDate">Date Due</label></div>' +
                            '<div class="display-field due-date">23 Mar 2011</div>' +
                            '<div class="display-label"><label for="DueDistance">Distance Due</label></div>' +
                            '<div class="display-field due-distance">(not entered)</div>' +
                            '<div class="display-label"><label for="Remarks">Remarks</label></div>' +
                            '<div class="display-field remarks wrap">Check condition of the wipers</div>' +
                            '<div>' +
                                '<form action="/Reminder/Fulfill/6" method="post">' +
                                    '<button data-action="reminder-fulfill-selected" class="button generic small" type="submit">' +
                                        '<img src="/Content/button-fulfill.png" title="Fulfill Reminder" alt="Fulfill" />' +
                                    '</button>' +
                                '</form>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                    '</div>');
        },
        teardown: function () {
            $.ajax = this.savedAjax;
            $('#reminders-pane').reminders('destroy');
        }
    });

    /****************************************************************
    * Data Loading Tests
    ****************************************************************/
    test('while loading data, then the widget ensures the contents are hidden', function () {
        expect(1);
        var reminders = $('#reminders-pane').reminders();

        reminders.reminders( 'option', 'sendRequest', function (options) {
            ok($('.content:hidden').length > 0, 'contents are hidden');
            options.success({});
        });

        // force a data refresh
        reminders.reminders('option', 'selectedVehicleId', 1);
    });

    test('when loading data is complete, then the contents are shown', function () {
        expect(1);
        var reminders = $('#reminders-pane').reminders({
            sendRequest: function (options) { options.success({}); }
        });

        // force a data refresh
        reminders.reminders('option', 'selectedVehicleId', 1);

        forceCompletionOfAllAnimations();
        ok($('.content:hidden').length === 0, 'contents are shown');
    });

    test('when loading data errors out, then the widget ensures the contents are hidden', function () {
        expect(1);
        var reminders = $('#reminders-pane').reminders({            
            templateId: 'testTemplate'
        });

        reminders.reminders('option', 'sendRequest', function (options) {
            ok($('.content:hidden').length > 0, 'contents are hidden');
            options.error({});
        });
        // force a data refresh
        reminders.reminders('option', 'selectedVehicleId', 1);
    });

    test('when loading data errors out, then triggers error status', function () {
        expect(2);
        var eventType = 'loadError',
            reminders = $('#reminders-pane').reminders({
                templateId: '#testTemplate',
                sendRequest: function (options) { options.error({}); }                
            });
        
        reminders.reminders('option', 'publish', function (event, status) {
            if (status.type === eventType) {
                ok(status, 'status object passed to publisher');
                equal(status.type, eventType, 'status is of type : ' + eventType);
            }
        });

        // force a data refresh
        reminders.reminders('option', 'selectedVehicleId', 1);
    });

    test('when refreshData is called, then sendRequest is called', function () {
        expect(1);

        $('#reminders-pane').reminders({
            sendRequest: function (options) { options.success({}); }
        });

        $('#reminders-pane').reminders('option', 'sendRequest', function () {
            ok(true, 'sendRequest was called properly');
        });

        $('#reminders-pane').reminders('refreshData');
    });

    test('when refreshData is called, then cached data is not invalidated', function () {
        expect(0);

        $('#reminders-pane').reminders({
            sendRequest: function (options) { options.success({}); },
            invalidateData: function () {
                ok(false, 'invalidateData was called properly');
            }
        });

        $('#reminders-pane').reminders('refreshData');
    });

    test('when requeryData is called, then cached data is invalidated', function () {
        expect(1);

        $('#reminders-pane').reminders({
            sendRequest: function (options) { options.success({}); },
            invalidateData: function () {
                ok(true, 'invalidateData was called properly');
            }
        });

        $('#reminders-pane').reminders('requeryData');
    });

    test('when requeryData is called, then sendRequest is invoked', function () {
        expect(1);

        $('#reminders-pane').reminders({
            sendRequest: function (options) { options.success({}); },
            invalidateData: function () { }
        });

        $('#reminders-pane').reminders('option', 'sendRequest', function () {
            ok(true, 'sendRequest was called properly');
        });

        $('#reminders-pane').reminders('requeryData');
    });

    /****************************************************************
    * Data Templating Tests
    ****************************************************************/
    module('MileageStats reminders Widget Template Tests', {
        setup: function () {
            $('#qunit-fixture').append(
                '<div id="reminders-pane" class="tab opened section"><div class="content"/></div>' +
                    '<script id="testTemplate" type="text/x-jquery-tmpl"><p>contents go here</p></script>' +
                    '<script id="testTemplate2" type="text/x-jquery-tmpl"><p>other contents</p></script>'
            );
            this.savedAjax = $.ajax;
        },
        teardown: function () {
            $.ajax = this.savedAjax;
            $('#reminders-pane').reminders('destroy');
        }
    });

    test('when selected vehicle id is set and sendRequest not specified, then widget calls $.ajax', function () {
        expect(1);

        var reminders = $('#reminders-pane').reminders();

        $.ajax = function () {
            ok(true, '$.ajax was called');
        };

        // force a data refresh
        reminders.reminders('option', 'selectedVehicleId', 1);
    });

    test('when widget data is retrieved, then widget contents are replaced by the template', function () {
        expect(1);

        var reminders = $('#reminders-pane').reminders({
                sendRequest: function (options) { options.success({}); },
                templateId: '#testTemplate'
            });

        // force a data refresh
        reminders.reminders('option', 'selectedVehicleId', 1);

        equal($.trim($('#reminders-pane').html()), $.trim($('#testTemplate').html().toLowerCase()), 'Template applied');
    });

    test('when refreshData is called, then template is re-applied', function () {
        expect(2);

        var reminders = $('#reminders-pane').reminders({
                sendRequest: function (options) { options.success({}); },
                templateId: '#testTemplate'
            });

        // force a data refresh
        reminders.reminders('option', 'selectedVehicleId', 1);
        
        equal($.trim($('#reminders-pane').html()), $.trim($('#testTemplate').html().toLowerCase()), 'Template applied');

        $('#reminders-pane').reminders('option', 'templateId', '#testTemplate2');
        $('#reminders-pane').reminders('refreshData');

        equal($.trim($('#reminders-pane').html()), $.trim($('#testTemplate2').html().toLowerCase()), 'Template applied');
    });

}(jQuery));
