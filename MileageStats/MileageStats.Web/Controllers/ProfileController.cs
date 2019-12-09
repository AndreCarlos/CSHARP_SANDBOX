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
using System.Linq;
using System.Collections.ObjectModel;
using System.Security;
using System.Web.Mvc;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Models;
using MileageStats.Web.Models;
using MileageStats.Web.Authentication;

namespace MileageStats.Web.Controllers
{
    [Authorize]
    public class ProfileController : AuthorizedController
    {
        private readonly ICountryServices countryServices;
        private readonly IFormsAuthentication formsAuthentication;
        private readonly IUserServices userServices;

        public ProfileController(
            IUserServices userServices,
            ICountryServices countryServices,
            IFormsAuthentication formsAuthentication)
            : base(userServices, null)
        {
            this.userServices = userServices;
            this.countryServices = countryServices;
            this.formsAuthentication = formsAuthentication;
        }

        //
        // GET /Profile/Edit
        public ActionResult Edit()
        {
            User model = CurrentUser;
            AddCountryListToViewBag();
            return View(model);
        }

        //
        // POST /Profile/Edit
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User updatedUser)
        {
            if (updatedUser.UserId == CurrentUserId)
            {
                if (ModelState.IsValid)
                {
                    updatedUser.HasRegistered = true;
                    UserServices.UpdateUser(updatedUser);
                    formsAuthentication.SetAuthCookie(HttpContext,
                                                      UserAuthenticationTicketBuilder.CreateAuthenticationTicket(
                                                          updatedUser));
                    return RedirectToRoute("Dashboard");
                }
                User model = updatedUser;
                AddCountryListToViewBag();
                return View(model);
            }
            throw new SecurityException("Not authorized");
        }

        //
        // POST /Profile/JsonEdit
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult JsonEdit(User updatedUser)
        {
            if (Request.IsAjaxRequest() && (updatedUser.UserId == CurrentUserId))
            {
                if (ModelState.IsValid)
                {
                    updatedUser.HasRegistered = true;
                    UserServices.UpdateUser(updatedUser);
                    formsAuthentication.SetAuthCookie(HttpContext,
                                                      UserAuthenticationTicketBuilder.CreateAuthenticationTicket(
                                                          updatedUser));
                    return new EmptyResult();
                }
                User model = updatedUser;
                AddCountryListToViewBag();
                return Json(model);
            }
            throw new SecurityException("Not authorized");
        }

        //
        // POST /Profile/CompleteRegistration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteRegistration(User updatedUser)
        {
            if (updatedUser.UserId == CurrentUserId)
            {
                if (ModelState.IsValid)
                {
                    updatedUser.HasRegistered = true;
                    userServices.UpdateUser(updatedUser);
                    formsAuthentication.SetAuthCookie(HttpContext,
                                                      UserAuthenticationTicketBuilder.CreateAuthenticationTicket(
                                                          updatedUser));
                    return RedirectToRoute("Dashboard");
                }
                AddCountryListToViewBag();
                User model = updatedUser;
                return View("Edit", model);
            }
            throw new SecurityException("Not authorized");
        }

        //
        // POST /Profile/JsonCompleteRegistration
        [HttpPost]
        public ActionResult JsonCompleteRegistration(User updatedUser)
        {
            if (Request.IsAjaxRequest() && (updatedUser.UserId == CurrentUserId))
            {
                if (ModelState.IsValid)
                {
                    updatedUser.HasRegistered = true;
                    userServices.UpdateUser(updatedUser);
                    formsAuthentication.SetAuthCookie(HttpContext,
                                                      UserAuthenticationTicketBuilder.CreateAuthenticationTicket(
                                                          updatedUser));
                    return new EmptyResult();
                }
                throw new ArgumentException("Model is invalid");
            }
            throw new SecurityException("Not authorized");
        }

        private void AddCountryListToViewBag()
        {
            var countryNames = countryServices
                .GetCountriesAndRegionsList()
                .Select(country => new { text = country, value = country });

            ViewBag.CountryList = new SelectList(countryNames, "value", "text");
        }
    }
}