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
    module('MileageStats Imminent Reminders Pane Widget Tests', {
        setup: function () {
            this.savedAjax = $.ajax;
            $('#qunit-fixture').append(
                '<div class="section" id="reminders">' +
                    '<div id="summary-reminders-content">' +
                        '<h1>Reminders</h1>' +
                        '<div class="list nav">' +
                        '</div>' +
                    '</div>' +
                    '<script id="testTemplate" type="text/x-jquery-tmpl">' +
                        '<p>contents go here</p>' +
                    '</script>' +
                    '<script id="testTemplate2" type="text/x-jquery-tmpl">' +
                        '<p>other contents go here</p>' +
                    '</script>'
            );
        },

        teardown: function () {
            $.ajax = this.savedAjax;
        }
    });

    test('when created, then sendRequest is called', function () {
        expect(1);
        $('#reminders').imminentRemindersPane({
            sendRequest: function (options) {
                ok(true, 'sendRequest called');
            }
        });
    });

    test('when created and sendRequest not specified, then widget calls $.ajax', function () {
        expect(1);

        $.ajax = function () {
            ok(true, '$.ajax was called');
        };

        $('#reminders').imminentRemindersPane();
    });

    test('when sendRequest is called, then widget passes with dataUrl', function () {
        expect(1);

        $('#reminders').imminentRemindersPane({
            sendRequest: function (options) {
                equal(options.url, '/some/url', 'Url was passed in');
            },
            dataUrl: '/some/url'
        });

    });

    test('when widget data is retrieved, then widget contents are replaced by the template', function () {
        expect(1);
        $('#reminders').imminentRemindersPane({
            sendRequest: function (options) {
                options.success({});
            },
            templateId: '#testTemplate'
        });

        equal($('#summary-reminders-content').html(), $('#testTemplate').html(), 'Template applied');
    });

    test('when widget data is retrieved and no template specified, then widget contents are not replaced by the template', function () {
        expect(1);
        $('#reminders').imminentRemindersPane({
            sendRequest: function (options) {
                options.success({});
            }
        });

        notEqual($('#summary-reminders-content').html(), $('#testTemplate').html(), 'Template not applied');
    });

    test('when loading data errors out, then the widget contents are hidden', function () {
        expect(1);
        $('#reminders').imminentRemindersPane({
            sendRequest: function (options) { options.error({}); }
        });

        setTimeout(function () {
            forceCompletionOfAllAnimations();
            ok($('#summary-reminders-content').is(':hidden'), 'imminent reminders are hidden');
            start();
        }, 200);
        stop();
    });

    test('when loading data errors out, then the error status is triggered', function () {
        expect(2);
        var eventType = 'loadError';

        $('#reminders').imminentRemindersPane({
            sendRequest: function (options) { options.error({}); },
            publish: function (event, status) {
                if (status.type === eventType) {
                    ok(status, 'status object passed to publisher');
                    equal(status.type, eventType, 'status is of type : ' + eventType);
                }
            }
        });

        forceCompletionOfAllAnimations();
    });

    test('when refreshData is called, then sendRequest is called', function () {
        expect(1);

        $('#reminders').imminentRemindersPane({
            sendRequest: function (option) { option.success({}); },
            invalidateData: function () { }
        });

        // Need to set test function up separately in case it may 
        // be called during create of the widget.
        $('#reminders').imminentRemindersPane(
                            'option',
                            'sendRequest', 
                            function (options) { 
                                ok(true, 'refreshData did not invoke sendRequest'); 
                            });

        $('#reminders').imminentRemindersPane('refreshData');
    });

    test('when requeryData is called, then cached data is invalidated', function () {
        expect(1);

        $('#reminders').imminentRemindersPane({
            sendRequest: function (options) { options.success({}); },
            invalidateData: function () {
                ok(true, 'resetData was called properly');
            }
        });

        $('#reminders').imminentRemindersPane('requeryData');
    });

    test('when refreshData is called, then template is re-applied', function () {
        expect(2);

        $('#reminders').imminentRemindersPane({
            sendRequest: function (options) { options.success({}); },
            templateId: '#testTemplate'
        });

        equal($('#summary-reminders-content').html(), $('#testTemplate').html(), 'Template applied');

        $('#reminders').imminentRemindersPane('option', 'templateId', '#testTemplate2');
        $('#reminders').imminentRemindersPane('refreshData');

        equal($('#summary-reminders-content').html(), $('#testTemplate2').html(), 'Template applied');
    });

    test('when created, then adds class to element', function () {
        expect(1);
        $('#reminders').imminentRemindersPane({
            sendRequest: function (options) { }
        });

        equal($('#reminders').length, 1, 'reminders class added');
    });
} (jQuery));