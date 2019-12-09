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
using MileageStats.Data.SqlCe.Repositories;
using MileageStats.Model;
using Xunit;

namespace MileageStats.Data.SqlCe.Tests.Repositories
{
    // This test fixture was intentionally written with an explicit dependency on 
    // the MileageStatsDbContext. 
    public class VehicleRepositoryFixture
    {
        private User defaultTestUser;

        public VehicleRepositoryFixture()
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

            var repository = new UserRepository(new MileageStatsDbContext());
            repository.Create(this.defaultTestUser);
        }

        [Fact]
        public void WhenConstructedWithNullUnitOfWork_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new VehicleRepository(null));
        }

        [Fact]
        public void WhenGetAllFromEmptyDatabase_ThenReturnsEmptyCollection()
        {
            var repository = new VehicleRepository(new MileageStatsDbContext());
            IEnumerable<Vehicle> actual = repository.GetVehicles(this.defaultTestUser.UserId);

            Assert.NotNull(actual);
            List<Vehicle> actualList = new List<Vehicle>(actual);
            Assert.Equal(0, actualList.Count);
        }


        [Fact]
        public void WhenVehicleAdded_ThenUpdatesRepository()
        {
            var repository = new VehicleRepository(new MileageStatsDbContext());

            var vehicle = new Vehicle {Name = "Vehicle"};


            repository.Create(this.defaultTestUser.UserId, vehicle);

            List<Vehicle> actualList = new MileageStatsDbContext().Vehicles.ToList();

            Assert.Equal(1, actualList.Count);
            Assert.Equal(vehicle.Name, actualList[0].Name);
        }

        [Fact]
        public void WhenVehicleAdded_ThenPersists()
        {
            int userId = this.defaultTestUser.UserId;
            var repository = new VehicleRepository(new MileageStatsDbContext());

            Vehicle vehicle1 = new Vehicle {Name = "Vehicle1"};
            Vehicle vehicle2 = new Vehicle {Name = "Vehicle2"};

            repository.Create(userId, vehicle1);
            repository.Create(userId, vehicle2);

            // I use a new context and repository to verify the data was stored
            var repository2 = new VehicleRepository(new MileageStatsDbContext());

            var retrievedVehicles = repository2.GetVehicles(userId);

            Assert.NotNull(retrievedVehicles);
            List<Vehicle> actualList = new List<Vehicle>(retrievedVehicles);

            Assert.Equal(2, actualList.Count);
            Assert.Equal(vehicle1.Name, actualList[0].Name);
            Assert.Equal(vehicle2.Name, actualList[1].Name);
        }

        [Fact]
        public void WhenVehicleAdded_ThenUpdatesVehicleId()
        {
            int userId = this.defaultTestUser.UserId;
            var repository = new VehicleRepository(new MileageStatsDbContext());

            Vehicle vehicle = new Vehicle {Name = "Vehicle"};

            repository.Create(userId, vehicle);

            IEnumerable<Vehicle> retrievedVehicles = repository.GetVehicles(userId);

            Assert.NotNull(retrievedVehicles);
            List<Vehicle> actualList = new List<Vehicle>(retrievedVehicles);

            Assert.Equal(1, actualList.Count);
            Assert.Equal(1, actualList[0].VehicleId);
        }

        [Fact]
        public void WhenVehicleModified_ThenPersistsChange()
        {
            int userId = this.defaultTestUser.UserId;
            var repository = new VehicleRepository(new MileageStatsDbContext());

            Vehicle vehicle = new Vehicle {Name = "Vehicle", UserId = userId};
            repository.Create(userId, vehicle);

            // I use a new context and repository to verify the data was stored
            var repositoryForUpdate = new VehicleRepository(new MileageStatsDbContext());

            var retrievedVehicle = repositoryForUpdate.GetVehicles(userId).First();

            retrievedVehicle.Name = "Updated Vehicle Name";
            repositoryForUpdate.Update(retrievedVehicle);
            int updatedVehicleId = retrievedVehicle.VehicleId;

            var repositoryForVerify = new VehicleRepository(new MileageStatsDbContext());
            var updatedVehicle = repositoryForVerify.GetVehicle(userId, updatedVehicleId);

            Assert.Equal("Updated Vehicle Name", updatedVehicle.Name);
        }

        [Fact]
        public void WhenVehicleModifiedInSameContext_ThenPersistsChange()
        {
            IUnitOfWork uow = new MileageStatsDbContext();
            int userId = this.defaultTestUser.UserId;
            var repository = new VehicleRepository(uow);

            Vehicle vehicle = new Vehicle {Name = "Vehicle", UserId = userId};
            repository.Create(userId, vehicle);

            // I use a new context and repository to verify the data was stored
            var repositoryForUpdate = new VehicleRepository(uow);

            var retrievedVehicle = repositoryForUpdate.GetVehicles(userId).First();

            retrievedVehicle.Name = "Updated Vehicle Name";
            repositoryForUpdate.Update(retrievedVehicle);
            int updatedVehicleId = retrievedVehicle.VehicleId;

            var repositoryForVerify = new VehicleRepository(uow);
            var updatedVehicle = repositoryForVerify.GetVehicle(userId, updatedVehicleId);

            Assert.Equal("Updated Vehicle Name", updatedVehicle.Name);
        }

        [Fact]
        public void WhenGettingOtherUsersVehicle_ThenThrowsInvalidOperationException()
        {
            int userId = this.defaultTestUser.UserId;
            var repository = new VehicleRepository(new MileageStatsDbContext());

            var vehicle = new Vehicle {Name = "Vehicle"};

            repository.Create(userId, vehicle);

            var repositoryForVerify = new VehicleRepository(new MileageStatsDbContext());
            Assert.Throws<InvalidOperationException>(() => repositoryForVerify.GetVehicle(42, vehicle.VehicleId));
        }

        [Fact]
        public void WhenVehicleDeleted_ThenPersists()
        {
            int userId = this.defaultTestUser.UserId;
            var repository = new VehicleRepository(new MileageStatsDbContext());

            Vehicle vehicle1 = new Vehicle {Name = "Vehicle1"};
            repository.Create(userId, vehicle1);

            var retrievedVehicles = repository.GetVehicles(userId);
            Assert.Equal(1, retrievedVehicles.Count());
            repository.Delete(retrievedVehicles.First().VehicleId);

            // I use a new context and repository to verify the vehicle was deleted
            var repository2 = new VehicleRepository(new MileageStatsDbContext());

            var verifyVehicles = repository2.GetVehicles(userId);

            Assert.NotNull(retrievedVehicles);
            Assert.Equal(0, verifyVehicles.Count());
        }
    }
}