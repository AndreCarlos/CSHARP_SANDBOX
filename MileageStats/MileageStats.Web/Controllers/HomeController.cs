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
using System.Web.Mvc;
using System.Web.UI;
using MileageStats.Domain.Contracts;

namespace MileageStats.Web.Controllers
{
    public class HomeController : AuthorizedController
    {
        private readonly IChartDataService chartDataService;

        public HomeController(IUserServices userServices, IChartDataService chartDataService)
            : base(userServices, null)
        {
            this.chartDataService = chartDataService;
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
                return RedirectToRoute("Dashboard");

#if DEBUG
            // NOTE: When we are in debug mode, we are using a mock OpenId provider.
            // For convenience we fill in the url for the fake provider
            ViewData["providerUrl"] = "http://oturner.myidprovider.org/";
#endif
            return View();
        }        

        [HttpPost]
        [Authorize]
        public ActionResult JsonGetFleetStatisticSeries()
        {
            var series = chartDataService.CalculateSeriesForUser(CurrentUserId, null, null);
            return Json(series);
        }
    }
}