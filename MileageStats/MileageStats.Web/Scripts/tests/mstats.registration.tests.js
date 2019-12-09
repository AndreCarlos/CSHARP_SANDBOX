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

    module('MileageStats Registration Widget Tests', {
        setup: function () {
            $('#qunit-fixture').append(
                '<div id="registration">' +
                    '<div id="registration-content">' +
                        '<form action="/Profile/CompleteRegistration" method="post">' +
                            '<input id="UserId" name="UserId" type="hidden" value="1" />' +
                            '<input id="AuthorizationId" name="AuthorizationId" type="hidden" value="Sample Token" />' +
                            '<input id="DisplayName" name="DisplayName" type="text" value="Sample DisplayName" />' + 
                            '<select id="Country" name="Country">' +
                                '<option value="">-- Select country --</option>' +
                                '<option selected="selected">Sample Country</option>' +
                                 '<option>Other Country</option>' +
                            '</select>' + 
                            '<input id="PostalCode" name="PostalCode" type="text" value="Sample PostalCode" />' + 
                            '<div>' +
                                '<button data-action="profile-save" type="submit">Save</button>' +
                            '</div>' +
                        '</form>' +
                    '</div>' +
                    '</div>'
            );
        }
    });

    test('when widget is attached to the #registration element, then submit is intercepted and calls sendRequest', function () {
        expect(1);

        $('#registration').registration({
            sendRequest: function (options) { 
                ok(true, 'sendRequest called on submit');            
            }
        });
        $('[data-action=profile-save]').first().click();
    });

    test('when widget is attached to the #registration element, then submit is intercepted and calls sendRequest with form data', function () {
        expect(5);

        $('#registration').registration({
            sendRequest: function (options) { 
                equal(options.data.UserId, '1', 'UserId passed to sendRequest');       
                equal(options.data.AuthorizationId, 'Sample Token', 'AuthorizationId passed to sendRequest');       
                equal(options.data.DisplayName, 'Sample DisplayName', 'DisplayName passed to sendRequest');       
                equal(options.data.Country, 'Sample Country', 'Country passed to sendRequest');       
                equal(options.data.PostalCode, 'Sample PostalCode', 'PostalCode passed to sendRequest');       
            }       
        });
        $('[data-action=profile-save]').first().click();
    });

    test('when form submitted, then status set to saving', function () {
        expect(2);
        var eventType = 'saving';

        $('#registration').registration({
            sendRequest: function (options) {
                setTimeout(function () {
                    // just kill some time so the test can finish before we change states
                    options.success({});
                }, 500);
            },
            publish: function (event, status) {
                if (status.type === eventType) {
                    ok(status, 'status object passed to publisher');
                    equal(status.type, eventType, 'status is of type : ' + eventType);
                }
            }
        });

        $('[data-action=profile-save]').first().click();
        forceCompletionOfAllAnimations();
    });

    test('when form submission is successful, then saved is triggered', function () {
        expect(2);
        var eventType = 'saved';
        $('#registration').registration({
            sendRequest: function (options) {
                options.success({});
            },
            publish: function (event, status) {
                if (status.type === eventType) {
                    ok(status, 'status object passed to publisher');
                    equal(status.type, eventType, 'status is of type : ' + eventType);
                }
            }
        });

        $('[data-action=profile-save]').first().click();
        forceCompletionOfAllAnimations();
    });

    test('when sendRequest errors out, then error status set to error', function () {
        expect(2);
        var eventType = 'saveError';

        $('#registration').registration({
            sendRequest: function (options) {
                options.error({});
            },
            publish: function (event, status) {
                if (status.type === eventType) {
                    ok(status, 'status object passed to publisher');
                    equal(status.type, eventType, 'status is of type : ' + eventType);
                }
            }
        });
        $('[data-action=profile-save]').first().click();
        forceCompletionOfAllAnimations();
    });

}(jQuery));