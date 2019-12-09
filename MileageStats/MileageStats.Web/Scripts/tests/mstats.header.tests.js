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
(function ($) {

    module('Header Widget Tests', {
        setup: function () {
            $('#qunit-fixture').append('<div class="header" id="header" data-url="/Dashboard">' +
                '<div><div><h1 data-title>Dashboard</h1>' +
                    '<div id="notification"></div>' +
                        '<div class="nav">' +
                            '<span id="welcome">Welcome <span data-display-name>Sample User</span></span>' +
                            '[ <a id="dashboard-link" href="/Dashboard">Dashboard</a>' +        
                            '| <a id="charts-link" href="/Chart/List">Charts</a>' +
                            '| <a id="profile-link" href="/Profile/Edit">Profile</a>' +
                            '| <a id="login-link" href="/Auth/SignOut">Sign Out</a> ]' +
                        '</div></div>' +
                    '</div>' +
                '</div>'
            );
        }
    });

    test('when widget is attached to the #header element, then dashboard and chart links is changed to update the URL hash', function () {
        expect(2);

        $('#header').header();

        var link = $('#dashboard-link').attr('href'),
            rootUrl = link.substring(0, link.indexOf('#')),
            querystring = $.param.fragment(link),
            state = $.deparam.querystring(querystring),
            layoutHash = state.layout,
            vehicledIdHash = state.vid;

        equal(rootUrl, '/Dashboard', 'base url updated so no redirect occurs');
        equal(layoutHash, 'dashboard', 'screen set properly');
    });

    test('when title option is changed, then it displays new title', function() {
        expect(1);
        var header = $('#header').header();
        header.header('option', 'title', 'test title');
        
        equal($('[data-title]').text(), 'test title', 'header text set properly');
    });

}(jQuery));