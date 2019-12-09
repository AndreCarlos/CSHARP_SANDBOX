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
using MileageStats.Domain.Handlers;
using MileageStats.Model;
using Moq;
using Xunit;

namespace Mileagestats.Services.Tests
{
    public class WhenGetFleetSummaryStatistics
    {
        private const int DefaultUserId = 99;

        [Fact]
        public void WhenGetFleetSummaryStatistics_CalculatesAcrossVehicles()
        {
            var fillups1 = new[]
                            {
                                new FillupEntry {Date = DateTime.UtcNow.AddDays(-1), PricePerUnit = 1.0, TotalUnits = 10},
                                new FillupEntry {Date = DateTime.UtcNow, PricePerUnit = 1.0, TotalUnits = 10}
                            };

            var fillups2 = new[]
                            {
                                new FillupEntry {Date = DateTime.UtcNow.AddDays(-1), PricePerUnit = 1.0, TotalUnits = 10},
                                new FillupEntry {Date = DateTime.UtcNow, PricePerUnit = 1.0, TotalUnits = 10},
                                new FillupEntry
                                    {
                                        Date = DateTime.UtcNow,
                                        PricePerUnit = 1.0,
                                        TotalUnits = 10,
                                        TransactionFee = 5.0
                                    }
                            };

            var vehicle1 = new Vehicle { VehicleId = 1, Fillups = fillups1 };
            var vehicle2 = new Vehicle { VehicleId = 2, Fillups = fillups2 };

            var mockVehicleRepository = new Mock<IVehicleRepository>();

            mockVehicleRepository
                .Setup(x => x.GetVehicles(DefaultUserId))
                .Returns(new[] { vehicle1, vehicle2 });

            var handler = new GetFleetSummaryStatistics(mockVehicleRepository.Object);

            var actual = handler.Execute(DefaultUserId);

            Assert.NotNull(actual);
            Assert.Equal(30, actual.TotalFuelCost);
            Assert.Equal(35, actual.TotalCost);
        }
    }
}