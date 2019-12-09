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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Handlers;
using MileageStats.Domain.Models;
using MileageStats.Web.Models;
using VehiclePhoto = MileageStats.Model.VehiclePhoto;

namespace MileageStats.Web.Controllers
{
    public class VehicleController : AuthorizedController
    {
        private readonly IChartDataService chartDataService;
        private readonly ICountryServices countryServices;

        public VehicleController(
            IUserServices userServices,
            ICountryServices countryServices,
            IChartDataService chartDataService,
            IServiceLocator serviceLocator)
            : base(userServices, serviceLocator)
        {
            this.chartDataService = chartDataService;
            this.countryServices = countryServices;
        }

        //
        // GET: /Vehicle/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            var vehicles = Using<GetVehicleListForUser>()
                .Execute(this.CurrentUserId);

            var selected = vehicles
                .Single(x => x.VehicleId == id);

            // we are limiting this to 3 reminders 
            // after we retrieve the full set from the server
            var overdue = Using<GetOverdueRemindersForVehicle>()
                .Execute(id, DateTime.UtcNow, selected.Odometer ?? 0)
                .Take(3);

            var vm = new VehicleDetailsViewModel
                         {
                             VehicleList = new VehicleListViewModel(vehicles, id) { IsCollapsed = true },
                             Vehicle = selected,
                             OverdueReminders = overdue,
                             UserId = CurrentUserId
                         };
            vm.VehicleList.IsCollapsed = true;

            return View(vm);
        }

        [Authorize]
        public ActionResult Add()
        {
            var vehicles = Using<GetVehicleListForUser>()
                .Execute(CurrentUserId);

            AddYearMakeModelSelectListsToViewBag();

            var vm = new VehicleAddViewModel
                         {
                             User = CurrentUser,
                             Vehicle = new VehicleFormModel(),
                             VehiclesList = new VehicleListViewModel(vehicles) { IsCollapsed = true },
                         };
            vm.VehiclesList.IsCollapsed = true;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult Add(FormCollection formValues, VehicleFormModel vehicleForm, HttpPostedFileBase photoFile)
        {
            if (IsSaveOperation(formValues))
            {
                if (TrySaveVehicle(vehicleForm, photoFile)) return RedirectToRoute("Dashboard");
            }

            AddYearMakeModelSelectListsToViewBag(vehicleForm);

            var vehicles = Using<GetVehicleListForUser>().Execute(CurrentUserId);

            var vm = new VehicleAddViewModel
                         {
                             User = CurrentUser,
                             VehiclesList = new VehicleListViewModel(vehicles),
                             Vehicle = vehicleForm
                         };
            vm.VehiclesList.IsCollapsed = true;

            return View(vm);
        }

        private bool IsSaveOperation(FormCollection formValues)
        {
            return (formValues != null) && (formValues["Save"] != null) && (ModelState.IsValid);
        }

        private bool TrySaveVehicle(VehicleFormModel vehicleForm, HttpPostedFileBase photoFile)
        {
            IEnumerable<ValidationResult> vehicleErrors = Using<CanAddVehicle>().Execute(CurrentUserId,
                                                                                                        vehicleForm);
            ModelState.AddModelErrors(vehicleErrors, "Save");

            if (!ModelState.IsValid) return false;

            ValidatePostedPhotoFile(photoFile);

            if (ModelState.IsValid)
            {
                Using<CreateVehicle>().Execute(CurrentUserId, vehicleForm, photoFile);
                return true;
            }
            return false;
        }

        private bool TryUpdateVehicle(VehicleFormModel vehicleForm, HttpPostedFileBase photoFile)
        {
            IEnumerable<ValidationResult> vehicleErrors =
                Using<CanValidateVehicleYearMakeAndModel>().Execute(vehicleForm);
            ModelState.AddModelErrors(vehicleErrors, "Edit");

            if (!ModelState.IsValid) return false;

            ValidatePostedPhotoFile(photoFile);

            if (ModelState.IsValid)
            {
                Using<UpdateVehicle>().Execute(CurrentUserId, vehicleForm, photoFile);
                return true;
            }
            return false;
        }

        private void ValidatePostedPhotoFile(HttpPostedFileBase photoFile)
        {
            if (photoFile == null) return; // the photo is optional, so no validation errors if it's omitted

            IEnumerable<ValidationResult> photoErrors =
                Using<CanAddPhoto>().Execute(photoFile.InputStream,
                                             photoFile.ContentLength,
                                             photoFile.ContentType);

            ModelState.AddModelErrors(photoErrors, "photoFile");
        }

        //
        // GET: /Vehicle/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var vehicles = Using<GetVehicleListForUser>()
                .Execute(CurrentUserId);

            var selected = vehicles
                .FirstOrDefault(x => x.VehicleId == id);

            // adding a flash notification here might provide a better user experience
            if (selected == null) return RedirectToRoute("Dashboard");

            var vehicleForm = ConstructForm(selected);
            AddYearMakeModelSelectListsToViewBag(vehicleForm);

            var vm = new VehicleAddViewModel
                         {
                             User = CurrentUser,
                             Vehicle = vehicleForm,
                             VehiclesList = new VehicleListViewModel(vehicles, id)
                         };
            vm.VehiclesList.IsCollapsed = true;

            return View(vm);
        }

