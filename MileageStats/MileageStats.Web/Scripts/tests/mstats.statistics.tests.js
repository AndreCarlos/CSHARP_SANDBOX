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
    module('MileageStats Statistics Pane Widget Tests', {
        setup: function () {
            this.savedAjax = $.ajax;
            $('#qunit-fixture').append(
                '<div id="statistics" class="statistics section">' +
                    '<div id="statistics-content">' +
                        '<h1>Summary Statistics</h1>' +
                        '<div class="statistic mile-per-gallon">21' +
                            '<span class="units">mpg</span>' +
                        '</div>' +
                        '<div class="statistic cost-per-month">19&cent;' +
                        '<span class="units">per mile</span></div>' +
                        '<div class="statistic cost-per-mile">$51' +
                        '<span class="units">per month</span></div>' +
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
        $('#statistics').statisticsPane({
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

        $('#statistics').statisticsPane();
    });

    test('when sendRequest is called, then widget passes with dataUrl', function () {
        expect(1);

        $('#statistics').statisticsPane({
            sendRequest: function (options) {
                equal(options.url, '/some/url', 'Url was passed in');
            },
            dataUrl: '/some/url'
        });

    });

    test('when widget data is retrieved and no template specified, then widget contents are not replaced by the template', function () {
        expect(1);
        $('#statistics').statisticsPane({
            sendRequest: function (options) {
                options.success({});
            }
        });

        notEqual($('.mstats-statistics-content').html(), $('#testTemplate').html(), 'Template not applied');
    });


    test('when widget data is retrieved, then widget contents are replaced by the template', function () {
        expect(1);
        var widget = $('#statistics').statisticsPane({
            sendRequest: function (options) {
                options.success({});
            },
            templateId: '#testTemplate'
        });

        equal($.trim(widget.find('div').html()).toLowerCase(), $.trim($('#testTemplate').html()).toLowerCase(), 'Template applied');
    });

    test('when loading data errors out, then triggers error status', function () {
        expect(2);
        var eventType = 'loadingError';
        $('#statistics').statisticsPane({
            templateId: '#testTemplate',
            sendRequest: function (options) { options.error({}); },
            publish: function (event, status) {
                if (status.type === eventType) {
                    ok(status, 'status object passed to publisher');
                    equal(status.type, eventType, 'status is of type : ' + eventType);
                }
            }
        });
    });

    test('when loading data errors out, then the widget contents are hidden', function () {
        expect(1);
        $('#statistics').statisticsPane({
            sendRequest: function (options) { options.error({}); }
        });

        setTimeout(function () {
            forceCompletionOfAllAnimations();
            ok($('#statistics-content').is(':hidden'), 'statistics are hidden');
            start();
        }, 200);
        stop();
    });

    test('when loading data succeeds, then contents are shown', function () {
        expect(1);
        $('#statistics').statisticsPane({
            sendRequest: function (options) {
                forceCompletionOfAllAnimations();
                options.success();
            }
        });

        forceCompletionOfAllAnimations();

        ok(!$('.mstats-statistics-content').is(':hidden'), 'statistics are shown');
    });

    test('when refreshData is called, then sendRequest is called', function () {
        expect(1);

        var widget = $('#statistics').statisticsPane({
            sendRequest: function (options) { options.success({}); }
        });

        widget.statisticsPane('option', 'sendRequest', function () {
            ok(true, 'sendRequest was called properly');
        });

        widget.statisticsPane('refreshData');
    });

    test('when refreshData is called, then cached data is not invalidated', function () {
        expect(0);

        $('#statistics').statisticsPane({
            sendRequest: function (options) { options.success({}); },
            invalidateData: function () {
                ok(false, 'invalidateData was called properly');
            }
        });

        $('#statistics').statisticsPane('refreshData');
    });

    test('when requeryData is called, then cached data is invalidated', function () {
        expect(1);

        $('#statistics').statisticsPane({
            sendRequest: function (options) { options.success({}); },
            invalidateData: function () {
                ok(true, 'invalidateData was called properly');
            }
        });

        $('#statistics').statisticsPane('requeryData');
    });

    test('when requeryData is called, then sendRequest is invoked', function () {
        expect(1);

        $('#statistics').statisticsPane({
            sendRequest: function (options) { options.success({}); },
            invalidateData: function () { }
        });

        $('#statistics').statisticsPane('option', 'sendRequest', function () {
            ok(true, 'sendRequest was called properly');
        });

        $('#statistics').statisticsPane('requeryData');
    });

    test('when refreshData is called, then template is re-applied', function () {
        expect(2);

        var widget = $('#statistics').statisticsPane({
                sendRequest: function(options) { options.success({  }); },
                templateId: '#testTemplate'
            }),
            content = widget.find('div');

        equal($.trim(content.html()).toLowerCase(), $.trim($('#testTemplate').html()).toLowerCase(), 'Template applied');

        widget.statisticsPane('option', 'templateId', '#testTemplate2');
        widget.statisticsPane('refreshData');

        equal($.trim(content.html()).toLowerCase(), $.trim($('#testTemplate2').html()).toLowerCase(), 'Template applied');
    });
}(jQuery));