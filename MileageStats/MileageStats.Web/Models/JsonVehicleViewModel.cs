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
using System.Collections.Generic;
using MileageStats.Domain.Models;

namespace MileageStats.Web.Models
{
    public class JsonVehicleViewModel
    {
        /// <summary>
        /// Gets or sets the entity ID of vehicle.
        /// </summary>
        /// <value>
        /// An integer identifying the entity.
        /// </value>
        public int VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vehicle.
        /// </summary>
        /// <value>
        /// A string.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the sort order relative to other vehicles.
        /// </summary>
        /// <value>
        /// A positive number up to 10,000 or zero.
        /// </value>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the manufacturing year of the vehicle.
        /// </summary>
        /// <value>
        /// An integer after 1896.
        /// </value>    
        public int? Year { get; set; }

        /// <summary>
        /// Gets or sets the make of the vehicle (e.g. Toyota, Ford).
        /// </summary>
        /// <value>
        /// A string.
        /// </value>
        public string MakeName { get; set; }

        /// <summary>
        /// Gets or sets the model of the vehicle (e.g. Camry, Fiesta)
        /// </summary>
        /// <value>
        /// A string.
        /// </value>
        public string ModelName { get; set; }

        /// <summary>
        /// Gets the highest odometer value recorded for the vehicle.
        /// </summary>
        public int? Odometer { get; set; }

        /// <summary>
        /// The id of the vehicle's photo
        /// </summary>
        public int PhotoId { get; set; }

        /// <summary>
        /// Gets the statistics for the entire lifetime of this vehicle.
        /// </summary>
        public JsonStatisticsViewModel LifeTimeStatistics { get; set; }

        /// <summary>
        /// Gets the statistics for the last 12 months for this vehicle.
        /// </summary>
        public JsonStatisticsViewModel LastTwelveMonthsStatistics { get; set; }

        /// <summary>
        /// Gets or sets the overdue reminders for this vehicle.
        /// </summary>    
        public IEnumerable<ReminderSummaryModel> OverdueReminders { get; set; }
    }
}