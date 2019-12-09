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
using System.Globalization;
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Properties;

namespace MileageStats.Domain.Handlers
{
    public class CanAddReminder
    {
        private readonly IVehicleRepository _vehicleRepository;

        public CanAddReminder(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public virtual IEnumerable<ValidationResult> Execute(int userId, ICreateReminderCommand reminder)
        {
            var foundVehicle = _vehicleRepository.GetVehicle(userId, reminder.VehicleId);

            if (foundVehicle == null)
            {
                yield return new ValidationResult(Resources.VehicleNotFound);
            }
            else
            {
                var stats = CalculateStatistics.Calculate(foundVehicle.Fillups);
                var odometer = stats.Odometer;

                if ((reminder.DueDistance.HasValue) && (reminder.DueDistance.Value <= odometer))
                {
                    yield return new ValidationResult(
                        "DueDistance",
                        string.Format(CultureInfo.CurrentUICulture, Resources.DueDistanceNotGreaterThanOdometer, odometer));
                }
            }
        }
    }
}