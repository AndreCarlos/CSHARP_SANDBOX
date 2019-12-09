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
using MileageStats.Domain.Properties;
using MileageStats.Model;

namespace MileageStats.Domain.Handlers
{
    public class GetUnfulfilledRemindersForVehicle
    {
        private readonly IReminderRepository _reminderRepository;

        public GetUnfulfilledRemindersForVehicle(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public virtual IEnumerable<Reminder> Execute(int userId, int vehicleId, int odometer)
        {
            try
            {
                var reminders = _reminderRepository
                    .GetRemindersForVehicle(vehicleId)
                    .ToList();

                foreach (var reminder in reminders)
                {
                    reminder.CalculateIsOverdue(odometer);
                }

                var unfulfilledReminders = reminders
                    .Where(f => f.IsFulfilled == false)
                    .OrderBy(f => f.DueDistance)
                    .ThenBy(f => f.DueDate)
                    .ToList();

                return unfulfilledReminders.ToList();

            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.UnableToRetrieveUnfulfilledReminders, ex);
            }
        }
    }
}