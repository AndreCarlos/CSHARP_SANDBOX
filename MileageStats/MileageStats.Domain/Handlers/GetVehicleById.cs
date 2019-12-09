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
using MileageStats.Data;
using MileageStats.Domain.Models;
using MileageStats.Model;

namespace MileageStats.Domain.Handlers
{
    public class GetVehicleById
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehicleById(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public virtual VehicleModel Execute(int userId, int vehicleId)
        {
            Vehicle vehicle;

            try
            {
                vehicle = _vehicleRepository.GetVehicle(userId, vehicleId);
            }
            catch(InvalidOperationException)
            {
                return null;
            }

            var fillups = vehicle.Fillups.OrderBy(f => f.Odometer).Skip(1);
            var statistics = CalculateStatistics.Calculate(fillups);

            return new VehicleModel(vehicle, statistics);
        }
    }
}