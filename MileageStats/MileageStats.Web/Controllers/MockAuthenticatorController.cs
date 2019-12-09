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
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MileageStats.Web.Controllers
{
#if DEBUG
    /// <summary>
    /// A mock authenticator to support an offline experience for the app.
    /// </summary>
    public class MockAuthenticatorController : Controller
    {
        //
        // GET: /MockAutenticator/

        public ActionResult Index(string providerUrl, string returnUrl)
        {
            return this.View(new MockAuthenticationViewModel()
                                 {
                                     ReturnUrl = returnUrl,
                                     ProviderUrl = providerUrl,
                                     claimed_identifier = "http://oturner.myidprovider.org/"                                     
                                 });
        }
    }

    public class MockAuthenticationViewModel
    {
        public string ReturnUrl { get; set; }
        
        public string ProviderUrl { get; set; }

        [Display(Name = "Claimed Identifier")]
        public string claimed_identifier { get; set; }        
    }
#endif
}