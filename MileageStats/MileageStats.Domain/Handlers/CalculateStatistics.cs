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
using System.Linq;
using MileageStats.Domain.Models;

namespace MileageStats.Domain.Handlers
{
    public static class CalculateStatistics
    {
        public static VehicleStatisticsModel Calculate(IEnumerable<Model.FillupEntry> fillUps, bool includeFirst = true)
        {
            if (!fillUps.Any()) return new VehicleStatisticsModel();

            var firstFillUp = fillUps.OrderBy(x => x.Date).FirstOrDefault();

            double totalFuelCost = 0.0;
            double totalUnits = 0.0;
            double totalCost = 0.0;
            int totalDistance = 0;

            foreach (var fillUp in fillUps)
            {
                if (includeFirst || fillUp != firstFillUp)
                {
                    totalFuelCost += fillUp.PricePerUnit*fillUp.TotalUnits;
                    totalUnits += fillUp.TotalUnits;
                    totalCost += fillUp.TotalCost;
                    totalDistance += fillUp.Distance ?? 0;
                }
            }

            var odometer = fillUps.Max(x => x.Odometer);

            var earliestEntryDate = fillUps.Min(x => x.Date).ToUniversalTime();
            var today = DateTime.UtcNow.Date;
            var totalMonths = CalculateDifferenceInMonths(earliestEntryDate.Date, today.Date);

            return new VehicleStatisticsModel(totalFuelCost, totalUnits, totalCost, totalDistance, odometer, totalMonths);
        }

        private static int CalculateDifferenceInMonths(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate) return 0;
            int years = (endDate.Year - startDate.Year);
            int months = (12 * years) + endDate.Month - startDate.Month;
            // In the case where it has not yet been a full month of fill up, default to 1
            return Math.Max(1, months);
        }
    }
}