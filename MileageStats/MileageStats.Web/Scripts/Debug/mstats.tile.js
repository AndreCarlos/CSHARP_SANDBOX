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
/*global jQuery */
(function ($) {

    $.widget('mstats.tile', {

        // takes an array of animationInfo objects that looks like:
        // {position : {top: 10, left: 20}, duration : 400 };
        // or a single animationInfo object
        moveTo: function (animationInfoArray, callback) {
            var that = this,
                arrayLength = 0;

            this._unlock();

            // if this is an array, iterate over it
            if (animationInfoArray.length) {
                arrayLength = animationInfoArray.length;
                $.each(animationInfoArray, function (index, info) {
                    if (index === arrayLength - 1) {
                        that._animate(info, callback);
                    } else {
                        that._animate(info);
                    }
                });
            }

            // otherwise just animate one step with the data
            else {
                this._animate(animationInfoArray, callback);
            }
        },

        // takes an animationInfo object that looks like:
        // {position : {top: 10, left: 20}, duration : 400 };
        _animate: function (animationInfo, callback) {
            this.element.animate(
                animationInfo.position, {
                    duration: animationInfo.duration,
                    complete: function () {
                        if (callback) {
                            callback();
                        }
                }
            });
        },

        _unlock: function () {
            this.element.css({
                position: 'absolute',
                float: 'none'
            });
        },

        beginAnimation: function () {
            var $element = this.element;
            $element.css({
                top: $element.position().top + 'px',
                left: $element.position().left + 'px'
            });
        },

        endAnimation: function () {
            this.element
                .attr('style', '')
                .css({
                    top: 0,
                    left: 0,
                    position: 'relative',
                    float: 'left'
                });
        }
    });

}(jQuery));