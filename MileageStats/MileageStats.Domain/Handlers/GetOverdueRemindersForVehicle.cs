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
using System.Linq;
using MileageStats.Data;
using MileageStats.Domain.Models;

namespace MileageStats.Domain.Handlers
{
    public class GetOverdueRemindersForVehicle
    {
        private readonly IReminderRepository _reminderRepository;

        public GetOverdueRemindersForVehicle(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public virtual IEnumerable<ReminderSummaryModel> Execute(int vehicleId, System.DateTime dueDate, int odometer)
        {
            return from reminder in _reminderRepository.GetOverdueReminders(vehicleId, dueDate, odometer)
                   orderby reminder.DueDistance
                   orderby reminder.DueDate
                   select new ReminderSummaryModel(reminder, isOvedue:true);
        }
    }
}