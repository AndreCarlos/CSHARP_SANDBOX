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
using System;
using System.Web.Mvc;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace MileageStats.Web.Authentication
{
    internal class DefaultOpenIdRelyingParty : IOpenIdRelyingParty, IDisposable
    {
        private readonly OpenIdRelyingParty relyingParty = new OpenIdRelyingParty();

        public IAuthenticationResponse GetResponse()
        {
            return this.relyingParty.GetResponse();
        }

        public ActionResult RedirectToProvider(string providerUrl, string returnUrl, FetchRequest fetch)
        {
            IAuthenticationRequest authenticationRequest = this.relyingParty.CreateRequest(providerUrl,
                                                                                           Realm.AutoDetect,
                                                                                           new Uri(returnUrl));
            authenticationRequest.AddExtension(fetch);

            return new OutgoingRequestActionResult(authenticationRequest.RedirectingResponse);
        }

        public virtual void Dispose()
        {
            this.relyingParty.Dispose();
        }
    }
}