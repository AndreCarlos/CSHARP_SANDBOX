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
using MileageStats.Domain.Models;
using MileageStats.Web.Models;

namespace MileageStats.Web.Helpers
{
    public static class VehicleHelper
    {
        public static MvcHtmlString AverageFuelEfficiencyText(this HtmlHelper helper, VehicleModel vehicle)
        {
            double raw = vehicle.Statistics.AverageFuelEfficiency;
            double averageFuelEfficiency = Math.Round(raw);

            string averageFuelEfficiencyText = string.Format("{0:N0}", averageFuelEfficiency);

            if (averageFuelEfficiency >= 99000)
            {
                averageFuelEfficiencyText = "99k+";
            }
            else if (averageFuelEfficiency >= 10000)
            {
                averageFuelEfficiencyText = string.Format("{0:N1}k", averageFuelEfficiency/1000);
            }

            return MvcHtmlString.Create(averageFuelEfficiencyText);
        }

        public static MvcHtmlString AverageFuelEfficiencyMagnitude(this HtmlHelper helper, VehicleModel vehicle)
        {
            double raw = vehicle.Statistics.AverageFuelEfficiency;
            double averageFuelEfficiency = Math.Round(raw);
            string averageFuelEfficiencyMagnitude = "";

            if (averageFuelEfficiency >= 1000)
            {
                averageFuelEfficiencyMagnitude = "thousands";
            }
            else if (averageFuelEfficiency >= 100)
            {
                averageFuelEfficiencyMagnitude = "hundreds";
            }

            return MvcHtmlString.Create(averageFuelEfficiencyMagnitude);
        }

        public static string CssClassForTile(this HtmlHelper helper, VehicleListViewModel list, VehicleModel vehicle)
        {
            var selectedItem = list.Vehicles.SelectedItem;

            var shouldCompact = (list.IsCollapsed && selectedItem == null)
                ||( selectedItem != null && vehicle.VehicleId != selectedItem.VehicleId);

            return shouldCompact ? "compact" : String.Empty;
        }
    }
}