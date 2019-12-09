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

namespace MileageStats.Domain.Handlers
{
    public class GetYearsMakesAndModels
    {
        private readonly IVehicleManufacturerRepository _manufacturerRepository;

        public GetYearsMakesAndModels(IVehicleManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public virtual Tuple<int[], string[], string[]> Execute(int? filteredToYear = null, string filteredByMake = null)
        {
            int[] years = _manufacturerRepository.GetYears();
            string[] makes = null;
            string[] models = null;

            if ((years != null) && (years.Length > 0))
            {
                // If the user specified a year, then look up the makes.
                // Otherwise, use the makes from the first year in the list.
                int selectedYear = -1;

                if ((filteredToYear != null) &&
                    (_manufacturerRepository.IsValidYear(filteredToYear.Value)))
                {
                    selectedYear = filteredToYear.Value;
                }

                makes = _manufacturerRepository.GetMakes(selectedYear);

                // if the user specified a year and a make, then look up the models.
                if ((makes != null) && (makes.Length > 0))
                {
                    string selectedMake = string.Empty;

                    if ((!string.IsNullOrEmpty(filteredByMake)) &&
                        (_manufacturerRepository.IsValidMake(selectedYear, filteredByMake)))
                    {
                        selectedMake = filteredByMake;
                    }

                    models = _manufacturerRepository.GetModels(selectedYear, selectedMake);
                }
            }

            if (years == null)
            {
                years = new int[] {};
            }

            if (makes == null)
            {
                makes = new string[] {};
            }

            if (models == null)
            {
                models = new string[] {};
            }

            return new Tuple<int[], string[], string[]>(years, makes, models);
        }
    }
}