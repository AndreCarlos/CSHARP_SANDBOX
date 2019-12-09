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
using System.Collections.Generic;
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Models;
using MileageStats.Domain.Properties;

namespace MileageStats.Domain.Handlers
{
    public class CanValidateVehicleYearMakeAndModel
    {
        private readonly IVehicleManufacturerRepository _manufacturerRepository;

        public CanValidateVehicleYearMakeAndModel(IVehicleManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public virtual IEnumerable<ValidationResult> Execute(ICreateVehicleCommand vehicle)
        {
            bool isYearSet = vehicle.Year.HasValue;
            bool isMakeSet = !string.IsNullOrEmpty(vehicle.MakeName);
            bool isModelSet = !string.IsNullOrEmpty(vehicle.ModelName);

            bool isYearValid = true;
            bool isMakeValid = true;
            bool isModelValid = true;

            // Make requires a year
            if ((!isYearSet) && isMakeSet)
            {
                yield return new ValidationResult("MakeName", Resources.VehicleMissingYearForMake);
                isMakeValid = false;
            }

            // Model requires a year and make
            if (isModelSet)
            {
                if (!isYearSet)
                {
                    yield return new ValidationResult("ModelName", Resources.VehicleMissingYearForModel);
                    isModelValid = false;
                }
                else if (!isMakeSet)
                {
                    yield return new ValidationResult("ModelName", Resources.VehicleMissingMakeForModel);
                    isModelValid = false;
                }
            }

            // Validate Year value (if not already invalid)
            if (isYearValid)
            {
                isYearValid = ((!isYearSet) || _manufacturerRepository.IsValidYear(vehicle.Year.Value));
                if (!isYearValid)
                {
                    yield return new ValidationResult("Year", Resources.VehicleYearInvalid);
                }
            }

            // Validate Make value (if not already invalid)
            if (isMakeValid)
            {
                isMakeValid = ((!isMakeSet) ||
                               (isYearSet &&
                                _manufacturerRepository.IsValidMake(vehicle.Year.Value, vehicle.MakeName)));
                if (!isMakeValid)
                {
                    yield return new ValidationResult("MakeName", Resources.VehicleMakeInvalid);
                }
            }

            // Validate Model value (if not already invalid)
            if (isModelValid)
            {
                isModelValid = ((!isModelSet) ||
                                (isYearSet && isMakeSet &&
                                 _manufacturerRepository.IsValidModel(vehicle.Year.Value, vehicle.MakeName,
                                                                      vehicle.ModelName)));
                if (!isModelValid)
                {
                    yield return new ValidationResult("ModelName", Resources.VehicleModelInvalid);
                }
            }
        }
    }
}