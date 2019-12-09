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
using Xunit;

namespace MileageStats.Data.SqlCe.Tests.Repositories
{
    public class CountryRepositoryFixture
    {
        public CountryRepositoryFixture()
        {
            DatabaseTestUtility.DropCreateAndSeedMileageStatsDatabase();
        }

        [Fact]
        public void WhenConstructingRepositoryWithNullContext_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => { var repository = new CountryRepository(null); });
        }

        [Fact]
        public void WhenGettingAllRecords_ThenAllRecordsAreReturned()
        {
            var repository = new CountryRepository(new MileageStatsDbContext());

            var countries = repository.GetAll();

            Assert.NotNull(countries);
            Assert.Equal(238, countries.Count());
        }

        [Fact]
        public void WhenGettingAllRecords_ThenRecordsIncludeName()
        {
            var repository = new CountryRepository(new MileageStatsDbContext());

            var countries = repository.GetAll().ToList();

            Assert.NotNull(countries);
            Assert.Equal("Afghanistan", countries[0].Name);
        }
    }
}