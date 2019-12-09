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
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Properties;
using MileageStats.Model;

namespace MileageStats.Domain.Handlers
{
    public class AddReminderToVehicle
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IReminderRepository _reminderRepository;

        public AddReminderToVehicle(IVehicleRepository vehicleRepository, IReminderRepository reminderRepository)
        {
            _vehicleRepository = vehicleRepository;
            _reminderRepository = reminderRepository;
        }

        public virtual void Execute(int userId, int vehicleId, ICreateReminderCommand reminder)
        {
            if (reminder == null) throw new ArgumentNullException("reminder");
            try
            {
                var vehicle = _vehicleRepository.GetVehicle(userId, vehicleId);
                if (vehicle != null)
                {
                    reminder.VehicleId = vehicleId;
                    var entity = ToEntity(reminder);

                    _reminderRepository.Create(vehicleId, entity);

                    // Update reminder fields
                    reminder.ReminderId = entity.ReminderId;
                    reminder.VehicleId = entity.VehicleId;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.UnableToAddReminderToVehicleExceptionMessage, ex);
            }
        }

        static Reminder ToEntity(ICreateReminderCommand source)
        {
            if (source == null)
            {
                return null;
            }

            return new Reminder
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
    }
}