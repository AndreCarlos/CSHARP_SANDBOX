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
using MileageStats.Data.SqlCe.Repositories;
using Xunit;

namespace MileageStats.Data.SqlCe.Tests.Repositories
{
    public class VehiclePhotoRepositoryFixture
    {
        [Fact]
        public void WhenConstuctedWithUnitOfWork_ThenSuccessful()
        {
            VehiclePhotoRepository actual = new VehiclePhotoRepository(new MileageStatsDbContext());

            Assert.NotNull(actual);
        }

        [Fact]
        public void WhenConstructedWithNullUnitOfWork_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => { var repository = new VehiclePhotoRepository(null); });
        }


        [Fact]
        public void WhenCreateCalled_ThenPhotoPersists()
        {
            DatabaseTestUtility.DropCreateMileageStatsDatabase();
            var repository = new VehiclePhotoRepository(new MileageStatsDbContext());
            var photo = new Model.VehiclePhoto()
                            {
                                ImageMimeType = "image/jpeg",
                                Image = new byte[1]
                            };
            repository.Create(1, photo);

            var repository2 = new VehiclePhotoRepository(new MileageStatsDbContext());
            Assert.NotNull(repository2.Get(1));
        }

        [Fact]
        public void WhenDeleteCalled_ThenPhotoNuked()
        {
            DatabaseTestUtility.DropCreateMileageStatsDatabase();
            var repository = new VehiclePhotoRepository(new MileageStatsDbContext());
            var photo = new Model.VehiclePhoto()
                            {
                                ImageMimeType = "image/jpeg",
                                Image = new byte[1]
                            };
            repository.Create(1, photo);

            var photoToEdit = repository.Get(1);
            repository.Delete(photoToEdit.VehiclePhotoId);

            var repository2 = new VehiclePhotoRepository(new MileageStatsDbContext());
            Assert.Throws<InvalidOperationException>(() => repository2.Get(1));
        }

        [Fact]
        public void WhenDeleteCalledForNonexistentPhoto_ThenThrows()
        {
            DatabaseTestUtility.DropCreateMileageStatsDatabase();
            var repository = new VehiclePhotoRepository(new MileageStatsDbContext());

            Assert.Throws<InvalidOperationException>(() => repository.Delete(12345));
        }

        [Fact]
        public void WhenGetCalled_ThenReturnsPhoto()
        {
            DatabaseTestUtility.DropCreateMileageStatsDatabase();

            using (var dbContext = new MileageStatsDbContext())
            {
                var photo = new Model.VehiclePhoto()
                                {
                                    ImageMimeType = "image/jpeg",
                                    Image = new byte[1]
                                };

                dbContext.VehiclePhotos.Add(photo);
                dbContext.SaveChanges();
            }

            VehiclePhotoRepository target = new VehiclePhotoRepository(new MileageStatsDbContext());

            var actual = target.Get(1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void WhenGetCalledForNonExistantPhoto_ThenThrowsInvalidOperationException()
        {
            DatabaseTestUtility.DropCreateMileageStatsDatabase();

            VehiclePhotoRepository target = new VehiclePhotoRepository(new MileageStatsDbContext());

            Assert.Throws<InvalidOperationException>(() => target.Get(1200));
        }
    }
}