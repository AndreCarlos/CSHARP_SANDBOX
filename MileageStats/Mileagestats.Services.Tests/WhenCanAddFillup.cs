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
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Handlers;
using MileageStats.Domain.Models;
using MileageStats.Model;
using MileageStats.Web.Models;
using Moq;
using Xunit;

namespace MileageStats.Services.Tests
{
    public class WhenCanAddFillup
    {
        private readonly Mock<IVehicleRepository> _vehicleRepo;
        private readonly Mock<IFillupRepository> _fillupRepo;
        private const int DefaultUserId = 99;
        private const int DefaultVehicleId = 88;

        public WhenCanAddFillup()
        {
            _vehicleRepo = new Mock<IVehicleRepository>();
            _fillupRepo = new Mock<IFillupRepository>();
        }

        [Fact]
        public void WhenCanAddFillup_ThenReturnsEmptyCollection()
        {
            var fillUp = new FillupEntryFormModel
                             {
                                 Date = DateTime.UtcNow,
                                 TotalUnits = 10,
                                 PricePerUnit = 1.0,
                             };

            _vehicleRepo
                .Setup(vr => vr.GetVehicle(DefaultUserId, DefaultVehicleId))
                .Returns(new Vehicle { VehicleId = DefaultVehicleId, Name = "Vehicle" });

            _fillupRepo
                .Setup(x => x.GetFillups(It.IsAny<int>()))
                .Returns(new List<FillupEntry>());

            var handler = new CanAddFillup(_vehicleRepo.Object, _fillupRepo.Object);
            var actual = handler.Execute(DefaultUserId, DefaultVehicleId, fillUp);

            Assert.Empty(actual);
        }

        [Fact]
        public void WhenCanAddFillupWithInvalidVehicleId_ThenReturnsValidationResult()
        {
            var fillUp = new FillupEntryFormModel
                             {
                                 Date = DateTime.UtcNow,
                                 TotalUnits = 10,
                                 PricePerUnit = 1.0,
                             };

            var fillUps = new List<FillupEntry>();
            _vehicleRepo
                .Setup(r => r.GetVehicle(DefaultUserId, DefaultVehicleId))
                .Returns((Vehicle)null);

            _fillupRepo
                .Setup(x => x.GetFillups(DefaultVehicleId))
                .Returns(fillUps);

            var handler = new CanAddFillup(_vehicleRepo.Object, _fillupRepo.Object);
            var actual = handler.Execute(DefaultUserId, DefaultVehicleId, fillUp);

            var actualList = new List<ValidationResult>(actual);
            Assert.Equal(1, actualList.Count);
            Assert.Contains("not found", actualList[0].Message, StringComparison.CurrentCultureIgnoreCase);
        }

        [Fact]
        public void WhenCanAddFillupWithInvalidFillupOdometer_ThenReturnsValidationResult()
        {
            var fillUp = new FillupEntryFormModel
                             {
                                 Date = DateTime.UtcNow,
                                 TotalUnits = 10,
                                 PricePerUnit = 1.0,
                                 Odometer = 500 //less than prior fillup
                             };

            var fillUps = new List<FillupEntry>
                              {
                                  new FillupEntry
                                      {
                                          FillupEntryId = 1,
                                          Date = DateTime.UtcNow.AddDays(-1),
                                          Odometer = 1000
                                      }
                              };

            _vehicleRepo
                .Setup(vr => vr.GetVehicle(DefaultUserId, DefaultVehicleId))
                .Returns(new Vehicle { VehicleId = DefaultVehicleId, Name = "Vehicle" });

            _fillupRepo
                .Setup(x => x.GetFillups(DefaultVehicleId))
                .Returns(fillUps);

            var handler = new CanAddFillup(_vehicleRepo.Object, _fillupRepo.Object);
            var actual = handler.Execute(DefaultUserId, DefaultVehicleId, fillUp);

            var actualList = new List<ValidationResult>(actual);
            Assert.Equal(1, actualList.Count);
            Assert.Contains("odometer ", actualList[0].Message, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}