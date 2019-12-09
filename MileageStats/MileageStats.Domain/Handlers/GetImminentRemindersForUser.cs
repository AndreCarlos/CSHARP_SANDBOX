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
using MileageStats.Domain.Models;
using MileageStats.Model;

namespace MileageStats.Domain.Handlers
{
    public class GetImminentRemindersForUser
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IReminderRepository _reminderRepository;

        private const int UpcomingReminderOdometerTolerance = 500;
        private const int UpcomingReminderDaysTolerance = 15;

        public GetImminentRemindersForUser(IVehicleRepository vehicleRepository, IReminderRepository reminderRepository)
        {
            _vehicleRepository = vehicleRepository;
            _reminderRepository = reminderRepository;
        }

        public virtual IEnumerable<ImminentReminderModel> Execute(int userId, DateTime dueDate, int selectedVehicle = 0)
        {
            var vehicles = _vehicleRepository.GetVehicles(userId);

            var reminders = new List<AggregatedData>();

            //NOTE: the structure of the data forces us into a SELECT N+1 scenario
            foreach (var vehicle in vehicles)
            {
                var stats = CalculateStatistics.Calculate(vehicle.Fillups);
                var odometer = stats.Odometer ?? 0;

                var overdue = _reminderRepository
                    .GetOverdueReminders(vehicle.VehicleId, dueDate, odometer)
                    .Select(r => new AggregatedData
                                {
                                    IsOverdue = true,
                                    Reminder = r,
                                    Vehicle = vehicle
                                })
                     .ToList();

                var end = dueDate.AddDays(UpcomingReminderDaysTolerance).Date;
                var start = dueDate.Date;
                
                var upcoming =  _reminderRepository
                    .GetUpcomingReminders(vehicle.VehicleId, start, end, odometer, UpcomingReminderOdometerTolerance)
                    .Select(r => new AggregatedData
                                {
                                    IsOverdue = false,
                                    Reminder = r,
                                    Vehicle = vehicle
                                })
                      // we need to exclude reminders that qualify as both overdue and upcoming
                      // performing this check in memory is less than desirable
                     .Where( r=> !overdue.Any(o => o.Reminder.ReminderId == r.Reminder.ReminderId))
                     .ToList();

                reminders.AddRange(overdue);
                reminders.AddRange(upcoming);
            }

            var result = reminders
                .Select(x => new ImminentReminderModel(x.Vehicle, x.Reminder, x.IsOverdue))
                .ToList();

                result.Sort(ImminentReminderModel.CompareImminentReminders);

            return result;
        }

        class AggregatedData
        {
            public Reminder Reminder { get; set; }
            public Vehicle Vehicle { get; set; }
            public bool IsOverdue { get; set; }
        }
    }
}