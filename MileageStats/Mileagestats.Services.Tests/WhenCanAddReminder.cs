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
using MileageStats.Data;
using MileageStats.Domain.Handlers;
using MileageStats.Domain.Models;
using MileageStats.Model;
using MileageStats.Web.Models;
using Moq;
using Xunit;

namespace MileageStats.Services.Tests
{
    public class WhenCanAddReminder
    {
        private const int DefaultUserId = 99;
        private const int DefaultVehicleId = 77;
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;

        public WhenCanAddReminder()
        {
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        }

        [Fact]
        public void WhenCanAddReminder_ThenReturnsEmptyCollection()
        {
            var vehicle1 = new Vehicle { VehicleId = DefaultVehicleId, Name = "vehicle1" };

            _vehicleRepositoryMock
                .Setup(r => r.GetVehicle(DefaultUserId, DefaultVehicleId))
                .Returns(vehicle1);

            var handler = new CanAddReminder(_vehicleRepositoryMock.Object);

            var formModel = new ReminderFormModel { VehicleId = DefaultVehicleId, Title = "Test", DueDistance = 20000 };

            var result = handler.Execute(DefaultUserId, formModel);

            Assert.Empty(result);
        }

        [Fact]
        public void WhenCanAddReminderWithInvalidVehicleId_ThenReturnsValidationResult()
        {
            const int nonExistentVehicleId = -1;

            _vehicleRepositoryMock
                .Setup(vr => vr.GetVehicle(DefaultUserId, nonExistentVehicleId))
                .Returns((Vehicle)null);

            var handler = new CanAddReminder(_vehicleRepositoryMock.Object);

            var formModel = new ReminderFormModel { VehicleId = nonExistentVehicleId, Title = "Test", DueDistance = 20000 };

            var result = handler.Execute(DefaultUserId, formModel);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void WhenCanAddReminderWithInvalidDueDistance_ThenReturnsValidationResult()
        {
            var vehicle1 = new Vehicle
                               {
                                   VehicleId = DefaultVehicleId,
                                   Name = "vehicle1",
                                   Fillups = new[] { new FillupEntry { Odometer = 7000 } }
                               };

            _vehicleRepositoryMock
                .Setup(vr => vr.GetVehicle(DefaultUserId, DefaultVehicleId))
                .Returns(vehicle1);

            var handler = new CanAddReminder(_vehicleRepositoryMock.Object);

            var formModel = new ReminderFormModel { VehicleId = DefaultVehicleId, Title = "Test", DueDistance = 5000 };

            var result = handler.Execute(DefaultUserId, formModel);

            Assert.NotEmpty(result);
        }
    }
}