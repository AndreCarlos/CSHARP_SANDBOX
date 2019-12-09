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
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.Messages;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace MileageStats.Web.Authentication
{
#if DEBUG
    /// <summary>
    /// A fake OpenIdRelyingParty to provide an 'offline' authentication capability.
    /// This is a sample only to allow easily running the RI without needing an openID.
    /// Do _not_ use this in a production application.
    /// </summary>
    /// <remarks>
    /// This simple approach was done as an alternative to creating a working, but local,
    /// OpenId provider.
    /// </remarks>    
    public class LocalFakeRelyingParty : IOpenIdRelyingParty
    {
        public DotNetOpenAuth.OpenId.RelyingParty.IAuthenticationResponse GetResponse()
        {
            return new MockAuthenticationResponse(HttpContext.Current);
        }

        public ActionResult RedirectToProvider(string providerUrl, string returnUrl, FetchRequest fetch)
        {
            return new RedirectToRouteResult(new RouteValueDictionary(new
                                                                          {
                                                                              Controller = "MockAuthenticator",
                                                                              Action = "Index",
                                                                              ProviderUrl = providerUrl,
                                                                              ReturnUrl = returnUrl
                                                                          }));
        }

        private class MockAuthenticationResponse : IAuthenticationResponse
        {
            private readonly HttpContext _httpContext;

            public MockAuthenticationResponse(HttpContext httpContext)
            {
                if (httpContext == null) throw new ArgumentNullException("httpContext");
                this._httpContext = httpContext;
            }

            public string GetCallbackArgument(string key)
            {
                throw new NotImplementedException();
            }

            public string GetUntrustedCallbackArgument(string key)
            {
                throw new NotImplementedException();
            }

            public IDictionary<string, string> GetCallbackArguments()
            {
                throw new NotImplementedException();
            }

            public IDictionary<string, string> GetUntrustedCallbackArguments()
            {
                throw new NotImplementedException();
            }

            public T GetExtension<T>() where T : IOpenIdMessageExtension
            {
                return (T) this.GetExtension(typeof(T));
            }

            public IOpenIdMessageExtension GetExtension(Type extensionType)
            {
                if (extensionType == typeof(FetchResponse))
                {
                    var response = new FetchResponse();

                    return response;
                }

                return null;
            }

            public T GetUntrustedExtension<T>() where T : IOpenIdMessageExtension
            {
                throw new NotImplementedException();
            }

            public IOpenIdMessageExtension GetUntrustedExtension(Type extensionType)
            {
                throw new NotImplementedException();
            }

            public Identifier ClaimedIdentifier
            {
                get
                {
                    var identifier = this._httpContext.Request.Form.Get("claimed_identifier");
                    return Identifier.Parse(identifier, true);
                }
            }

            public string FriendlyIdentifierForDisplay
            {
                get { return this._httpContext.Request.Form.Get("friendly_name"); }
            }

            public AuthenticationStatus Status
            {
                get { return AuthenticationStatus.Authenticated; }
            }

            public IProviderEndpoint Provider
            {
                get { throw new NotImplementedException(); }
            }

            public Exception Exception
            {
                get { return null; }
            }
        }
    }
#endif
}