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

namespace MileageStats.Model
{
    /// <summary>
    /// A vehicle used to track mileage statistics.
    /// </summary>
    public class Vehicle
    {
        public Vehicle()
        {
            this.Fillups = new List<FillupEntry>();
            this.Reminders = new List<Reminder>();
        }

        /// <summary>
        /// Gets or sets the entity ID of vehicle.
        /// </summary>
        /// <value>
        /// An integer identifying the entity.
        /// </value>
        public int VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the entity ID of user who owns the vehicle.
        /// </summary>
        /// <value>
        /// An integer identifying the entity.
        /// </value>
        public int UserId { get; set; }

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
        /// A positive number or zero.
        /// </value>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the photo of the vehicle.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        public VehiclePhoto Photo { get; set; }

        /// <summary>
        /// Gets or sets the entity ID of photo for the vehicle.
        /// </summary>
        /// <value>
        /// An integer identifying the entity.
        /// </value>
        public int PhotoId { get; set; }

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
        /// The fillups associated with a vehicle.
        /// </summary>
        /// <remarks>
        /// Depending on the method of retrieval used from the repository, this property may not necessarily get filled.
        /// </remarks>
        public ICollection<FillupEntry> Fillups { get; set; }

        /// <summary>
        /// The reminders for this vehicle.  Depending on the method of retrieval from the repository, this property may not necessarily get filled.
        /// </summary>
        public ICollection<Reminder> Reminders { get; set; }
    }
}