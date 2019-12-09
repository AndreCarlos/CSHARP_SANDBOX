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
    public class WhenCreatingVehicle
    {
        private readonly Mock<IVehicleRepository> _vehicleRepo;
        private readonly Mock<IVehiclePhotoRepository> _photoRepo;

        private const int UserId = 99;

        public WhenCreatingVehicle()
        {
            _vehicleRepo = new Mock<IVehicleRepository>();
            _photoRepo = new Mock<IVehiclePhotoRepository>();
        }

        [Fact]
        public void InvokesVehicleRepository()
        {
            var vehicleForm = new VehicleFormModel { Name = "vehicle" };

            var handler = new CreateVehicle(_vehicleRepo.Object, _photoRepo.Object);
            handler.Execute(UserId, vehicleForm, null);

            _vehicleRepo
                .Verify(r => r.Create(UserId, It.IsAny<Vehicle>()), Times.Once());
        }

        [Fact]
        public void AndVehicleRepositoryThrows_ThenWrapsException()
        {
            _vehicleRepo
                .Setup(v => v.Create(It.IsAny<int>(), It.IsAny<Vehicle>()))
                .Throws<InvalidOperationException>();

            var vehicleForm = new VehicleFormModel { Name = "vehicle" };

            var handler = new CreateVehicle(_vehicleRepo.Object, _photoRepo.Object);

            var ex = Assert.Throws<BusinessServicesException>(() => handler.Execute(UserId, vehicleForm, null));
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

        [Fact]
        public void WithAPhoto_ThenInvokesVehicleRepositoryToUpdatePhotoInfo()
        {
            var vehicleForm = new VehicleFormModel { Name = "vehicle" };
            var photoStream = Mock.MockPhotoStream();

            var handler = new CreateVehicle(_vehicleRepo.Object, _photoRepo.Object);
            handler.Execute(UserId, vehicleForm, photoStream.Object);

            _vehicleRepo
                .Verify(r => r.Create(UserId, It.IsAny<Vehicle>()), Times.Once());

            _vehicleRepo
                .Verify(r => r.Update(It.IsAny<Vehicle>()), Times.Once());
        }
    }
}