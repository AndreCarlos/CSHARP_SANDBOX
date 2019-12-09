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
using System.Linq;
using MileageStats.Data.SqlCe.Repositories;
using MileageStats.Model;
using Xunit;

namespace MileageStats.Data.SqlCe.Tests.Repositories
{
    public class ReminderRepositoryFixture
    {
        private User defaultTestUser;
        private Vehicle defaultVehicle;

        public ReminderRepositoryFixture()
        {
            this.InitializeFixture();
        }

        private void InitializeFixture()
        {
            DatabaseTestUtility.DropCreateMileageStatsDatabase();
            this.defaultTestUser = new User()
                                       {
                                           AuthorizationId = "TestAuthorizationId",
                                           DisplayName = "DefaultTestUser"
                                       };

            var userRepository = new UserRepository(this.GetUnitOfWork());
            userRepository.Create(this.defaultTestUser);

            int userId = this.defaultTestUser.UserId;

            var vehicleRepository = new VehicleRepository(this.GetUnitOfWork());
            this.defaultVehicle = new Vehicle()
                                      {
                                          Name = "Test Vehicle"
                                      };
            vehicleRepository.Create(this.defaultTestUser.UserId, this.defaultVehicle);
        }

        [Fact]
        public void WhenConstructedWithNullUnitOfWork_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ReminderRepository(null));
        }

        [Fact]
        public void WhenCreatingReminder_ThenPersists()
        {
            var repository = new ReminderRepository(this.GetUnitOfWork());

            Reminder reminder = new Reminder()
                                    {
                                        DueDate = DateTime.UtcNow.AddDays(30),
                                        DueDistance = 1000,
                                        Title = "Test Reminder"
                                    };

            repository.Create(this.defaultVehicle.VehicleId, reminder);

            var repositoryForVerification = new ReminderRepository(this.GetUnitOfWork());
            var returnedReminder =
                repositoryForVerification.GetRemindersForVehicle(this.defaultVehicle.VehicleId).First();

            Assert.NotNull(returnedReminder);
            Assert.Equal("Test Reminder", returnedReminder.Title);
        }

        [Fact]
        public void WhenDeletingReminder_ThenPersists()
        {
            var repository = new ReminderRepository(this.GetUnitOfWork());

            Reminder reminder = new Reminder()
                                    {
                                        DueDate = DateTime.UtcNow.AddDays(30),
                                        DueDistance = 1000,
                                        Title = "Test Reminder"
                                    };

            repository.Create(this.defaultVehicle.VehicleId, reminder);

            var repositoryForDelete = new ReminderRepository(this.GetUnitOfWork());

            Assert.Equal(1, repositoryForDelete.GetRemindersForVehicle(this.defaultVehicle.VehicleId).Count());

            repositoryForDelete.Delete(reminder.ReminderId);

            var repositoryForVerification = new ReminderRepository(this.GetUnitOfWork());
            var returnedReminders = repositoryForVerification.GetRemindersForVehicle(this.defaultVehicle.VehicleId);

            Assert.NotNull(returnedReminders);
            Assert.Equal(0, returnedReminders.Count());
        }

        [Fact]
        public void WhenUpdatingReminder_ThenPersists()
        {
            var repository = new ReminderRepository(this.GetUnitOfWork());

            Reminder reminder = new Reminder()
                                    {
                                        DueDate = DateTime.UtcNow.AddDays(30),
                                        DueDistance = 1000,
                                        Title = "Test Reminder"
                                    };

            repository.Create(this.defaultVehicle.VehicleId, reminder);

            var repositoryForUpdate = new ReminderRepository(this.GetUnitOfWork());

            var reminderToUpdate = repositoryForUpdate.GetRemindersForVehicle(this.defaultVehicle.VehicleId).First();
            reminderToUpdate.Title = "updated";

            repositoryForUpdate.Update(reminderToUpdate);

            var repositoryForVerification = new ReminderRepository(this.GetUnitOfWork());
            var returnedReminder =
                repositoryForVerification.GetRemindersForVehicle(this.defaultVehicle.VehicleId).First();

            Assert.NotNull(returnedReminder);
            Assert.Equal("updated", returnedReminder.Title);
        }

        [Fact]
        public void WhenRetrievingUpcomingReminders_ThenRemindersInDueDistanceRangeRetrieved()
        {
            DateTime dateStart = DateTime.UtcNow;
            DateTime dateEnd = DateTime.UtcNow.AddDays(5);
            int odometer = 1000;
            int warningThreshold = 500;

            var repository = new ReminderRepository(this.GetUnitOfWork());

            // reminders just inside range
            var inRangeReminder1 = new Reminder()
                               {
                                   DueDate = dateEnd.AddDays(30),
                                   DueDistance = odometer + 1,
                                   Title = "UpcomingReminder"
                               };
            repository.Create(this.defaultVehicle.VehicleId, 
                                inRangeReminder1);

            var inRangeReminder2 = new Reminder()
                               {
                                   DueDate = dateEnd.AddDays(30),
                                   DueDistance = odometer + warningThreshold,
                                   Title = "UpcomingReminder1"
                               };
            repository.Create(this.defaultVehicle.VehicleId,
                    inRangeReminder2);

            // reminders just outside of range
            repository.Create(this.defaultVehicle.VehicleId,
                              new Reminder()
                              {
                                  DueDate = dateEnd.AddDays(30),
                                  DueDistance = odometer,
                                  Title = "OutsideRangeReminder1"
                              });

            repository.Create(this.defaultVehicle.VehicleId,
                              new Reminder()
                                  {
                                      DueDate = dateEnd.AddDays(30),
                                      DueDistance = odometer + warningThreshold + 1,
                                      Title = "OutsideRangeReminder2"
                                  });

            var reminders = repository.GetUpcomingReminders(
                this.defaultVehicle.VehicleId,
                dateStart,
                dateEnd,
                odometer,
                warningThreshold
                );

            Assert.Equal(2, reminders.Count());
            Assert.True(reminders.Any(r => r.ReminderId == inRangeReminder1.ReminderId));
            Assert.True(reminders.Any(r => r.ReminderId == inRangeReminder2.ReminderId));
        }

        [Fact]
        public void WhenRetrievingUpcomingReminders_ThenRemindersInDueDateRangeRetrieved()
        {
            DateTime dateStart = DateTime.UtcNow;
            DateTime dateEnd = DateTime.UtcNow.AddDays(5);
            int odometer = 1000;
            int warningThreshold = 500;
            int outsideOdometerRange = odometer + warningThreshold + 1;

            var repository = new ReminderRepository(this.GetUnitOfWork());

            // reminders just inside range
            var inRangeReminder1 = new Reminder()
                               {
                                   DueDate = dateStart.AddDays(1),
                                   DueDistance = outsideOdometerRange,
                                   Title = "UpcomingReminder"
                               };
            repository.Create(this.defaultVehicle.VehicleId,
                                inRangeReminder1);

            var inRangeReminder2 = new Reminder()
                                {
                                    DueDate = dateEnd.AddDays(-1),
                                    DueDistance = outsideOdometerRange,
                                    Title = "UpcomingReminder1"
                                };
            repository.Create(this.defaultVehicle.VehicleId,
                    inRangeReminder2);

            // reminders just outside of range
            repository.Create(this.defaultVehicle.VehicleId,
                              new Reminder()
                              {
                                  DueDate = dateStart.AddDays(-1),
                                  DueDistance = outsideOdometerRange,
                                  Title = "OutsideRangeReminder1"
                              });

            repository.Create(this.defaultVehicle.VehicleId,
                              new Reminder()
                              {
                                  DueDate = dateEnd.AddDays(1),
                                  DueDistance = outsideOdometerRange,
                                  Title = "OutsideRangeReminder2"
                              });

            var reminders = repository.GetUpcomingReminders(
                this.defaultVehicle.VehicleId,
                dateStart,
                dateEnd,
                odometer,
                warningThreshold
                );

            Assert.Equal(2, reminders.Count());
            Assert.True(reminders.Any(r => r.ReminderId == inRangeReminder1.ReminderId));
            Assert.True(reminders.Any(r => r.ReminderId == inRangeReminder2.ReminderId));

        }
        private IUnitOfWork GetUnitOfWork()
        {
            return new MileageStatsDbContext();
        }
    }
}