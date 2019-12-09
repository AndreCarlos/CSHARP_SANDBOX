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
using System.Linq;
using MileageStats.Data;
using MileageStats.Domain.Models;

namespace MileageStats.Domain.Handlers
{
    public class GetFleetSummaryStatistics
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetFleetSummaryStatistics(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public virtual FleetStatistics Execute(int userId)
        {
            var vehicles = _vehicleRepository.GetVehicles(userId);

            var stats = from vehicle in vehicles
                        where vehicle.Fillups.Any()
                        let statistics = CalculateStatistics.Calculate(vehicle.Fillups, includeFirst: false)
                        select statistics;

            return new FleetStatistics(stats);
        }
    }
}