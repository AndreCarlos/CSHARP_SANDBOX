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
/// <reference path="jquery-1.6.1.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery-ui.js" />

(function ($) {
    // The textlineinput validator function
    $.validator.addMethod('textlineinput', function (value, element, pattern) {
        if (!value) {
            return true; // not testing 'is required' here!
        }
        try {
            var match = value.match(pattern);
            return (match && (match.index === 0) && (match[0].length === value.length));
        } catch (e) {
            return false;
        }
    });

    // The adapter to support ASP.NET MVC unobtrusive validation
    $.validator.unobtrusive.adapters.add('textlineinput', ['pattern'], function (options) {
        options.rules.textlineinput = options.params.pattern;
        if (options.message) {
            options.messages.textlineinput = options.message;
        }
    });
    $.validator.unobtrusive.adapters.addSingleVal("textlineinput", "pattern");


    // the postalcode validator.  
    $.validator.addMethod('postalcode', function (value, element, params) {
        if (!value) {
            return true; // not testing 'is required' here!
        }
        try {
            var country = $('#Country').val(),
                postalCode = $('#PostalCode').val(),
                usMatch = postalCode.match(params.unitedStatesPattern),
                internationalMatch = postalCode.match(params.internationalPattern),
                message = '',
                match;

            if (country.toLowerCase() === 'united states') {
                message = params.unitedStatesErrorMessage;
                match = usMatch;
            } else {
                message = params.internationalErrorMessage;
                match = internationalMatch;
            }

            $.extend($.validator.messages, {
                postalcode: message
            });

            return (match && (match.index === 0) && (match[0].length === postalCode.length));
        } catch (e) {
            return false;
        }
    });

    // The adapter to support ASP.NET MVC unobtrusive validation
    $.validator.unobtrusive.adapters.add(
        'postalcode',
        ['internationalErrorMessage', 'unitedStatesErrorMessage', 'internationalPattern', 'unitedStatesPattern'],
        function (options) {
            options.rules.postalcode = options.params;
        }
    );

}(jQuery));