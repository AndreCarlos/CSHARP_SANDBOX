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
using MileageStats.Domain;
using MileageStats.Domain.Contracts;
using MileageStats.Model;
using Moq;
using Xunit;

namespace MileageStats.Services.Tests
{
    public class WhenGettingCountryList
    {
        [Fact]
        public void WhenCreatedWithNullRepository_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new CountryServices(null));
        }

        [Fact]
        public void WhenGettingCountryList_ThenReturnsCountryNames()
        {
            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(c => c.GetAll()).Returns(new List<Country>() {new Country()});
            var services = new CountryServices(countryRepositoryMock.Object);

            var countries = services.GetCountriesAndRegionsList();

            Assert.NotNull(countries);
            Assert.Equal(1, countries.Count);
        }

        [Fact]
        public void WhenGettingCountryListAndRepositoryThrows_ThenWrapsException()
        {
            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(c => c.GetAll()).Throws<InvalidOperationException>();
            var services = new CountryServices(countryRepositoryMock.Object);

            var ex = Assert.Throws<BusinessServicesException>(() => services.GetCountriesAndRegionsList());
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

        [Fact]
        public void WhenGettingCountryListAndRepositoryReturnsNoRecords_ThenReturnsEmptyCollection()
        {
            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(c => c.GetAll()).Returns(new List<Country>());
            var services = new CountryServices(countryRepositoryMock.Object);

            var countries = services.GetCountriesAndRegionsList();

            Assert.Empty(countries);
        }
    }
}