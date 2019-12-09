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

    module('MileageStats PubSub');

    test('When publish, subscribers are fired', function () {
        expect(1);
        mstats.pubsub.subscribe('test', function () {
            ok(true, 'Yep, event is firing');
            start();
        });
        mstats.pubsub.publish('test');
        stop();
    });

    test('When data is supplied, it is passed from publisher to subscriber', function () {
        expect(1);
        var testdata = 'test data',
            testevent = 'test event';
        mstats.pubsub.subscribe(testevent, function (data) {
            equal(data, testdata, 'Correct data arrived through ' + testevent + ' in one piece.');
            start();
        });
        mstats.pubsub.publish(testevent, testdata);
        stop();
    });

    test('When unsubscribed from an event, future events are not fired', function () {
        expect(0);
        var testevent = 'some.event',
            handler = function () {
                ok(false, 'PubSub should no longer be subscribed here');
            };
        mstats.pubsub.subscribe(testevent, handler, this);
        mstats.pubsub.unsubscribe(testevent, handler, this);
        mstats.pubsub.publish(testevent);
    });

    test('When a subscribed callback throws, then other events are still fired', function () {
        expect(1);
        var testevent = 'some.eventWithError',
            badHandler = function () {
                throw "some error";
            },
            anotherHandler = function () {
                ok(true, 'anotherHandler was called after an error in another handler');
                start();
            };

        mstats.pubsub.subscribe(testevent, badHandler, this);
        mstats.pubsub.subscribe(testevent, anotherHandler, this);

        mstats.pubsub.publish(testevent);
        stop();
    });

} (jQuery));
