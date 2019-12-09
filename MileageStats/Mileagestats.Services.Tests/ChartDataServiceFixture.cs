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
using System.Collections.ObjectModel;
using System.Linq;
using MileageStats.Data;
using MileageStats.Domain;
using MileageStats.Domain.Models;
using MileageStats.Model;
using Moq;
using Xunit;

namespace Mileagestats.Services.Tests
{
    public class ChartDataServiceFixture
    {
        readonly Mock<IVehicleRepository> _vehicleRepo = new Mock<IVehicleRepository>();

        [Fact]
        public void WhenConstructed_ThenInitializesValues()
        {
            var actual = new ChartDataService(_vehicleRepo.Object);

            Assert.NotNull(actual);
        }

        [Fact]
        public void WhenCalculateSeriesForUser_ThenReturnsSeries()
        {
            DateTime today = DateTime.UtcNow.Date;

            int userId = 1;

            var vehicles = new[]
                               {
                                   new Vehicle {VehicleId = 1, Fillups = CreateFillups(today,1)},
                                   new Vehicle {VehicleId = 2, Fillups = CreateFillups(today,2)},
                                   new Vehicle {VehicleId = 3, Fillups = CreateFillups(today,3)}
                               };

            _vehicleRepo
                .Setup(r => r.GetVehicles(userId))
                .Returns(vehicles);

            var target = new ChartDataService(_vehicleRepo.Object);
            var actual = target.CalculateSeriesForUser(userId, null, null);

            // 3 vehicles * 7 months
            Assert.Equal(3*7, actual.Entries.Count);

            var actualEntries = new List<StatisticSeriesEntry>(actual.Entries);

            var afeValues = new[] {8.75, 6.67, 5.0, 10.0, 20.0, 6.67, 4};
            var tcValues = new double[] {160, 450, 400, 200, 100, 300, 500};
            var tdValues = new double[] {700, 1000, 1000, 1000, 1000, 1000, 1000};

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(1, actualEntries[i].Id);
                Assert.Equal(today.AddMonths(-1*(7 - i)).Year, actualEntries[i].Year);
                Assert.Equal(today.AddMonths(-1*(7 - i)).Month, actualEntries[i].Month);
                Assert.Equal(afeValues[i], actualEntries[i].AverageFuelEfficiency);
                Assert.Equal(tcValues[i], actualEntries[i].TotalCost);
                Assert.Equal(tdValues[i], actualEntries[i].TotalDistance);
            }
        }

        [Fact]
        public void WhenCalculateSeriesWithNoFillups_ThenReturnsEmptySeries()
        {
            var fillUps = new ReadOnlyCollection<FillupEntry>(new List<FillupEntry>());

            int userId = 1;

            var vehicles = new[]
                               {
                                   new Vehicle {VehicleId = 1, Fillups = fillUps},
                                   new Vehicle {VehicleId = 2, Fillups = fillUps},
                                   new Vehicle {VehicleId = 3, Fillups = fillUps}
                               };

            _vehicleRepo
                .Setup(r => r.GetVehicles(userId))
                .Returns(vehicles);

            var target = new ChartDataService(_vehicleRepo.Object);

            StatisticSeries actual = target.CalculateSeriesForUser(userId, null, null);

            Assert.NotNull(actual);

            // 3 vehicles * 0 months
            Assert.Equal(0, actual.Entries.Count);
        }


