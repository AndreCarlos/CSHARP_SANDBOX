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
using MileageStats.Data;
using MileageStats.Domain.Handlers;
using MileageStats.Model;
using MileageStats.Web.Controllers;
using Moq;
using Xunit;

namespace MileageStats.Services.Tests
{
    public class WhenGettingVehicle
    {
        private readonly Mock<IVehicleRepository> _vehicleRepo;

        private const int UserId = 99;
        private const int DefaultVehicleId = 1;

        public WhenGettingVehicle()
        {
            _vehicleRepo = new Mock<IVehicleRepository>();
        }

        [Fact]
        public void ThenVehicleReturned()
        {
            var vehicle = new Vehicle { VehicleId = 1, Name = "vehicle" };

            _vehicleRepo
                .Setup(vr => vr.GetVehicle(UserId, vehicle.VehicleId))
                .Returns(vehicle);

            var handler = new GetVehicleById(_vehicleRepo.Object);

            var retrievedVehicle = handler.Execute(UserId, vehicle.VehicleId);

            Assert.NotNull(retrievedVehicle);
            Assert.Equal("vehicle", retrievedVehicle.Name);
        }

        [Fact]
        public void ThenVehiclePhotoIdSet()
        {
            const int photoId = 99;

            var vehicle = new Vehicle { VehicleId = DefaultVehicleId, Name = "vehicle", PhotoId = photoId };

            _vehicleRepo
                .Setup(vr => vr.GetVehicle(UserId, DefaultVehicleId))
                .Returns(vehicle);

            var handler = new GetVehicleById(_vehicleRepo.Object);

            var retrievedVehicle = handler.Execute(UserId, vehicle.VehicleId);

            Assert.Equal(photoId, retrievedVehicle.PhotoId);
        }

        [Fact]
        public void ForOtherUser_ThenNullReturned()
        {
            const int notTheCurrentUserId = UserId + 1;

            var vehicle = new Vehicle { VehicleId = DefaultVehicleId, Name = "vehicle" };

            _vehicleRepo
                .Setup(vr => vr.GetVehicle(notTheCurrentUserId, DefaultVehicleId))
                .Throws(new InvalidOperationException());

            var handler = new GetVehicleById(_vehicleRepo.Object);
            var retrievedVehicle = handler.Execute(notTheCurrentUserId, vehicle.VehicleId);

            Assert.Null(retrievedVehicle);
        }

        [Fact]
        public void WhenGettingVehicle_ThenVehicleStatisticsReturned()
        {
            var fillups = new List<FillupEntry>
                              {
                                  new FillupEntry
                                      {
                                          Date = DateTime.UtcNow.AddDays(-10),
                                          Odometer = 500,
                                          PricePerUnit = 10.0,
                                          TotalUnits = 10.0
                                      },
                                  new FillupEntry
                                      {
                                          Date = DateTime.UtcNow.AddDays(-5),
                                          Odometer = 1000,
                                          PricePerUnit = 10.0,
                                          TotalUnits = 10.0
                                      }
                              };

            var vehicle = new Vehicle
                              {
                                  VehicleId = DefaultVehicleId, 
                                  Fillups = fillups
                              };

            _vehicleRepo
                .Setup(vr => vr.GetVehicle(UserId, DefaultVehicleId))
                .Returns(vehicle);

            var handler = new GetVehicleById(_vehicleRepo.Object);
            var retrievedVehicle = handler.Execute(UserId, vehicle.VehicleId);

            Assert.NotNull(retrievedVehicle);
            Assert.Equal(1000, retrievedVehicle.Odometer);
        }
    }
}