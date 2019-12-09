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

namespace MileageStats.Domain.Models
{
    public class VehicleStatisticsModel
    {
        private readonly int? odometer;
        private readonly double totalCost;
        private readonly int totalDistance;
        private readonly double totalFuelCost;
        private readonly int totalMonths;
        private readonly double totalUnits;

        public VehicleStatisticsModel()
        {
        }

        public VehicleStatisticsModel(double totalFuelCost, double totalUnits, double totalCost, int totalDistance,
                                 int odometer, int totalMonths)
        {
            this.totalFuelCost = totalFuelCost;
            this.totalUnits = totalUnits;
            this.totalCost = totalCost;
            this.totalDistance = totalDistance;
            this.odometer = odometer;
            this.totalMonths = totalMonths;
        }

        /// <summary>
        /// Gets or sets the name of this statistic set.
        /// </summary>
        /// <value>
        /// A string - commonly set to describe the range "Lifetime", "Last12Months", etc.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the average cost to fill up per unit.
        /// </summary>
        public double AverageFillupPrice
        {
            get { return TotalUnits == 0 ? 0 : RoundToTwoDecimals(TotalFuelCost/TotalUnits); }
        }

        /// <summary>
        /// Gets the average fuel efficiency (e.g. Miles/Gallon or Kilomter / Litre)
        /// </summary>
        public double AverageFuelEfficiency
        {
            get { return TotalUnits == 0 ? 0 : RoundToTwoDecimals(TotalDistance/TotalUnits); }
        }

        /// <summary>
        /// Gets the average cost to drive per distance (e.g. $/Mile or €/Kilometer)
        /// </summary>
        public double AverageCostToDrive
        {
            get { return TotalDistance == 0 ? 0 : RoundToTwoDecimals(TotalCost/TotalDistance); }
        }

        /// <summary>
        /// Gets the average cost to drive per month (e.g. $/Month or €/Month) between the first entry and today.
        /// </summary>
        public double AverageCostPerMonth
        {
            get { return totalMonths == 0 ? 0 : RoundToTwoDecimals(TotalCost/totalMonths); }
        }

        /// <summary>
        /// Gets the highest odometer value recorded.
        /// </summary>
        /// <remarks>
        /// This is a calculated value and should not be set directly.
        /// </remarks>
        public int? Odometer
        {
            get { return odometer; }
        }

        /// <summary>
        /// Gets the total vehicle distance traveled for fillup entries.
        /// </summary>
        /// <remarks>
        /// This is a calculated value and should not be set directly.
        /// </remarks>
        public int TotalDistance
        {
            get { return totalDistance; }
        }

        /// <summary>
        /// Gets the total cost of all fillup entries, not including transaction fees.
        /// </summary>
        /// <remarks>
        /// This is a calculated value and should not be set directly.
        /// </remarks>
        public double TotalFuelCost
        {
            get { return RoundToTwoDecimals(totalFuelCost); }
        }

        /// <summary>
        /// Gets the total units consumed based on all fillup entries.
        /// </summary>
        /// <remarks>
        /// This is a calculated value and should not be set directly.
        /// </remarks>
        public double TotalUnits
        {
            get { return RoundToTwoDecimals(totalUnits); }
        }

        /// <summary>
        /// Gets the total cost of all fillup entries including transaction fees and service entries.
        /// </summary>
        /// <remarks>
        /// This is a calculated value and should not be set directly.
        /// </remarks>
        public double TotalCost
        {
            get { return RoundToTwoDecimals(totalCost); }
        }

        private static double RoundToTwoDecimals(double number)
        {
            return Math.Round(number, 2);
        }
    }
}