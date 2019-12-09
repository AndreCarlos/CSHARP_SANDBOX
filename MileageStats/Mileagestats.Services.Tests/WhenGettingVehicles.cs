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
using MileageStats.Domain.Handlers;
using MileageStats.Model;
using Moq;
using Xunit;

namespace MileageStats.Services.Tests
{
    public class WhenGettingVehicles
    {
        private readonly Mock<IVehicleRepository> _vehicleRepo;

        private const int UserId = 99;

        public WhenGettingVehicles()
        {
            _vehicleRepo = new Mock<IVehicleRepository>();
        }

        [Fact]
        public void ByUserIdForUserWithNoVehicles_ThenReturnsEmptyCollection()
        {
            _vehicleRepo
                .Setup(r => r.GetVehicles(UserId))
                .Returns(new Vehicle[] { })
                .Verifiable();

            var handler = new GetVehicleListForUser(_vehicleRepo.Object);

            var result = handler.Execute(UserId);

            _vehicleRepo.Verify();
            Assert.NotNull(result);
            Assert.Equal(0, result.Count());
        }

        [Fact]
        public void AndRepositoryThrows_ThenWrapsException()
        {
            _vehicleRepo
                .Setup(r => r.GetVehicles(UserId))
                .Throws<InvalidOperationException>();

            var handler = new GetVehicleListForUser(_vehicleRepo.Object);

            var exception = Assert.Throws<BusinessServicesException>(() => handler.Execute(UserId));

            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception.InnerException);
        }

        [Fact]
        public void ByUserIdForUser_ThenReturnsVehicles()
        {
            var vehicles = new List<Vehicle> { new Vehicle() };

            _vehicleRepo
                .Setup(vr => vr.GetVehicles(UserId))
                .Returns(vehicles);

            var result = new GetVehicleListForUser(_vehicleRepo.Object).Execute(UserId);

            Assert.Equal(vehicles.Count, result.Count());
        }

        [Fact]
        public void ByUserIdForUser_ThenReturnsVehiclesInSortedOrder()
        {
            var vehicle01 = new Vehicle { Name = "first", VehicleId = 4, SortOrder = 1 };
            var vehicle02 = new Vehicle { Name = "second", VehicleId = 1, SortOrder = 3 };
            var vehicle03 = new Vehicle { Name = "third", VehicleId = 2, SortOrder = 2 };

            var vehicles = new List<Vehicle> { vehicle03, vehicle02, vehicle01 };

            _vehicleRepo
                .Setup(vr => vr.GetVehicles(UserId))
                .Returns(vehicles);

            var handler = new GetVehicleListForUser(_vehicleRepo.Object);

            var result = handler.Execute(UserId);
            var acutal = result.ToArray();

            Assert.Equal(vehicle01.Name, acutal[0].Name);
            Assert.Equal(vehicle03.Name, acutal[1].Name);
            Assert.Equal(vehicle02.Name, acutal[2].Name);
        }
    }
}