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
    public class ReminderModel
    {
        private int? lastVehicleOdometer;

        public bool IsOverdue
        {
            get
            {
                int odometer = this.lastVehicleOdometer == null ? 0 : (int) this.lastVehicleOdometer;
                return ((this.IsFulfilled == false)
                        && ((this.DueDate.HasValue && this.DueDate.Value.Date <= DateTime.UtcNow.Date)
                            || (this.DueDistance.HasValue && this.DueDistance <= odometer)));
            }
        }

        public int ReminderId { get; set; }

        public int VehicleId { get; set; }

        public string Title { get; set; }

        public string Remarks { get; set; }

        public DateTime? DueDate { get; set; }

        public int? DueDistance { get; set; }

        public bool IsFulfilled { get; set; }

        public void UpdateLastVehicleOdometer(int? odometer)
        {
            this.lastVehicleOdometer = odometer;
        }
    }
}