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
using System.Collections.ObjectModel;
using System.Linq;
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Properties;

namespace MileageStats.Domain
{
    public class CountryServices : ICountryServices
    {
        private readonly ICountryRepository countryRepository;

        public CountryServices(ICountryRepository countryRepository)
        {
            if (countryRepository == null) throw new ArgumentNullException("countryRepository");
            this.countryRepository = countryRepository;
        }

        public ReadOnlyCollection<string> GetCountriesAndRegionsList()
        {
            try
            {
                var countriesList = this.countryRepository.GetAll().Select(c => c.Name).ToList();
                return new ReadOnlyCollection<string>(countriesList);
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.UnableToGetCountryListExceptionMessage, ex);
            }
        }
    }
}