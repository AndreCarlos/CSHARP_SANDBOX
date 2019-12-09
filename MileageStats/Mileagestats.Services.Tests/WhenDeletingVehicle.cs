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
using MileageStats.Domain.Handlers;
using MileageStats.Web.Controllers;
using Moq;
using Xunit;

namespace MileageStats.Services.Tests
{
    public class WhenDeletingVehicle
    {
        private readonly Mock<IVehicleRepository> _vehicleRepo;

        private const int UserId = 99;
        private const int DefaultVehicleId = 1;

        public WhenDeletingVehicle()
        {
            _vehicleRepo = new Mock<IVehicleRepository>();
        }

        [Fact]
        public void ThenDelegatesToVehicleRepository()
        {
            var vehicle = new Model.Vehicle { VehicleId = DefaultVehicleId, Name = "Name" };

            _vehicleRepo
                .Setup(vr => vr.GetVehicle(UserId, DefaultVehicleId))
                .Returns(vehicle);

            var handler = new DeleteVehicle(_vehicleRepo.Object);
            handler.Execute(UserId, DefaultVehicleId);

            _vehicleRepo.Verify(r => r.Delete(DefaultVehicleId), Times.Once());
        }

        [Fact]
        public void FromOtherUser_ThenThrows()
        {
            const int someOtherUserId = 12;

            // the repo throws an exception when it can't find a match with both the user and the vehicle
            _vehicleRepo
                .Setup(vr => vr.GetVehicle(someOtherUserId, DefaultVehicleId))
                .Throws(new InvalidOperationException());

            var handler = new DeleteVehicle(_vehicleRepo.Object);

            Assert.Throws<BusinessServicesException>(() => handler.Execute(someOtherUserId, DefaultVehicleId));
        }

        [Fact]
        public void AndVehicleDoesNotExist_ThenThrows()
        {
            // this is the same test as FromOtherUser_ThenThrows

            // the repo throws an exception when it can't find a match with both the user and the vehicle
            _vehicleRepo
                .Setup(vr => vr.GetVehicle(UserId, It.IsAny<int>()))
                .Throws(new InvalidOperationException());

            var handler = new DeleteVehicle(_vehicleRepo.Object);

            Assert.Throws<BusinessServicesException>(() => handler.Execute(UserId, DefaultVehicleId));
        }

        [Fact]
        public void AndExceptionThrown_ThenWrapsException()
        {
            _vehicleRepo
                .Setup(vr => vr.GetVehicle(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new InvalidOperationException());

            var handler = new DeleteVehicle(_vehicleRepo.Object);

            var ex = Assert.Throws<BusinessServicesException>(() => handler.Execute(UserId, DefaultVehicleId));
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }
    }
}