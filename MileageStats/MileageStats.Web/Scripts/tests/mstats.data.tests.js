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

    module('MileageStats DataStore');
    test('When data is saved, then it can be retrieved', function () {
        expect(1);
        var value = 'some-data';
        mstats.dataStore.set('/some/url', value);
        equal(mstats.dataStore.get('/some/url'), value, 'mstats.datastore saved and returned ' + value);
    });

    test('When data is cleared, then it can no longer be retrieved', function () {
        expect(2);
        var value = 'some-data';
        mstats.dataStore.set('/some/url', value);
        equal(mstats.dataStore.get('/some/url'), value, 'mstats.datastore saved and returned ' + value);

        mstats.dataStore.clear('/some/url');
        equal(mstats.dataStore.get('/some/url'), undefined, 'data element was cleared from the store');
    });

    test('When clearAll is called, then all data is removed from the store', function () {
        expect(3);

        mstats.dataStore.set('/some/url', 'value1');
        mstats.dataStore.set('/another/url', 'value2');
        mstats.dataStore.set('/third/url', 'value3');

        mstats.dataStore.clearAll();

        equal(mstats.dataStore.get('/some/url'), undefined, 'the data store was properly cleared out.');
        equal(mstats.dataStore.get('/another/url'), undefined, 'the data store was properly cleared out.');
        equal(mstats.dataStore.get('/third/url'), undefined, 'the data store was properly cleared out.');
    });


    module(
        'MileageStats DataManager sendRequest Tests',
        {
            setup: function () {
                this.savedAjax = $.ajax;
                this.savedRootUrl = mstats.rootUrl;
            },
            teardown: function () {
                $.ajax = this.savedAjax;
                mstats.dataStore.clearAll();
                mstats.rootUrl = this.savedRootUrl;
            }
        }
    );

    test('when sendRequest is called and is successful, then the success callback is called', function () {
        expect(1);

        var successCallback = function () {
            ok(true, 'success callback was called');
        };

        $.ajax = function (options) {
            options.success();
        };

        mstats.dataManager.sendRequest({
            url: 'url',
            success: successCallback
        });
    });
    
    test('when sendRequest is called and caching is disabled, then ajax is invoked for each call', function () {
        expect(2);

        $.ajax = function (options) {
            options.success({hi:'world'});
            ok(true, 'ajax invoked');
        };

        var options = {
            url: 'url',
            cache: false,
            success: function() {}
        };

        mstats.dataManager.sendRequest(options);
        
        mstats.dataManager.sendRequest(options);
    });

    test('when sendRequest is called and fails, then the failure callback is called', function () {
        expect(1);

        var successCallback = function () {
            ok(false, 'success callback was called and should not have been');
        },
            errorCallback = function () {
                ok(true, 'error callback was called as expected');
            };

        $.ajax = function (options) {
            options.error();
        };

        mstats.dataManager.sendRequest({
            url: 'url',
            success: successCallback,
            error: errorCallback
        });
    });

    test('when sendRequest is called, then the url from options is used', function () {
        expect(1);
        $.ajax = function (options) {
            equal(options.url, '/url', 'Url was properly set');
        };

        mstats.dataManager.sendRequest({
            url: '/url'
        });
    });

    test('when sendRequest is called for pre-cached data, then the the cached data is returned', function () {
        expect(1);

        mstats.dataStore.set('/url', 'myData');

        $.ajax = function (options) {
            ok(false, 'ajax was called when it should not have been');
        };

        var successCallback = function (data) {
            equal(data, 'myData', 'cached data was returned');
        };

        mstats.dataManager.sendRequest({
            url: '/url',
            success: successCallback
        });
    });

    test('when sendRequest is called, then the returned data is stored in the cache', function () {
        expect(1);
        var setData,
            url = '/url',
            returnedData = 'returnedData',
            successCallback = function (data) {
                setData = mstats.dataStore.get(url);
                equal(setData, returnedData);
            };
            
        $.ajax = function (options) {
            options.success(returnedData);
        };

        mstats.dataManager.sendRequest({
            url: url,
            success: successCallback
        });
    });

    test('when sendRequest is called, then url includes the root url', function () {
        expect(1);
        $.ajax = function (options) {
            equal(options.url, '/test/url', 'Url was properly set');
        };
        mstats.rootUrl = '/test';

        mstats.dataManager.sendRequest({
            url: '/url'
        });

    });

    test('when resetData is called, then the associated data is removed from the cache.', function () {
        expect(1);

        mstats.dataStore.set('/url', 'data');

        mstats.dataManager.resetData('/url');

        equal(mstats.dataStore.get('/url'), undefined, 'data was properly cleared');
    });

}(jQuery));
