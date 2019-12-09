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
/*jslint onevar: true, undef: true, newcap: true, regexp: true, 
plusplus: true, bitwise: true, devel: true, maxerr: 50 */
/*global window: true, jQuery:true, $:true, document:true, mstats:true */

$(function () {
    var charts,
        header,
        infoPane,
        summaryPane,
        vehicleList;
    
    // setup default error handler for redirects due to session timeout.
    $(document).ajaxError(function (ev, jqXHR, settings, errorThrown) {
        if (jqXHR.status === 200) {
          if (jqXHR.responseText.indexOf('Mileage Stats Sign In') !== -1) {
            window.location.replace(mstats.getRelativeEndpointUrl('/Auth/SignIn'));
          } else if (jqXHR.responseText.indexOf('Mileage Stats | Accident!') !== -1) {
            window.location.replace(mstats.getRelativeEndpointUrl('/GenericError.htm'));
          }
        }
    });

    $('#notification').status({
        subscribe: mstats.pubsub.subscribe
    });
    
    header = $('#header').header();
    
    mstats.pinnedSite.intializeData(mstats.dataManager.sendRequest);    

    if (!window.location.pathname.match(/Dashboard/)) {
        return; // only enable widgets on the dashboard
    }

    vehicleList = $('#vehicles').vehicleList({
        // This allows the vehicleList to participate
        // in global messaging with other widgets
        publish: mstats.pubsub.publish,

        // This overrides the vehicleLists default ($.ajax) 
        // way of getting data so we can inject a caching layer
        sendRequest: mstats.dataManager.sendRequest,

        // this allows the vehicleList to invalidate data
        // stored in the data cache.  
        invalidateData: mstats.dataManager.resetData,

        // the ID of the element containing the vehicle list template
        templateId: '#mstats-vehicle-list-template'
    });

    infoPane = $('#info').infoPane({
        sendRequest: mstats.dataManager.sendRequest,
        invalidateData: mstats.dataManager.resetData,
        publish: mstats.pubsub.publish, 
        header: header
    });
    
    summaryPane = $('#summary').summaryPane({
        sendRequest: mstats.dataManager.sendRequest,
        invalidateData: mstats.dataManager.resetData,
        publish: mstats.pubsub.publish,
        header: header
    });
    
    charts = $('#main-chart').charts({
        sendRequest: mstats.dataManager.sendRequest,
        invalidateData: mstats.dataManager.resetData
    });
    
    $('body').layoutManager({
        subscribe: mstats.pubsub.subscribe,
        pinnedSite: mstats.pinnedSite,
        charts: charts,
        header: header,
        infoPane: infoPane,
        summaryPane: summaryPane,
        vehicleList: vehicleList
    });
});
