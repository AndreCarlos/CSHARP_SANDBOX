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
    public class JsonReminderViewModel
    {
        /// <summary>
        /// Gets or sets the entity ID of reminder.
        /// </summary>
        /// <value>
        /// An integer identifying the entity.
        /// </value>
        public int ReminderId { get; set; }

        /// <summary>
        /// Gets or sets the vehicle the reminder is for.
        /// </summary>
        /// <value>
        /// An integer identifying the entity.
        /// </value>
        public int VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the vehicle name the reminder is for.
        /// </summary>
        /// <value>
        /// The vehicle's name.
        /// </value>
        public string VehicleName { get; set; }

        /// <summary>
        /// Gets or sets the title of the reminder.
        /// </summary>
        /// <value>
        /// A string.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the remarks for the reminder.
        /// </summary>
        /// <value>
        /// A string.
        /// </value>        
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the date the reminder is due.
        /// </summary>
        /// <value>
        /// A DateTime (UTC) or null.
        /// </value>
        public string DueDate { get; set; }

        /// <summary>
        /// Gets or sets the distance the reminder is due when reached
        /// </summary>
        /// <value>
        /// A number or null.
        /// </value>
        public int? DueDistance { get; set; }

        /// <summary>
        /// Gets a value indicating whether the reminder has been fulfilled.
        /// </summary>
        /// <value>
        /// <c>true</c> if fulfilled; otherwise, <c>false</c>.
        /// </value>
        public bool IsFulfilled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the reminder is overdue (cached value based on vehicle odometer)
        /// </summary>
        /// <value>
        ///   <c>true</c> if is overdue; otherwise, <c>false</c>.
        /// </value>
        public bool IsOverdue { get; set; }
    }
}