        private static VehicleFormModel ConstructForm(VehicleModel model)
        {
            return new VehicleFormModel
                       {
                           VehicleId = model.VehicleId,
                           Name = model.Name,
                           Year = model.Year,
                           MakeName = model.MakeName,
                           ModelName = model.ModelName,
                           SortOrder = model.SortOrder
                       };
        }

        //
        // POST: /Vehicle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection formValues, VehicleFormModel vehicleForm, HttpPostedFileBase photoFile)
        {
            if (IsSaveOperation(formValues))
            {
                if (TryUpdateVehicle(vehicleForm, photoFile))
                {
                    return RedirectToAction("Details", "Vehicle", new {id = vehicleForm.VehicleId});
                }
            }

            AddYearMakeModelSelectListsToViewBag(vehicleForm);

            var vehicles = Using<GetVehicleListForUser>().Execute(CurrentUserId);

            var vm = new VehicleAddViewModel
                         {
                             User = CurrentUser,
                             VehiclesList = new VehicleListViewModel(vehicles),
                             Vehicle = vehicleForm
                         };
            vm.VehiclesList.IsCollapsed = true;
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
            Using<DeleteVehicle>().Execute(CurrentUserId, id);
            return RedirectToRoute("Dashboard");
        }

        [Authorize]
        public ActionResult List()
        {
            AddCountryListToViewBag();

            var vehicles = Using<GetVehicleListForUser>()
                .Execute(CurrentUserId);

            var imminentReminders = Using<GetImminentRemindersForUser>()
                .Execute(CurrentUserId, DateTime.UtcNow);

            var statistics = Using<GetFleetSummaryStatistics>()
                .Execute(CurrentUserId);

            var model = new DashboardViewModel
                            {
                                User = CurrentUser,
                                VehicleListViewModel = new VehicleListViewModel(vehicles),
                                ImminentReminders = imminentReminders,
                                FleetSummaryStatistics = statistics
                            };

            return View(model);
        }

        private void AddCountryListToViewBag()
        {
            ReadOnlyCollection<string> countryNames = countryServices.GetCountriesAndRegionsList();
            ViewBag.CountryList = new SelectList(countryNames);
        }

        private static SelectList EnumerableToSelectList<T>(IEnumerable<T> source, object selectValue)
        {
            return new SelectList(source.Select(x => new {Value = x.ToString(), Text = x.ToString()}), "Value", "Text",
                                  selectValue);
        }

