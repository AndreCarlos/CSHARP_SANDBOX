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
using MileageStats.Model;
using Moq;
using Xunit;

namespace MileageStats.Services.Tests
{
    public class WhenUpdatingVehicleSortOrder
    {
        private const int UserId = 99;
        private readonly Mock<IVehicleRepository> _vehicleRepo;

        public WhenUpdatingVehicleSortOrder()
        {
            _vehicleRepo = new Mock<IVehicleRepository>();
        }

        [Fact]
        public void ThenDelegatesToVehicleRepositoryForEachVehicle()
        {
            var vehicleOne = new Vehicle {VehicleId = 1, Name = "oldName", SortOrder = 0};
            var vehicleTwo = new Vehicle {VehicleId = 2, Name = "oldName", SortOrder = 1};
            var vehicleThree = new Vehicle {VehicleId = 3, Name = "oldName", SortOrder = 2};

            _vehicleRepo.Setup(r => r.GetVehicle(It.IsAny<int>(), 1)).Returns(vehicleOne);
            _vehicleRepo.Setup(r => r.GetVehicle(It.IsAny<int>(), 2)).Returns(vehicleTwo);
            _vehicleRepo.Setup(r => r.GetVehicle(It.IsAny<int>(), 3)).Returns(vehicleThree);

            var newOrder = new[] {3, 2, 1};

            var handler = new UpdateVehicleSortOrder(_vehicleRepo.Object);
            handler.Execute(UserId, newOrder);

            _vehicleRepo.Verify(r => r.Update(vehicleOne), Times.Once());
            _vehicleRepo.Verify(r => r.Update(vehicleTwo), Times.Once());
            _vehicleRepo.Verify(r => r.Update(vehicleThree), Times.Once());
        }

        [Fact]
        public void ThenUpdatesSortOrderForEachVehicle()
        {
            var vehicleOne = new Vehicle {VehicleId = 1, Name = "oldName", SortOrder = 0};
            var vehicleTwo = new Vehicle {VehicleId = 2, Name = "oldName", SortOrder = 1};
            var vehicleThree = new Vehicle {VehicleId = 3, Name = "oldName", SortOrder = 2};

            _vehicleRepo.Setup(vr => vr.GetVehicle(It.IsAny<int>(), 1)).Returns(vehicleOne);
            _vehicleRepo.Setup(vr => vr.GetVehicle(It.IsAny<int>(), 2)).Returns(vehicleTwo);
            _vehicleRepo.Setup(vr => vr.GetVehicle(It.IsAny<int>(), 3)).Returns(vehicleThree);

            var newOrder = new[] {3, 2, 1};
            var handler = new UpdateVehicleSortOrder(_vehicleRepo.Object);
            handler.Execute(UserId, newOrder);

            Assert.Equal(2, vehicleOne.SortOrder);
            Assert.Equal(1, vehicleTwo.SortOrder);
            Assert.Equal(0, vehicleThree.SortOrder);
        }

        [Fact]
        public void AndRepositoryFails_ThenThrowsBusinessServicesException()
        {
            _vehicleRepo
                .Setup(r => r.GetVehicle(UserId, It.IsAny<int>()))
                .Returns(new Vehicle());

            _vehicleRepo
                .Setup(b => b.Update(It.IsAny<Vehicle>()))
                .Throws(new InvalidOperationException());

            var newOrder = new[] {3, 2, 1};

            var handler = new UpdateVehicleSortOrder(_vehicleRepo.Object);

            var exception = Assert.Throws<BusinessServicesException>(() => handler.Execute(UserId, newOrder));
            Assert.IsType<InvalidOperationException>(exception.InnerException);
        }
    }
}