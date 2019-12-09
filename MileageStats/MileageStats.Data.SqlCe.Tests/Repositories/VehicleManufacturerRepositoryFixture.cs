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
    public class VehicleManufacturerRepositoryFixture
    {
        private MileageStatsDbContext dbContext;

        private void SetupResolverAndDatabase()
        {
            DatabaseTestUtility.DropCreateMileageStatsDatabase();
            this.dbContext = new MileageStatsDbContext();
            this.dbContext.Seed();
        }

        [Fact]
        public void WhenConstructingWithResolver_ThenSuccessful()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository actual = new VehicleManufacturerRepository(this.dbContext);

            Assert.NotNull(actual);
        }

        [Fact]
        public void WhenConstructedWithNullResolver_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => { var repository = new VehicleManufacturerRepository(null); });
        }

        [Fact]
        public void WhenGetYears_ReturnsYears()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            int[] actual = target.GetYears();

            Assert.NotNull(actual);
            Assert.NotEqual(0, actual.Length);
        }

        [Fact]
        public void WhenGetMakesWithValidYear_ReturnsMakes()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            string[] actual = target.GetMakes(2011);

            Assert.NotNull(actual);
            Assert.NotEqual(0, actual.Length);
        }

        [Fact]
        public void WhenGetMakesWithInvalidYear_ReturnsNull()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            string[] actual = target.GetMakes(3211);

            Assert.Null(actual);
        }

        [Fact]
        public void WhenGetModelsWithValidYearAndMake_ReturnsModels()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            string[] actual = target.GetModels(2010, "BMW");

            Assert.NotNull(actual);
            Assert.NotEqual(0, actual.Length);
        }

        [Fact]
        public void WhenGetModelsWithInvalidYear_ReturnsNull()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            string[] actual = target.GetModels(3211, "BMW");

            Assert.Null(actual);
        }

        [Fact]
        public void WhenGetModelsWithInvalidMake_ReturnsNull()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            string[] actual = target.GetModels(2011, "HotWheels");

            Assert.Null(actual);
        }

        [Fact]
        public void WhenIsValidYearWithValidYear_ReturnsTrue()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            bool actual = target.IsValidYear(2010);

            Assert.True(actual);
        }

        [Fact]
        public void WhenIsValidYearWithInvalidYear_ReturnsFalse()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            bool actual = target.IsValidYear(3015);

            Assert.False(actual);
        }

        [Fact]
        public void WhenIsValidMakeWithValidYearAndMake_ReturnsTrue()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            bool actual = target.IsValidMake(2010, "Audi");

            Assert.True(actual);
        }

        [Fact]
        public void WhenIsValidMakeWithInvalidYear_ReturnsFalse()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            bool actual = target.IsValidMake(3022, "Audi");

            Assert.False(actual);
        }

        [Fact]
        public void WhenIsValidMakeWithInvalidMake_ReturnsFalse()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            bool actual = target.IsValidMake(2010, "Innee");

            Assert.False(actual);
        }

        [Fact]
        public void WhenIsValidModelWithValidYearAndMake_ReturnsTrue()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            bool actual = target.IsValidModel(2010, "Audi", "A4");

            Assert.True(actual);
        }

        [Fact]
        public void WhenIsValidModelWithInvalidYear_ReturnsFalse()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            bool actual = target.IsValidModel(3022, "Audi", "A4");

            Assert.False(actual);
        }

        [Fact]
        public void WhenIsValidModelWithInvalidMake_ReturnsFalse()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            bool actual = target.IsValidModel(2010, "Innee", "A4");

            Assert.False(actual);
        }

        [Fact]
        public void WhenIsValidModelWithInvalidModel_ReturnsFalse()
        {
            this.SetupResolverAndDatabase();
            VehicleManufacturerRepository target = new VehicleManufacturerRepository(this.dbContext);

            bool actual = target.IsValidModel(2010, "Audi", "A422L16");

            Assert.False(actual);
        }
    }
}