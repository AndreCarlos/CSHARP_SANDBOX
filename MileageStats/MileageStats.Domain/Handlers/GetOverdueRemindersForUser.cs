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
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Models;
using MileageStats.Domain.Properties;
using MileageStats.Model;

namespace MileageStats.Domain.Handlers
{
    public class GetOverdueRemindersForUser
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IReminderRepository _reminderRepository;

        public GetOverdueRemindersForUser(IVehicleRepository vehicleRepository, IReminderRepository reminderRepository)
        {
            _vehicleRepository = vehicleRepository;
            _reminderRepository = reminderRepository;
        }

        static ReminderModel ToViewModel(Reminder source)
        {
            if (source == null)
            {
                return null;
            }

            return new ReminderModel
                       {
                           DueDate = source.DueDate,
                           DueDistance = source.DueDistance,
                           Remarks = source.Remarks,
                           ReminderId = source.ReminderId,
                           Title = source.Title,
                           VehicleId = source.VehicleId,
                           IsFulfilled = source.IsFulfilled
                       };
        }

        public virtual IEnumerable<ReminderModel> Execute(int userId)
        {
            // this results in a SELECT N+1 scenario
            try
            {
                var vehicles = _vehicleRepository.GetVehicles(userId);

                var reminders = (from vehicle in vehicles
                                 let stats = CalculateStatistics.Calculate(vehicle.Fillups)
                                 let odometer = stats.Odometer
                                 from reminder in _reminderRepository.GetRemindersForVehicle(vehicle.VehicleId)
                                 select new {viewmodel = ToViewModel(reminder), odometer})
                                .ToList();

                foreach (var item in reminders)
                {
                    item.viewmodel.UpdateLastVehicleOdometer(item.odometer);
                }

                var overdueReminders = reminders
                    .Select( x=> x.viewmodel)
                    .Where(f => !f.IsFulfilled && f.IsOverdue)
                    .OrderBy(f => f.DueDistance)
                    .ThenBy(f => f.DueDate)
                    .ToList();

                return overdueReminders.ToList();
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.UnableToRetrieveOverdueReminders, ex);
            }
        }
    }
}