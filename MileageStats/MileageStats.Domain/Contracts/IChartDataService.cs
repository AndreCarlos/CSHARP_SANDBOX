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
using MileageStats.Domain.Models;
using System;

namespace MileageStats.Domain.Contracts
{
    public interface IChartDataService
    {
        /// <summary>
        /// Calculates the series of statistics of all vehicles for the specified user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="startDate">The start date for filtering.</param>
        /// <param name="endDate">The end date for filtering.</param>
        /// <returns>A statistical series.</returns>
        StatisticSeries CalculateSeriesForUser(int userId, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Calculates the series of statistics for the specified vehicle.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="startDate">The start date for filtering.</param>
        /// <param name="endDate">The end date for filtering.</param>
        /// <returns>A statistical series.</returns>
        StatisticSeries CalculateSeriesForVehicle(int userId, int vehicleId, DateTime? startDate, DateTime? endDate);
    }
}