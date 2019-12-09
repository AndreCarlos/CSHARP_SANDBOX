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
using MileageStats.Domain.Models;
using MileageStats.Model;
using MileageStats.Web.Models;
using Moq;
using Xunit;

namespace MileageStats.Services.Tests
{
    public class WhenAddingFillup
    {
        private readonly Mock<IVehicleRepository> _vehicleRepo;
        private readonly Mock<IFillupRepository> _fillupRepositoryMock;
        private readonly Vehicle _vehicle;

        private const int DefaultUserId = 99;
        private const int DefaultVehicleId = 88;

        public WhenAddingFillup()
        {
            _vehicleRepo = new Mock<IVehicleRepository>();
            _fillupRepositoryMock = new Mock<IFillupRepository>();
            _vehicle = new Vehicle { VehicleId = DefaultVehicleId, Name = "vehicle" };
        }

        [Fact]
        public void WhenAddingFillup_ThenDelegatesToFillupRepository()
        {
            _vehicleRepo
                .Setup(r => r.GetVehicle(DefaultUserId, DefaultVehicleId))
                .Returns(_vehicle);

            var fillupForm = new FillupEntryFormModel();

            var handler = new AddFillupToVehicle(_vehicleRepo.Object, _fillupRepositoryMock.Object);
            handler.Execute(DefaultUserId, DefaultVehicleId, fillupForm);

            _fillupRepositoryMock
                .Verify(r => r.Create(DefaultUserId, DefaultVehicleId, It.IsAny<FillupEntry>()),
                Times.Once());
        }

        [Fact]
        public void WhenAddingFillup_ThenCalculatesDistance()
        {
            _vehicleRepo
                .Setup(r => r.GetVehicle(DefaultUserId, DefaultVehicleId))
                .Returns(_vehicle);

            var fillups = new[]
                {
                    new FillupEntry{FillupEntryId = 1, Date = new DateTime(2011, 3, 23), Odometer = 500},
                    new FillupEntry{FillupEntryId = 2, Date = new DateTime(2011, 3, 24), Odometer = 750}
                };

            var fillupForm = new FillupEntryFormModel
                                 {
                                     FillupEntryId = 3,
                                     Date = new DateTime(2011, 3, 26),
                                     Odometer = 1000
                                 };

            _fillupRepositoryMock
                .Setup(x => x.GetFillups(DefaultVehicleId))
                .Returns(fillups);

            var handler = new AddFillupToVehicle(_vehicleRepo.Object, _fillupRepositoryMock.Object);
            handler.Execute(DefaultUserId, DefaultVehicleId, fillupForm);

            Assert.Equal(250, fillupForm.Distance);
        }

        [Fact]
        public void WhenAddingFillupOnSameDate_ThenCalculatesDistance()
        {
            _vehicleRepo
                .Setup(r => r.GetVehicle(DefaultUserId, DefaultVehicleId))
                .Returns(_vehicle);

            var fillups = new[]
                              {
                                  new FillupEntry{FillupEntryId = 1, Date = new DateTime(2011, 3, 25), Odometer = 500},
                                  new FillupEntry{FillupEntryId = 2, Date = new DateTime(2011, 3, 25), Odometer = 750}
                              };

            var fillupForm = new FillupEntryFormModel
            {
                FillupEntryId = 3,
                Date = new DateTime(2011, 3, 25),
                Odometer = 1000
            };

            _fillupRepositoryMock
                 .Setup(x => x.GetFillups(DefaultVehicleId))
                 .Returns(fillups);

            var handler = new AddFillupToVehicle(_vehicleRepo.Object, _fillupRepositoryMock.Object);
            handler.Execute(DefaultUserId, DefaultVehicleId, fillupForm);

            Assert.Equal(250, fillupForm.Distance);
        }

        [Fact]
        public void WhenAddingFirstFillup_ThenDoesNotCalculatesDistance()
        {
            _vehicleRepo
                .Setup(r => r.GetVehicle(DefaultUserId, DefaultVehicleId))
                .Returns(_vehicle);

            var fillups = new FillupEntry[] {};

            var fillupForm = new FillupEntryFormModel { FillupEntryId = 3, Date = new DateTime(2011, 3, 25), Odometer = 1000 };

            _fillupRepositoryMock
                 .Setup(x => x.GetFillups(DefaultVehicleId))
                 .Returns(fillups);

            var handler = new AddFillupToVehicle(_vehicleRepo.Object, _fillupRepositoryMock.Object);
            handler.Execute(DefaultUserId, DefaultVehicleId, fillupForm);

            Assert.Null(fillupForm.Distance);
        }

        [Fact]
        public void WhenAddingFillupAndVehicleRepositoryThrows_ThenWrapsException()
        {
            _vehicleRepo
                .Setup(r => r.GetVehicle(DefaultUserId, DefaultVehicleId))
                .Throws<InvalidOperationException>();

            var handler = new AddFillupToVehicle(_vehicleRepo.Object, _fillupRepositoryMock.Object);

            var ex = Assert
                .Throws<BusinessServicesException>(() => handler.Execute(DefaultUserId, DefaultVehicleId, new FillupEntryFormModel()));
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

        [Fact]
        public void WhenAddingFillupAndFillupRepositoryThrows_ThenWrapsException()
        {
            _vehicleRepo
                .Setup(r => r.GetVehicle(DefaultUserId, DefaultVehicleId))
                .Returns(_vehicle);

            _fillupRepositoryMock
                .Setup(f => f.Create(DefaultUserId,DefaultVehicleId, It.IsAny<FillupEntry>()))
                .Throws<InvalidOperationException>();

            var handler = new AddFillupToVehicle(_vehicleRepo.Object, _fillupRepositoryMock.Object);
            var ex = Assert
                .Throws<BusinessServicesException>(() => handler.Execute(DefaultUserId, DefaultVehicleId, new FillupEntryFormModel()));
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }
    }
}