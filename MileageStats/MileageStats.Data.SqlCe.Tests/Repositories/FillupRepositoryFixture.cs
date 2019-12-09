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
using System.Linq;
using MileageStats.Data.SqlCe.Repositories;
using MileageStats.Model;
using Xunit;

namespace MileageStats.Data.SqlCe.Tests.Repositories
{
    public class FillupRepositoryFixture
    {
        private User defaultTestUser;
        private Vehicle defaultVehicle;

        public FillupRepositoryFixture()
        {
            this.InitializeFixture();
        }

        private void InitializeFixture()
        {
            DatabaseTestUtility.DropCreateMileageStatsDatabase();
            this.defaultTestUser = new User()
                                       {
                                           AuthorizationId = "TestAuthorizationId",
                                           DisplayName = "DefaultTestUser"
                                       };

            var userRepository = new UserRepository(new MileageStatsDbContext());
            userRepository.Create(this.defaultTestUser);

            int userId = this.defaultTestUser.UserId;

            var vehicleRepository = new VehicleRepository(new MileageStatsDbContext());
            this.defaultVehicle = new Vehicle()
                                      {
                                          Name = "Test Vehicle"
                                      };
            vehicleRepository.Create(this.defaultTestUser.UserId, this.defaultVehicle);
        }

        [Fact]
        public void WhenConstructedWithNullUnitOfWork_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new FillupRepository(null));
        }

        [Fact]
        public void WhenAddingMinimalFillupEntry_ThenPersistsToTheDatabase()
        {
            var repository = new FillupRepository(this.GetUnitOfWork());

            var fillupEntry = new FillupEntry()
                                  {
                                      Date = DateTime.Now,
                                      Odometer = 3000,
                                      PricePerUnit = 3.24d,
                                      TotalUnits = 13.01d,
                                  };

            repository.Create(this.defaultTestUser.UserId, this.defaultVehicle.VehicleId, fillupEntry);

            // Verification
            var repositoryForVerification = new FillupRepository(this.GetUnitOfWork());
            var retrievedFillup = repositoryForVerification.GetFillups(this.defaultVehicle.VehicleId).First();

            Assert.NotNull(retrievedFillup);
            Assert.Equal(fillupEntry.Date.ToShortDateString(), retrievedFillup.Date.ToShortDateString());
            // We only care that the dates map.
            AssertExtensions.PropertiesAreEqual(fillupEntry, retrievedFillup, "Odometer", "PricePerUnit", "TotalUnits");
        }


        [Fact]
        public void WhenAddingFillupEntry_ThenFillupAssignedNewId()
        {
            var repository = new FillupRepository(this.GetUnitOfWork());

            var fillupEntry = new FillupEntry()
                                  {
                                      Date = DateTime.Now,
                                      Odometer = 3000,
                                      PricePerUnit = 3.24d,
                                      TotalUnits = 13.01d,
                                  };

            repository.Create(this.defaultTestUser.UserId, this.defaultVehicle.VehicleId, fillupEntry);

            Assert.NotEqual(0, fillupEntry.FillupEntryId);
        }

        [Fact]
        public void WhenGettingAllFillupsForNewVehicle_ThenReturnsEmptyCollection()
        {
            var repository = new FillupRepository(this.GetUnitOfWork());

            var fillups = repository.GetFillups(this.defaultVehicle.VehicleId);

            Assert.NotNull(fillups);
            Assert.Equal(0, fillups.Count());
        }

        [Fact]
        public void WhenGettingAllFillups_ThenReturnsAllFillups()
        {
            var repository = new FillupRepository(this.GetUnitOfWork());

            var fillupEntry1 = new FillupEntry()
                                   {
                                       Date = DateTime.Now,
                                       Odometer = 3000,
                                       PricePerUnit = 3.24d,
                                       TotalUnits = 13.01d,
                                   };
            repository.Create(this.defaultTestUser.UserId, this.defaultVehicle.VehicleId, fillupEntry1);

            var fillupEntry2 = new FillupEntry()
                                   {
                                       Date = DateTime.Now,
                                       Odometer = 3001,
                                       PricePerUnit = 3.24d,
                                       TotalUnits = 13.01d,
                                   };
            repository.Create(this.defaultTestUser.UserId, this.defaultVehicle.VehicleId, fillupEntry2);


            var fillups = repository.GetFillups(this.defaultVehicle.VehicleId);

            Assert.NotNull(fillups);
            Assert.Equal(2, fillups.Count());
        }

        [Fact]
        public void WhenAddingFillupEntryWithAllData_ThenPersistsToTheDatabase()
        {
            var repository = new FillupRepository(this.GetUnitOfWork());

            var fillupEntry = new FillupEntry()
                                  {
                                      Date = DateTime.Now,
                                      Odometer = 3000,
                                      PricePerUnit = 3.24d,
                                      TotalUnits = 13.01d,
                                      Remarks = "Remarkable",
                                      TransactionFee = 1.25d,
                                      Vendor = "Ideal Vendor"
                                  };

            repository.Create(this.defaultTestUser.UserId, this.defaultVehicle.VehicleId, fillupEntry);

            // Verification
            var repositoryForVerification = new FillupRepository(this.GetUnitOfWork());
            var enteredFillup = repositoryForVerification.GetFillups(this.defaultVehicle.VehicleId).First();
            Assert.NotNull(enteredFillup);
            Assert.Equal(fillupEntry.Date.ToShortDateString(), enteredFillup.Date.ToShortDateString());
            // We only care that the dates map.
            AssertExtensions.PropertiesAreEqual(fillupEntry, enteredFillup, "Odometer", "PricePerUnit", "TotalUnits",
                                                "Remarks", "TransactionFee", "Vendor");
        }


        private IUnitOfWork GetUnitOfWork()
        {
            return new MileageStatsDbContext();
        }
    }
}