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

    module('Status Widget Tests', {
        setup: function () {
            $('#qunit-fixture').append('<div id="notification"/>');
        },
        teardown: function () {

        }
    });

    test('when initialized, then listens for status messages', function () {
        expect(1);
        $('#notification').status({
            subscribe: function (status) {
                ok(status, 'widget subscribed to status message');
            }
        });
        mstats.pubsub.publish(mstats.events.status, { type: 'saving' });
    });

    test('when status message published, then message is displayed', function () {
        expect(1);
        var msg = 'Saved';

        $('#notification').status({
            subscribe: mstats.pubsub.subscribe
        });
        
        mstats.pubsub.publish(mstats.events.status, {
            type: 'saved',
            duration: 3000,
            message: msg
        });

        setTimeout(function () {
            equal($('#notification').text(), msg, 'message initially displayed');
            start();
        }, 200); // give it a 200ms chance to set the message
        stop();
    });

    test('when status message published, then hides after duration expires', function () {
        expect(2);
        var msg = 'Saved';

        $('#notification').status({
            subscribe: mstats.pubsub.subscribe
        });
        
        mstats.pubsub.publish(mstats.events.status, {
            type: 'saved',
            duration: 2000,
            message: msg
        });

        setTimeout(function () {
            equal($('#notification').text(), msg, 'message initially displayed');
            setTimeout(function () {
                ok($('#notification').is(':hidden'), 'widget hid message after duration');
                start();
            }, 1500);
        }, 1000);
        stop();
    });

    test('when "saved" message published, then hides "saving" message', function () {
        expect(2);
        var savingMsg = 'Saving ...',
            savedMsg = 'Saved';

        $('#notification').status({
            subscribe: mstats.pubsub.subscribe
        });
        
        mstats.pubsub.publish(mstats.events.status, {
            type: 'saving',
            //duration: 3000,
            message: savingMsg
        });

        setTimeout(function () {
                
            equal($('#notification').text(), savingMsg, 'message initially displayed');

            // preempt 'saving' with a 'saved' message
            mstats.pubsub.publish(mstats.events.status, {
                type: 'saved',
                duration: 2000,
                message: savedMsg
            });

            // assert the saving message is now showing
            setTimeout(function () {
                equal($('#notification').text(), savedMsg, 'saved message displayed on top of saving');
                start();
            }, 1500); // wait until we're sure the "saved" message is showing

        }, 1500); // wait until after the default 1000ms delay for showing "saving"
        stop();
    });

}(jQuery));