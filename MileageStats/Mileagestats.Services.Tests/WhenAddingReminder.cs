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
using MileageStats.Domain.Handlers;
using MileageStats.Domain.Models;
using MileageStats.Model;
using MileageStats.Web.Models;
using Moq;
using Xunit;

namespace MileageStats.Services.Tests
{
    public class WhenAddingReminder
    {
        private const int DefaultUserId = 99;
        private const int DefaultVehicleId = 77;
        private const int CurrentOdometer = 10000;
        private readonly Mock<IReminderRepository> _reminderRepository;
        private readonly Mock<IVehicleRepository> _vehicleRepository;

        public WhenAddingReminder()
        {
            _vehicleRepository = new Mock<IVehicleRepository>();
            _reminderRepository = new Mock<IReminderRepository>();
        }

        [Fact]
        public void WhenAddingReminder_ThenDelegatesToReminderRepository()
        {
            var vehicle = new Vehicle { VehicleId = DefaultVehicleId, Name = "vehicle" };

            _vehicleRepository
                .Setup(r => r.GetVehicle(DefaultUserId, DefaultVehicleId))
                .Returns(vehicle);

            var handler = new AddReminderToVehicle(_vehicleRepository.Object, _reminderRepository.Object);
            handler.Execute(DefaultUserId, DefaultVehicleId, new ReminderFormModel());

            _reminderRepository
                .Verify(r => r.Create(DefaultVehicleId, It.IsAny<Reminder>()), Times.Once());
        }

        [Fact]
        public void WhenAddingReminder_ThenUpdatesServiceReminder()
        {
            const int newReminderId = 456;

            var vehicle = new Vehicle { VehicleId = DefaultVehicleId, Name = "vehicle" };

            _vehicleRepository
                .Setup(r => r.GetVehicle(DefaultUserId, DefaultVehicleId))
                .Returns(vehicle);

            _reminderRepository
                .Setup(r => r.Create(DefaultVehicleId, It.IsAny<Reminder>()))
                .Callback(new Action<int, Reminder>((vehicleId, reminder) =>
                                                        {
                                                            // represents the entity created internally
                                                            reminder.ReminderId = newReminderId;
                                                            reminder.VehicleId = DefaultVehicleId;
                                                        }));

            var formModel = new ReminderFormModel();

            var handler = new AddReminderToVehicle(_vehicleRepository.Object, _reminderRepository.Object);
            handler.Execute(DefaultUserId, DefaultVehicleId, formModel);

            Assert.Equal(newReminderId, formModel.ReminderId);
            Assert.Equal(DefaultVehicleId, formModel.VehicleId);
        }
    }
}