        private static ICollection<FillupEntry> CreateFillups(DateTime today, int vehicleId)
        {
            today = today.Date;
            DateTime monthsAgo7 = today.AddMonths(-7);
            DateTime monthsAgo6 = today.AddMonths(-6);
            DateTime monthsAgo5 = today.AddMonths(-5);
            DateTime monthsAgo4 = today.AddMonths(-4);
            DateTime monthsAgo3 = today.AddMonths(-3);
            DateTime monthsAgo2 = today.AddMonths(-2);
            DateTime monthsAgo1 = today.AddMonths(-1);

            var fillUps = new[]
                              {
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo7.Year, monthsAgo7.Month, 1),
                                          Odometer = 100,
                                          Distance = null,
                                          PricePerUnit = 2,
                                          TotalUnits = 20
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo7.Year, monthsAgo7.Month, 7),
                                          Odometer = 200,
                                          Distance = 100,
                                          PricePerUnit = 2,
                                          TotalUnits = 20
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo7.Year, monthsAgo7.Month, 14),
                                          Odometer = 400,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 20
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo7.Year, monthsAgo7.Month, 21),
                                          Odometer = 600,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 20
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo7.Year, monthsAgo7.Month, 28),
                                          Odometer = 800,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 20
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo6.Year, monthsAgo6.Month, 1),
                                          Odometer = 1000,
                                          Distance = 200,
                                          PricePerUnit = 3,
                                          TotalUnits = 30
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo6.Year, monthsAgo6.Month, 7),
                                          Odometer = 1200,
                                          Distance = 200,
                                          PricePerUnit = 3,
                                          TotalUnits = 30
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo6.Year, monthsAgo6.Month, 14),
                                          Odometer = 1400,
                                          Distance = 200,
                                          PricePerUnit = 3,
                                          TotalUnits = 30
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo6.Year, monthsAgo6.Month, 21),
                                          Odometer = 1600,
                                          Distance = 200,
                                          PricePerUnit = 3,
                                          TotalUnits = 30
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo6.Year, monthsAgo6.Month, 28),
                                          Odometer = 1800,
                                          Distance = 200,
                                          PricePerUnit = 3,
                                          TotalUnits = 30
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo5.Year, monthsAgo5.Month, 1),
                                          Odometer = 2000,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 40
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo5.Year, monthsAgo5.Month, 7),
                                          Odometer = 2200,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 40
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo5.Year, monthsAgo5.Month, 14),
                                          Odometer = 2400,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 40
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo5.Year, monthsAgo5.Month, 21),
                                          Odometer = 2600,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 40
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo5.Year, monthsAgo5.Month, 28),
                                          Odometer = 2800,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 40
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo4.Year, monthsAgo4.Month, 1),
                                          Odometer = 3000,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 20
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo4.Year, monthsAgo4.Month, 7),
                                          Odometer = 3200,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 20
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo4.Year, monthsAgo4.Month, 14),
                                          Odometer = 3400,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 20
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo4.Year, monthsAgo4.Month, 21),
                                          Odometer = 3600,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 20
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo4.Year, monthsAgo4.Month, 28),
                                          Odometer = 3800,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 20
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo3.Year, monthsAgo3.Month, 1),
                                          Odometer = 4000,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 10
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo3.Year, monthsAgo3.Month, 7),
                                          Odometer = 4200,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 10
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo3.Year, monthsAgo3.Month, 14),
                                          Odometer = 4400,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 10
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo3.Year, monthsAgo3.Month, 21),
                                          Odometer = 4600,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 10
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo3.Year, monthsAgo3.Month, 28),
                                          Odometer = 4800,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 10
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo2.Year, monthsAgo2.Month, 1),
                                          Odometer = 5000,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 30
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo2.Year, monthsAgo2.Month, 7),
                                          Odometer = 5200,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 30
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo2.Year, monthsAgo2.Month, 14),
                                          Odometer = 5400,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 30
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo2.Year, monthsAgo2.Month, 21),
                                          Odometer = 5600,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 30
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo2.Year, monthsAgo2.Month, 28),
                                          Odometer = 5800,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 30
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo1.Year, monthsAgo1.Month, 1),
                                          Odometer = 6000,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 50
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo1.Year, monthsAgo1.Month, 7),
                                          Odometer = 6200,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 50
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo1.Year, monthsAgo1.Month, 14),
                                          Odometer = 6400,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 50
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo1.Year, monthsAgo1.Month, 21),
                                          Odometer = 6600,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 50
                                      },
                                  new FillupEntry()
                                      {
                                          Date = new DateTime(monthsAgo1.Year, monthsAgo1.Month, 28),
                                          Odometer = 6800,
                                          Distance = 200,
                                          PricePerUnit = 2,
                                          TotalUnits = 50
                                      },
                              };

            return new ReadOnlyCollection<FillupEntry>(fillUps.ToList());
        }
    }
}