        private void AddYearMakeModelSelectListsToViewBag(VehicleFormModel vehicle = null)
        {
            var handler = Using<GetYearsMakesAndModels>();

            Tuple<int[], string[], string[]> yearsMakesAndModels = (vehicle != null)
                                                                       ? handler.Execute(vehicle.Year, vehicle.MakeName)
                                                                       : handler.Execute();

            ViewData["Years"] = EnumerableToSelectList(yearsMakesAndModels.Item1,
                                                       (vehicle != null) ? vehicle.Year : null);
            ViewData["Makes"] = EnumerableToSelectList(yearsMakesAndModels.Item2,
                                                       (vehicle != null) ? vehicle.MakeName : null);
            ViewData["Models"] = EnumerableToSelectList(yearsMakesAndModels.Item3,
                                                        (vehicle != null) ? vehicle.ModelName : null);
        }

        #region we might want to move these actions to another controller in the future

        //
        // GET: /Vehicle/Image/5
        [SuppressMessage("Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope", Justification = "FileStreamResult disposes the stream.")]
        public ActionResult Photo(int vehiclePhotoId)
        {
            VehiclePhoto photo = null;
            try
            {
                photo = Using<GetVehiclePhoto>().Execute(vehiclePhotoId);
                return new FileStreamResult(new MemoryStream(photo.Image), photo.ImageMimeType);
            }
            catch (BusinessServicesException)
            {
                //use the default image
                return new FilePathResult(Url.Content("~/Content/vehicle.png"), "image/png");
            }
        }

        //
        // GET: /Vehicle/FuelEfficiencyChart/5/1        
        // Note: This method is intentionally not authorized to support secondary image retrievals from some browsers.
        public ActionResult FuelEfficiencyChart(int userId, int vehicleId)
        {
            byte[] chartBytes = GetVehicleChartBytes(userId, vehicleId, x => x.AverageFuelEfficiency);

            if (chartBytes != null)
            {
                return new FileContentResult(chartBytes, "image/jpeg");
            }
            else
            {
                return new FilePathResult(Url.Content("~/Content/trans_pixel.gif"), "image/gif");
            }
        }

        //
        // GET: /Vehicle/TotalDistanceChart/5/1        
        // Note: This method is intentionally not authorized to support secondary image retrievals from some browsers.

        public ActionResult TotalDistanceChart(int userId, int vehicleId)
        {
            byte[] chartBytes = GetVehicleChartBytes(userId, vehicleId, x => x.TotalDistance);

            if (chartBytes != null)
            {
                return new FileContentResult(chartBytes, "image/jpeg");
            }
            else
            {
                return new FilePathResult(Url.Content("~/Content/trans_pixel.gif"), "image/gif");
            }
        }

        //
        // GET: /Vehicle/TotalCostChart/5/1     
        // Note: This method is intentionally not authorized to support secondary image retrievals from some browsers.

        public ActionResult TotalCostChart(int userId, int vehicleId)
        {
            byte[] chartBytes = GetVehicleChartBytes(userId, vehicleId, x => x.TotalCost);

            if (chartBytes != null)
            {
                return new FileContentResult(chartBytes, "image/jpeg");
            }
            else
            {
                return new FilePathResult(Url.Content("~/Content/trans_pixel.gif"), "image/gif");
            }
        }

