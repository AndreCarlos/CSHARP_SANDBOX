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
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using MileageStats.Web.Models;
using MileageStats.Web.Authentication;
using MileageStats.Web.UnityExtensions;
using MileageStats.Data;
using System.Linq;
using System.Collections.Generic;

namespace MileageStats.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static IUnityContainer container;
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.([iI][cC][oO]|[gG][iI][fF])(/.*)?" });

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Dashboard", // Route name
                "Dashboard", // URL
                new {controller = "Vehicle", action = "List"} // Parameter defaults
                );

            routes.MapRoute(
                "VehiclePhotoRoute", // Route name
                "Vehicle/Photo/{vehiclePhotoId}", // URL with parameters
                new { controller = "Vehicle", action = "Photo" } // Parameter defaults
                );

            routes.MapRoute(
                "VehicleFuelEfficiencyChartRoute",
                "Vehicle/FuelEfficiencyChart/{userId}/{vehicleId}",
                new { controller = "Vehicle", action = "FuelEfficiencyChart" }
                );

            routes.MapRoute(
                "VehicleTotalDistanceChartRoute",
                "Vehicle/TotalDistanceChart/{userId}/{vehicleId}",
                new { controller = "Vehicle", action = "TotalDistanceChart" }
                );

            routes.MapRoute(
                "VehicleTotalCostChartRoute",
                "Vehicle/TotalCostChart/{userId}/{vehicleId}",
                new { controller = "Vehicle", action = "TotalCostChart" }
                );

            routes.MapRoute(
                "ListRoute", // Route name
                "{controller}/List/{vehicleId}", // URL with parameters
                new { action = "List", vehicleId = UrlParameter.Optional } // Parameter defaults
                );

            routes.MapRoute(
                "JsonListRoute", // Route name
                "{controller}/JsonList/{vehicleId}", // URL with parameters
                new { action = "JsonList" } // Parameter defaults
                );

            routes.MapRoute(
                "AddRoute", // Route name
                "{controller}/Add/{vehicleId}",
                new {action = "Add"}
                );

            routes.MapRoute(
                "DetailsRoute",
                "{controller}/Details/{id}",
                new {action = "Details"}
                );
           
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
        }

        public override void Init()
        {
            this.PostAuthenticateRequest += this.PostAuthenticateRequestHandler;
            this.EndRequest += this.EndRequestHandler;

            base.Init();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            InitializeDependencyInjectionContainer();
            InitializeDatabase();
        }

        private void EndRequestHandler(object sender, EventArgs e)
        {
            // This is a workaround since subscribing to HttpContext.Current.ApplicationInstance.EndRequest 
            // from HttpContext.Current.ApplicationInstance.BeginRequest does not work. 
            IEnumerable<UnityHttpContextPerRequestLifetimeManager> perRequestManagers = 
                container.Registrations
                    .Select(r => r.LifetimeManager)
                    .OfType<UnityHttpContextPerRequestLifetimeManager>()
                    .ToArray();

            foreach (var manager in perRequestManagers)
            {
                manager.Dispose();
            }
        }

        private void PostAuthenticateRequestHandler(object sender, EventArgs e)
        {
            HttpCookie authCookie = this.Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (IsValidAuthCookie(authCookie))
            {
                var formsAuthentication = ServiceLocator.Current.GetInstance<IFormsAuthentication>();

                var ticket = formsAuthentication.Decrypt(authCookie.Value);
                var mileageStatsIdentity = new MileageStatsIdentity(ticket);
                this.Context.User = new GenericPrincipal(mileageStatsIdentity, null);
                
                // Reset cookie for a sliding expiration.
                formsAuthentication.SetAuthCookie(this.Context, ticket);
            }
        }

        private static bool IsValidAuthCookie(HttpCookie authCookie)
        {
            return authCookie != null && !String.IsNullOrEmpty(authCookie.Value);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope",
            Justification = "This should survive the lifetime of the application.")]
        private static void InitializeDependencyInjectionContainer()
        {
            container = new UnityContainerFactory().CreateConfiguredContainer();            
            var serviceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static void InitializeDatabase()
        {
            var repositoryInitializer = ServiceLocator.Current.GetInstance<IRepositoryInitializer>();
            repositoryInitializer.Initialize();           
        }

        
    }
}