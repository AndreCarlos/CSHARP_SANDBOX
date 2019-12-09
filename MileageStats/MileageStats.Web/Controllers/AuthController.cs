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
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;
using Microsoft.Practices.Unity;
using MileageStats.Domain.Contracts;
using MileageStats.Web.Authentication;
using MileageStats.Domain.Properties;

namespace MileageStats.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IOpenIdRelyingParty relyingParty;
        private readonly IFormsAuthentication formsAuthentication;
        private readonly IUserServices userServices;

        [InjectionConstructor]
        public AuthController(IOpenIdRelyingParty relyingParty, IFormsAuthentication formsAuthentication,
                              IUserServices userServices)
        {
            this.relyingParty = relyingParty;
            this.formsAuthentication = formsAuthentication;
            this.userServices = userServices;
        }

        //
        // GET: /Auth/
        public ActionResult SignIn()
        {
            return this.View();
        }

        public ActionResult SignInWithProvider(string providerUrl)
        {
            if (string.IsNullOrEmpty(providerUrl))
            {
                return this.RedirectToAction("SignIn");
            }

            var fetch = new FetchRequest();
            var returnUrl = this.Url.Action("SignInResponse", "Auth", null, this.Request.Url.Scheme);

            try
            {
                return this.relyingParty.RedirectToProvider(providerUrl, returnUrl, fetch);
            }
            catch (Exception)
            {
                this.TempData["Message"] = Resources.AuthController_SignIn_UnableToAuthenticateWithProvider;
                return this.RedirectToAction("SignIn");
            }
        }

        public ActionResult SignInResponse(string returnUrl)
        {
            var response = this.relyingParty.GetResponse();

            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:
                    var user = this.userServices.GetOrCreateUser(response.ClaimedIdentifier);
                    this.formsAuthentication.SetAuthCookie(this.HttpContext,
                                                           UserAuthenticationTicketBuilder.CreateAuthenticationTicket(
                                                               user));

                    return this.RedirectToRoute("Dashboard");

                case AuthenticationStatus.Canceled:
                    this.TempData["Message"] = "Cancelled Authentication";
                    return this.RedirectToAction("SignIn");

                case AuthenticationStatus.Failed:
                    this.TempData["Message"] = response.Exception.Message;
                    return this.RedirectToAction("SignIn");

                default:
                    this.TempData["Message"] = Resources.AuthController_SignInResponse_Unable_to_authenticate;
                    return this.RedirectToAction("SignIn");
            }
        }

        public ActionResult SignOut()
        {
            this.formsAuthentication.Signout();
            return this.RedirectToAction("Index", "Home");
        }
    }
}