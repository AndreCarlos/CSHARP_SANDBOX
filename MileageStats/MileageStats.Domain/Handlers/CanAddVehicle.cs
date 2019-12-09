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
using System.Linq;
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Models;
using MileageStats.Domain.Properties;

namespace MileageStats.Domain.Handlers
{
    public class CanAddVehicle
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly CanValidateVehicleYearMakeAndModel _validateVehicleYearMakeAndModel;

        public const int MaxNumberOfVehiclesPerUser = 10;

        public CanAddVehicle(IVehicleRepository vehicleRepository, CanValidateVehicleYearMakeAndModel validateVehicleYearMakeAndModel)
        {
            _vehicleRepository = vehicleRepository;
            _validateVehicleYearMakeAndModel = validateVehicleYearMakeAndModel;
        }

        public virtual IEnumerable<ValidationResult> Execute(int userId, ICreateVehicleCommand vehicle)
        {
            var vehicles = _vehicleRepository.GetVehicles(userId);

            if (vehicles.Count() >= MaxNumberOfVehiclesPerUser)
            {
                yield return new ValidationResult(Resources.TooManyVehicles);
            }

            foreach (var result in _validateVehicleYearMakeAndModel.Execute(vehicle))
            {
                yield return result;
            }
        }
    }
}