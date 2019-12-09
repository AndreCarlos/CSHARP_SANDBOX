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
using System.Diagnostics;
using System.Linq;
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Handlers;
using MileageStats.Domain.Models;
using MileageStats.Model;

namespace MileageStats.Domain
{
    public class ChartDataService : IChartDataService
    {
        private readonly IVehicleRepository _vehicleRepository;

        #region IChartDataService Members

        public StatisticSeries CalculateSeriesForUser(int userId, DateTime? startDate, DateTime? endDate)
        {
            var vehicles = _vehicleRepository.GetVehicles(userId);

            var series = new StatisticSeries();

            foreach (var vehicle in vehicles)
            {
                CalculateSeriesForVehicle(vehicle, series, startDate, endDate);
            }

            return series;
        }

        public StatisticSeries CalculateSeriesForVehicle(int userId, int vehicleId, DateTime? startDate,
                                                         DateTime? endDate)
        {
            var series = new StatisticSeries();
            CalculateSeriesForVehicle(userId, vehicleId, series, startDate, endDate);
            return series;
        }

        #endregion

        public ChartDataService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        private void CalculateSeriesForVehicle(int userId, int vehicleId, StatisticSeries series, DateTime? startDate, DateTime? endDate)
        {
            var vehicle = _vehicleRepository.GetVehicle(userId, vehicleId);
            CalculateSeriesForVehicle(vehicle, series, startDate, endDate);
        }

        private static void CalculateSeriesForVehicle(Vehicle vehicle, StatisticSeries series, DateTime? startDate, DateTime? endDate)
        {
            Debug.Assert(series != null);

            DateTime startFilterDate = startDate ?? DateTime.MinValue;
            DateTime endFilterDate = endDate ?? DateTime.UtcNow;

            var fillUps = vehicle.Fillups;

            var fillupGroups = from fillUp in fillUps
                               where ((fillUp.Date >= startFilterDate) && (fillUp.Date <= endFilterDate))
                               group fillUp by new { Year = fillUp.Date.Year, Month = fillUp.Date.Month }
                                   into g
                                   orderby g.Key.Year, g.Key.Month
                                   select g;

            var firstFillUp = fillUps.OrderBy(x => x.Date).FirstOrDefault();

            VehicleStatisticsModel statistics;
            foreach (var fillupGroup in fillupGroups)
            {
                var includeFirstFillup = (fillupGroup.Key.Year != firstFillUp.Date.Year) ||
                                                    (fillupGroup.Key.Month != firstFillUp.Date.Month);

                statistics = CalculateStatistics.Calculate(fillupGroup, includeFirstFillup);

                Debug.Assert(firstFillUp != null);
                

                var seriesEntry = new StatisticSeriesEntry
                {
                    Id = vehicle.VehicleId,
                    Name = vehicle.Name,
                    Year = fillupGroup.Key.Year,
                    Month = fillupGroup.Key.Month,
                    AverageFuelEfficiency = Math.Round(statistics.AverageFuelEfficiency, 2),
                    TotalCost = Math.Round(statistics.TotalCost, 2),
                    TotalDistance = statistics.TotalDistance,
                };
                series.Entries.Add(seriesEntry);
            }            
        }
    }
}