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
using System.Web.Mvc;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Models;
using MileageStats.Web.Models;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using MileageStats.Domain.Properties;

namespace MileageStats.Web.Controllers
{
    public class ChartController : AuthorizedController
    {
        private readonly IChartDataService chartDataService;
        private IUserServices userServices;

        public ChartController(IUserServices userServices, IChartDataService chartDataService)
            : base(userServices, null)
        {
            this.chartDataService = chartDataService;
            this.userServices = userServices;
        }

        public ActionResult List()
        {
            return View("FuelEfficiency", this.CurrentUser);
        }

        [Authorize]
        public ActionResult TotalCost()
        {
            return View(this.CurrentUser);
        }

        [Authorize]
        public ActionResult TotalDistance()
        {
            return View(this.CurrentUser);
        }

        [Authorize]
        public ActionResult FuelEfficiency()
        {
            return View(this.CurrentUser);
        }

        //
        // GET: /Chart/FuelEfficiencyChart/5      
        // Note: This method is intentionally not authorized to support secondary image retrievals from some browsers.
        public ActionResult FuelEfficiencyChart(int id)
        {
            byte[] chartBytes = GetChartBytes(id, x => x.AverageFuelEfficiency, Resources.ChartController_AverageFuelEfficiencyChart_Title);

            if (chartBytes != null)
            {
                return new FileContentResult(chartBytes, "image/jpeg");
            }
            else
            {
                return new FilePathResult(this.Url.Content("~/Content/trans_pixel.gif"), "image/gif");
            }
        }

        //
        // GET: /Chart/TotalDistanceChart/5    
        // Note: This method is intentionally not authorized to support secondary image retrievals from some browsers.
        public ActionResult TotalDistanceChart(int id)
        {
            byte[] chartBytes = GetChartBytes(id, x => x.TotalDistance, Resources.ChartController_TotalDistance_Title);

            if (chartBytes != null)
            {
                return new FileContentResult(chartBytes, "image/jpeg");
            }
            else
            {
                return new FilePathResult(this.Url.Content("~/Content/trans_pixel.gif"), "image/gif");
            }
        }

        //
        // GET: /Chart/TotalCostChart/5
        // Note: This method is intentionally not authorized to support secondary image retrievals from some browsers.
        public ActionResult TotalCostChart(int id)
        {
            byte[] chartBytes = GetChartBytes(id, x => x.TotalCost, Resources.ChartController_TotalCost_Title);

            if (chartBytes != null)
            {
                return new FileContentResult(chartBytes, "image/jpeg");
            }
            else
            {
                return new FilePathResult(this.Url.Content("~/Content/trans_pixel.gif"), "image/gif");
            }
        }

        private byte[] GetChartBytes(int userId, Func<StatisticSeriesEntry, double> yValueAccessor, string chartTitle)
        {
            Debug.Assert(yValueAccessor != null);

            var seriesData = this.chartDataService.CalculateSeriesForUser(userId, DateTime.UtcNow.AddMonths(-12), null);

            var myChart = new Chart(width: 800, height: 450)
                .AddTitle(chartTitle)
                .AddLegend();

            if (ChartController.PlotMultipleChartLine(myChart, seriesData.Entries, yValueAccessor))
            {
                return myChart.GetBytes("jpeg");
            }
            else
            {
                return null;
            }            
        }

        public static bool PlotMultipleChartLine(Chart chart, IEnumerable<StatisticSeriesEntry> seriesData, Func<StatisticSeriesEntry, double> yValueAccessor)
        {
            if (chart == null)
            {
                throw new ArgumentNullException("chart");
            }

            bool isDataPlotted = false;
            if (seriesData != null)
            {
                var entriesGroupedById = seriesData.GroupBy(x => x.Id);

                foreach (var entryGroup in entriesGroupedById)
                {
                    isDataPlotted |= PlotSingleChartLine(chart, entryGroup, yValueAccessor);
                };
            }

            return isDataPlotted;
        }

        public static bool PlotSingleChartLine(Chart chart, IEnumerable<StatisticSeriesEntry> seriesData, Func<StatisticSeriesEntry, double> yValueAccessor)
        {
            if (chart == null)
            {
                throw new ArgumentNullException("chart");
            }

            bool isDataPlotted = false;
            if (seriesData != null && seriesData.Count() > 0)
            {
                var xValues = new List<DateTime>();
                var yValues = new List<double>();

                // I add these as DateTime types as charts need them for proper sorting of the x axis.
                DateTime date = DateTime.UtcNow.Date;
                StatisticSeriesEntry lastEntry = null;
                foreach (var entry in seriesData)
                {
                    date = new DateTime(entry.Year, entry.Month, 1);
                    xValues.Add(date);
                    yValues.Add(yValueAccessor(entry));
                    lastEntry = entry;
                }

                // I add a previous data point when there is only a single one to help the graph draw a line.
                if (xValues.Count == 1)
                {
                    xValues.Insert(0, date.AddMonths(-1));
                    yValues.Insert(0, 0.0);
                }

                chart.AddSeries(chartType: "Line",
                   name: lastEntry.Name,
                   xValue: xValues.ToArray(),
                   yValues: yValues.ToArray());

                isDataPlotted = true;
            }

            return isDataPlotted;
        }
    }
}