        private byte[] GetVehicleChartBytes(int userId, int vehicleId, Func<StatisticSeriesEntry, double> yValueAccessor)
        {
            Debug.Assert(yValueAccessor != null);

            StatisticSeries seriesData = chartDataService.CalculateSeriesForVehicle(userId, vehicleId,
                                                                                    DateTime.UtcNow.AddMonths(-12), null);

            var myChart = new Chart(width: 250, height: 120);

            if (ChartController.PlotSingleChartLine(myChart, seriesData.Entries, yValueAccessor))
            {
                return myChart.GetBytes("jpeg");
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region JSON endpoints

        // All JSON endpoints require [HttpPost] to prevent JSON hijacking.
        // With [HttpPost], returning arrays is allowed.  
        // See http://haacked.com/archive/2009/06/25/json-hijacking.aspx

        [HttpPost]
        [Authorize]
        public JsonResult JsonList()
        {
            var list = Using<GetVehicleListForUser>()
                .Execute(CurrentUserId)
                .Select(x => ToJsonVehicleViewModel(x))
                .ToList();

            return Json(list);
        }

        [HttpPost]
        [Authorize]
        public JsonResult JsonDetails(int id)
        {
            var vehicle = Using<GetVehicleById>()
                .Execute(CurrentUserId, vehicleId: id);

            // we are limiting this to 3 reminders 
            // after we retrieve the full set from the server
            var overdue = Using<GetOverdueRemindersForVehicle>()
                .Execute(id, DateTime.UtcNow, vehicle.Odometer ?? 0)
                .Take(3);

            var vm = ToJsonVehicleViewModel(vehicle, overdue);

            return Json(vm);
        }

        [HttpPost]
        [Authorize]
        public JsonResult JsonDelete(int id)
        {
            Using<DeleteVehicle>().Execute(CurrentUserId, id);
            return new JsonResult();
        }

        [HttpPost]
        [Authorize]
        public void UpdateSortOrder(UpdateVehicleSortOrderViewModel newVehicleListOrder)
        {
            Using<UpdateVehicleSortOrder>().Execute(CurrentUserId, newVehicleListOrder.VehicleSortOrder);
        }

        [HttpPost]
        [Authorize]
        public JsonResult MakesForYear(int year)
        {
            Tuple<int[], string[], string[]> result = Using<GetYearsMakesAndModels>().Execute(filteredToYear: year);
            return Json(result.Item2);
        }

        [HttpPost]
        [Authorize]
        public JsonResult ModelsForMake(int year, string make)
        {
            Tuple<int[], string[], string[]> result = Using<GetYearsMakesAndModels>().Execute(filteredToYear: year,
                                                                                              filteredByMake: make);
            return Json(result.Item3);
        }

        //
        // POST: /Vehicle/JsonAverageFuelEfficiencyChart/1
        [HttpPost]
        [Authorize]
        public JsonResult JsonGetVehicleStatisticSeries(int id)
        {
            StatisticSeries chartData = chartDataService.CalculateSeriesForVehicle(CurrentUserId, id,
                                                                                   DateTime.UtcNow.AddMonths(-12), null);
            return Json(chartData);
        }

        //
        // POST: /Vehicle/JsonFleetStatistics
        [HttpPost]
        [Authorize]
        public JsonResult JsonFleetStatistics()
        {
            var fleetSummaryStatistics = Using<GetFleetSummaryStatistics>().Execute(CurrentUserId);
            return Json(fleetSummaryStatistics);
        }

        private static JsonVehicleViewModel ToJsonVehicleViewModel(VehicleModel vehicle,
                                                                   IEnumerable<ReminderSummaryModel> overdue = null)
        {
            JsonStatisticsViewModel last12Stats = ToJsonStatisticsViewModel(vehicle.Statistics);

            return new JsonVehicleViewModel
                       {
                           VehicleId = vehicle.VehicleId,
                           Name = vehicle.Name,
                           SortOrder = vehicle.SortOrder,
                           Year = vehicle.Year,
                           MakeName = vehicle.MakeName,
                           ModelName = vehicle.ModelName,
                           Odometer = vehicle.Odometer,
                           PhotoId = vehicle.PhotoId,
                           LifeTimeStatistics = new JsonStatisticsViewModel(),
                           //not used
                           LastTwelveMonthsStatistics = last12Stats,
                           OverdueReminders = overdue ?? new List<ReminderSummaryModel>()
                       };
        }

        private static JsonStatisticsViewModel ToJsonStatisticsViewModel(VehicleStatisticsModel statistics)
        {
            return new JsonStatisticsViewModel
                       {
                           Name = statistics.Name,
                           AverageFillupPrice = statistics.AverageFillupPrice,
                           AverageFuelEfficiency = statistics.AverageFuelEfficiency,
                           AverageCostPerMonth = statistics.AverageCostPerMonth,
                           AverageCostToDrive = statistics.AverageCostToDrive,
                           Odometer = statistics.Odometer,
                           TotalDistance = statistics.TotalDistance,
                           TotalFuelCost = statistics.TotalFuelCost,
                           TotalUnits = statistics.TotalUnits,
                           TotalCost = statistics.TotalCost
                       };
        }

        #endregion
    }
}