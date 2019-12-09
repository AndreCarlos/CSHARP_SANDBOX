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
using MileageStats.Web.Models;
using Moq;
using Xunit;

namespace MileageStats.Services.Tests
{
    public class WhenUpdatingVehicle
    {
        private const int UserId = 99;
        private const int DefaultVehicleId = 1;
        private readonly Mock<IVehicleRepository> _vehicleRepo;
        private readonly Mock<IVehiclePhotoRepository> _photoRepo;

        public WhenUpdatingVehicle()
        {
            _vehicleRepo = new Mock<IVehicleRepository>();
            _photoRepo = new Mock<IVehiclePhotoRepository>();
        }

        [Fact]
        public void ThenDelegatesToVehicleRepository()
        {
            var vehicleForm = new VehicleFormModel { VehicleId = DefaultVehicleId };

            _vehicleRepo
                .Setup(vr => vr.GetVehicle(UserId, DefaultVehicleId))
                .Returns(new Model.Vehicle());

            var handler = new UpdateVehicle(_vehicleRepo.Object, _photoRepo.Object);
            handler.Execute(UserId, vehicleForm, null);

            _vehicleRepo.Verify(r => r.Update(It.IsAny<Model.Vehicle>()), Times.Once());
        }

        [Fact]
        public void WihtNewPhoto_ThenDelegatesToPhotoRepositoryAddNewPhoto()
        {
            var vehicleForm = new VehicleFormModel { VehicleId = DefaultVehicleId };

            _vehicleRepo
                .Setup(r => r.GetVehicle(UserId, DefaultVehicleId))
                .Returns(new Model.Vehicle { VehicleId = DefaultVehicleId });
            
            var newPhotoFile = Mock.MockPhotoStream().Object;

            var handler = new UpdateVehicle(_vehicleRepo.Object, _photoRepo.Object);
            handler.Execute(UserId, vehicleForm, newPhotoFile);

            _photoRepo.Verify(r => r.Create(DefaultVehicleId, It.IsAny<Model.VehiclePhoto>()), Times.Once());
        }

        [Fact]
        public void WithExistingPhoto_ThenDelegatesToPhotoRepositoryToDeleteOldPhoto()
        {
            const int vehiclePhotoId = 300;
            var vehicleForm = new VehicleFormModel { VehicleId = DefaultVehicleId };

            _vehicleRepo
                .Setup(vr => vr.GetVehicle(UserId, DefaultVehicleId))
                .Returns(new Model.Vehicle { VehicleId = DefaultVehicleId, PhotoId = vehiclePhotoId });

            var newPhotoFile = Mock.MockPhotoStream().Object;

            var handler = new UpdateVehicle(_vehicleRepo.Object, _photoRepo.Object);
            handler.Execute(UserId, vehicleForm, newPhotoFile);

            _photoRepo.Verify(r => r.Delete(vehiclePhotoId), Times.Once());
        }

        [Fact]
        public void ForOtherUser_ThenThrows()
        {
            const int anotherUserId = 87;

            var vehicleForm = new VehicleFormModel { Name = "vehicle", VehicleId = DefaultVehicleId };

            _vehicleRepo
                .Setup(vr => vr.GetVehicle(anotherUserId, DefaultVehicleId))
                .Throws(new InvalidOperationException());

            var handler = new UpdateVehicle(_vehicleRepo.Object, _photoRepo.Object);

            Assert.Throws<BusinessServicesException>(() => handler.Execute(anotherUserId, vehicleForm, null));
        }

        [Fact]
        public void ThatAndVehicleDoesNotExist_ThenThrowsNonExistent_ThenThrows()
        {
            // this is the same test as FromOtherUsers_ThenThrows
            const int nonExistentVehicleId = 87;
            var vehicleForm = new VehicleFormModel { Name = "vehicle", VehicleId = nonExistentVehicleId };

            // the repo throws an exception when it can't find a match with both the user and the vehicle
            _vehicleRepo
                .Setup(vr => vr.GetVehicle(UserId, It.IsAny<int>()))
                .Throws(new InvalidOperationException());

            var handler = new UpdateVehicle(_vehicleRepo.Object, _photoRepo.Object);

            Assert.Throws<BusinessServicesException>(() => handler.Execute(UserId, vehicleForm, null));
        }

        [Fact]
        public void Throws_ThenWrapsException()
        {
            var vehicleForm = new VehicleFormModel { Name = "vehicle", VehicleId = DefaultVehicleId };

            _vehicleRepo
                .Setup(vr => vr.GetVehicle(UserId, DefaultVehicleId))
                .Throws(new InvalidOperationException());

            var handler = new UpdateVehicle(_vehicleRepo.Object, _photoRepo.Object);

            Exception ex = Assert.Throws<BusinessServicesException>(() => handler.Execute(UserId, vehicleForm, null));

            Assert.NotNull(ex.InnerException);
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

   
    }
}