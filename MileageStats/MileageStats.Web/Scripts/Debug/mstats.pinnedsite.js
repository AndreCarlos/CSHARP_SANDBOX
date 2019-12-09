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
/*global jQuery, document, setTimeout, window */
(function (mstats, $) {
    var sendRequest;
    
    function showCallout () {
        $('#pinnedSiteCallout').show();
    }
    
    function hideCallout() {
        $('#pinnedSiteCallout').hide();
    }
    
    mstats.pinnedSite = {
        initializePinnedSiteImage: function () {
            
            // Do not enabled site pinning for non-Internet Explorer 9+ browsers
            // Do not show the callout if the site is already pinned
            if (!(!document.documentMode || this.isPinned())) {
                $('#pinnedSiteImage')
                    .bind('mousedown mouseout', hideCallout)
                    .bind('mouseover', showCallout)
                    .addClass('active');
                $('#pinnedSiteCallout').show();
                setTimeout(hideCallout, 5000);
            }
        },
        
        intializeData: function (sendRequestFunc) {
            sendRequest = sendRequestFunc;
            this.requeryJumpList();
        },
        
        requeryJumpList: function () {
            var getRelativeUrl = mstats.getRelativeEndpointUrl;
            
            try {
                if (this.isPinned()) {

                    sendRequest({
                        url: '/reminder/overduelist/',
                        contentType: 'application/json',
                        cache: false,
                        success: function (data) {

                            try {
                                var g_ext = window.external,
                                    faviconUrl = getRelativeUrl('/favicon.ico'),
                                    iconOverlayUrl,
                                    iconOverlayMessage,
                                    numReminders = data.Reminders.length,
                                    reminderUrl,
                                    reminder,
                                    i;
                                
                                g_ext.msSiteModeClearJumpList();
                                g_ext.msSiteModeCreateJumpList("Reminders");
                                g_ext.msSiteModeClearIconOverlay();

                                if (data.Reminders) {
                                    for (i = 0; i < numReminders; i += 1) {
                                        reminder = data.Reminders[i];
                                        reminderUrl = getRelativeUrl('/reminder/details/' + reminder.Reminder.ReminderId.toString());
                                        g_ext.msSiteModeAddJumpListItem(reminder.FullTitle, reminderUrl, faviconUrl, "self");
                                    }

                                    if (numReminders > 0) {
                                        iconOverlayUrl = '/content/overlay-' + numReminders + '.ico';
                                        iconOverlayMessage = 'You have ' + numReminders.toString() + ' maintenance tasks that are ready to be accomplished.';
                                        if (numReminders > 3) {
                                            iconOverlayUrl = '/content/overlay-3plus.ico';
                                        }
                                        g_ext.msSiteModeSetIconOverlay(getRelativeUrl(iconOverlayUrl), iconOverlayMessage);
                                    }
                                }

                                g_ext.msSiteModeShowJumpList();
                            }
                            catch (e) {
                                // Fail silently. Pinned Site API not supported.
                            }
                        }
                    });
                }
            }
            catch (e) {
                // Fail silently. Pinned Site API not supported.
            }
        },
        
        isPinned: function () {
            try {
                // we have to use try/catch because checking for the presence
                // of msIsSiteMode explicitly does not work
                return window.external.msIsSiteMode();
            }
            catch (e) {
                return false;
            }
        }
    };

}(this.mstats = this.mstats || {}, jQuery));