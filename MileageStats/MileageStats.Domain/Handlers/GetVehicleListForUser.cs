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
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Models;
using MileageStats.Model;

namespace MileageStats.Domain.Handlers
{
    public class GetVehicleListForUser
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehicleListForUser(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public virtual IEnumerable<VehicleModel> Execute(int userId)
        {
            IEnumerable<Vehicle> vehicleData = null;

            try
            {
                vehicleData = _vehicleRepository.GetVehicles(userId);
            }
            catch (InvalidOperationException e)
            {
                throw new BusinessServicesException("Unable to retrieve vehicle from database.", e);
            }

            var vehicles = from vehicle in vehicleData
                           let fillups = vehicle.Fillups.OrderBy(f => f.Odometer)
                           let statistics = CalculateStatistics.Calculate(fillups, includeFirst: false)
                           orderby vehicle.SortOrder
                           select new VehicleModel(vehicle, statistics);

            return vehicles;
        }
    }
}