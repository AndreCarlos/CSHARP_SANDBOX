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
/*jslint white: true, onevar: true, undef: true, newcap: true, regexp: true, plusplus: true, bitwise: true, devel: true, maxerr: 50, indent: 4 */
(function ($) {

    module('MileageStats Pinned Site Widget Tests', {
        setup: function () {
            this.savedAjax = $.ajax;
            this.savedDocumentMode = document.documentMode;
            this.savedIsPinned = mstats.pinnedSite.isPinned;

            $.ajax = function () { };
            document.documentMode = 9;
            // Unable to set window.external.msIsSiteMode (built into browser), so we override isPinned
            mstats.pinnedSite.isPinned = function () { 
                return true; 
            };

            $('#qunit-fixture').append(
                '<div>' +
                    '<div id="pinnedSiteImage" />' +
                    '<div id="pinnedSiteCallout" style="display:none" />' +
                    '</div>'
            );
        },
        teardown: function () {
            $.ajax = this.savedAjax;
            document.documentMode = this.savedDocumentMode;
            mstats.pinnedSite.isPinned = this.savedIsPinned;
        }
    });

    test('when document undefined, then fails silently', function () {
        expect(1);

        document.documentMode = undefined;

        mstats.pinnedSite.initializePinnedSiteImage();

        ok(true, 'pinnedSite.initialized failed silently');
    });

    test('when pinned sites not enabled, then fails silently', function () {
        expect(1);       

        mstats.pinnedSite.isPinned = function () { return false; };

        mstats.pinnedSite.initializePinnedSiteImage();

        ok(true, 'pinnedSite.initialized failed silently');
    });

    test('when pinned sites enabled, then mouseover shows pinnedSiteCallout ', function () {
        expect(1);

        mstats.pinnedSite.isPinned = function () { return false; };
        mstats.pinnedSite.initializePinnedSiteImage();

        $('#pinnedSiteImage').simulate('mouseover');

        ok(!$('#pinnedSiteCallout').is(':hidden'), 'pinnedSiteCallout is shown');
    });

    test('when pinned sites enabled, then mousedown hides pinnedSiteCallout ', function () {
        expect(1);

        mstats.pinnedSite.isPinned = function () { return false; };
        mstats.pinnedSite.initializePinnedSiteImage();

        $('#pinnedSiteCallout').show();

        $('#pinnedSiteImage').simulate('mousedown');

        ok($('#pinnedSiteCallout').is(':hidden'), 'pinnedSiteCallout is shown');
    });

    test('when pinned sites enabled, then mouseout hides pinnedSiteCallout ', function () {
        expect(1);

        mstats.pinnedSite.isPinned = function () { return false; };
        mstats.pinnedSite.initializePinnedSiteImage();

        $('#pinnedSiteCallout').show();

        $('#pinnedSiteImage').simulate('mouseout');

        ok($('#pinnedSiteCallout').is(':hidden'), 'pinnedSiteCallout is shown');
    });

    test('when pinned sites enabled, then ajax method called ', function () {
        expect(1);

        var sendRequest = function (options) {
            equal(options.url, '/reminder/overduelist/', 'Ajax URL is for overdue list of reminders');
        };

        mstats.pinnedSite.intializeData(sendRequest);
    });

    // Note: Difficult to test success handler since window.external is not overridable.

}(jQuery));
