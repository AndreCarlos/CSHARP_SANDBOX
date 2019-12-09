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
using System.Data;
using System.Linq;
using MileageStats.Model;

namespace MileageStats.Data.SqlCe.Repositories
{
    public class ReminderRepository : BaseRepository, IReminderRepository
    {
        public ReminderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(int vehicleId, Reminder reminder)
        {
            reminder.VehicleId = vehicleId;
            this.GetDbSet<Reminder>().Add(reminder);
            this.UnitOfWork.SaveChanges();
        }

        public Reminder GetReminder(int reminderId)
        {
            return this.GetDbSet<Reminder>()
                .Where(v => v.ReminderId == reminderId)
                .Single();
        }

        public void Update(Reminder updatedReminder)
        {
            this.GetDbSet<Reminder>().Attach(updatedReminder);
            this.SetEntityState(updatedReminder, updatedReminder.VehicleId == 0
                                                     ? EntityState.Added
                                                     : EntityState.Modified);
            this.UnitOfWork.SaveChanges();
        }

        public void Delete(int reminderId)
        {
            Reminder reminderToDelete = this.GetReminder(reminderId);
            this.GetDbSet<Reminder>().Remove(reminderToDelete);
            this.UnitOfWork.SaveChanges();
        }

        public IEnumerable<Reminder> GetOverdueReminders(int vehicleId, DateTime forDate, int odometer)
        {
            return this.GetDbSet<Reminder>()
                    .Where( r => (r.VehicleId == vehicleId) && (r.DueDate < forDate || r.DueDistance < odometer) && !r.IsFulfilled)
                    .ToList();
        }

        public IEnumerable<Reminder> GetUpcomingReminders(int vehicleId, DateTime forStartDate, DateTime forEndDate,
                                                          int odometer, int warningOdometer)
        {
            return
                this.GetDbSet<Reminder>().Where(
                    r => (r.VehicleId == vehicleId) && !r.IsFulfilled &&
                         ((r.DueDate > forStartDate && r.DueDate < forEndDate) ||
                          (r.DueDistance > odometer && r.DueDistance <= odometer + warningOdometer)))
                          .ToList();
        }

        public IEnumerable<Reminder> GetRemindersForVehicle(int vehicleId)
        {
            return this.GetDbSet<Reminder>()
                .Where(v => v.VehicleId == vehicleId)
                .ToList();
        }
    }
}
