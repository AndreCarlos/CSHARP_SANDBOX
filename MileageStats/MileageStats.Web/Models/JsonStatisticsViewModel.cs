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
namespace MileageStats.Web.Models
{
    public class JsonStatisticsViewModel
    {
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
        public double AverageFillupPrice { get; set; }

        /// <summary>
        /// Gets the average fuel efficiency (e.g. Miles/Gallon or Kilomter / Litre)
        /// </summary>
        public double AverageFuelEfficiency { get; set; }

        /// <summary>
        /// Gets the average cost to drive per distance (e.g. $/Mile or €/Kilometer)
        /// </summary>
        public double AverageCostToDrive { get; set; }

        /// <summary>
        /// Gets the average cost to drive per month (e.g. $/Month or €/Month) between the first entry and today.
        /// </summary>
        public double AverageCostPerMonth { get; set; }

        /// <summary>
        /// Gets the highest odometer value recorded.
        /// </summary>
        public int? Odometer { get; set; }

        /// <summary>
        /// Gets the total vehicle distance traveled for fillup entries.
        /// </summary>
        public int TotalDistance { get; set; }

        /// <summary>
        /// Gets the total cost of all fillup entries, not including transaction fees.
        /// </summary>
        public double TotalFuelCost { get; set; }

        /// <summary>
        /// Gets the total units consumed based on all fillup entries.
        /// </summary>
        public double TotalUnits { get; set; }

        /// <summary>
        /// Gets the total cost of all fillup entries including transaction fees and service entries.
        /// </summary>
        public double TotalCost { get; set; }

    }
}