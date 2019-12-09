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
using System.ComponentModel.DataAnnotations;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Properties;
using MileageStats.Domain.Validators;

namespace MileageStats.Web.Models
{
    /// <summary>
    /// A reminder for a user to service their vehicle.
    /// </summary>
    [AtLeastOneNonNullPropertyValidation("DueDate", "DueDistance", ErrorMessage = "Due Date or Due Distance is required.")]
    public class ReminderFormModel : ICreateReminderCommand
    {
        private int? lastVehicleOdometer;

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
        /// Gets or sets the title of the reminder.
        /// </summary>
        /// <value>
        /// A string.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ReminderTitleRequired", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(50, ErrorMessageResourceName = "ReminderTitleStringLengthValidationError", ErrorMessageResourceType = typeof(Resources))]
        [TextLineInputValidator]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the remarks for the reminder.
        /// </summary>
        /// <value>
        /// A string.
        /// </value>        
        [StringLength(250, ErrorMessageResourceName = "ReminderRemarksStringLengthValidationError", ErrorMessageResourceType = typeof(Resources))]
        [TextMultilineValidator]
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the date the reminder is due.
        /// </summary>
        /// <value>
        /// A DateTime (UTC) or null.
        /// </value>
        [Display(Name = "ReminderDueDateLabelText", ResourceType = typeof(Resources))]
        [StoreRestrictedDate]
        [FutureDate]
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the distance the reminder is due when reached
        /// </summary>
        /// <value>
        /// A number or null.
        /// </value>
        [Range(0, 1000000, ErrorMessageResourceName = "ReminderDueDistanceRangeValidationError", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "ReminderDueDistanceLabelText", ResourceType = typeof(Resources))]
        public int? DueDistance { get; set; }

        /// <summary>
        /// Gets a value indicating whether the reminder has been fulfilled.
        /// </summary>
        /// <value>
        /// <c>true</c> if fulfilled; otherwise, <c>false</c>.
        /// </value>
        public bool IsFulfilled { get; set; }

        /// <summary>
        /// Gets a value indicating whether the reminder is overdue (cached value based on vehicle odometer)
        /// </summary>
        /// <value>
        ///   <c>true</c> if is overdue; otherwise, <c>false</c>.
        /// </value>
        public bool IsOverdue
        {
            get
            {
                int odometer = this.lastVehicleOdometer == null ? 0 : (int)this.lastVehicleOdometer;
                return ((this.IsFulfilled == false)
                        && ((this.DueDate.HasValue && this.DueDate.Value.Date <= DateTime.UtcNow.Date)
                            || (this.DueDistance.HasValue && this.DueDistance <= odometer)));
            }
        }

        public void UpdateLastVehicleOdometer(int? odometer)
        {
            lastVehicleOdometer = odometer;
        }
    }
}