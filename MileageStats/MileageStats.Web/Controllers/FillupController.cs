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
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Handlers;
using MileageStats.Domain.Models;
using MileageStats.Web.Models;
using MileageStats.Domain.Properties;

namespace MileageStats.Web.Controllers
{
    [Authorize]
    public class FillupController : AuthorizedController
    {
        public FillupController(IUserServices userServices, IServiceLocator serviceLocator)
            : base(userServices, serviceLocator)
        {
        }

        //
        // GET: /Fillup/Details/1
        public ActionResult Details(int id)
        {
            var fillup = Using<GetFillupById>()
                .Execute(id);

            var vehicles = Using<GetVehicleListForUser>()
                .Execute(CurrentUserId);

            var fillupEntries = Using<GetFillupsForVehicle>()
                .Execute(fillup.VehicleId)
                .OrderByDescending(f => f.Date);

            var viewModel = new FillupDetailsViewModel
                                {
                                    VehicleList = new VehicleListViewModel(vehicles, fillup.VehicleId) { IsCollapsed = true },
                                    FillupEntry = fillup,
                                    Fillups = new SelectedItemList<Model.FillupEntry>(fillupEntries, fillup),
                                };

            return View(viewModel);
        }

        //
        // GET: /Fillups/List/1
        public ActionResult List(int vehicleId)
        {
            var vehicles = Using<GetVehicleListForUser>()
                .Execute(CurrentUserId);

            var fillups = Using<GetFillupsForVehicle>()
                .Execute(vehicleId)
                .OrderByDescending(f => f.Date);

            var viewModel = new FillupDetailsViewModel
            {
                VehicleList = new VehicleListViewModel(vehicles, vehicleId) {IsCollapsed = true},
                FillupEntry = fillups.FirstOrDefault(),
                Fillups = new SelectedItemList<Model.FillupEntry>(fillups, fillups.FirstOrDefault()),
            };

            return View(viewModel);
        }

        //
        // GET: /Fillups/Add/1
        public ActionResult Add(int vehicleId)
        {
            var vehicles = Using<GetVehicleListForUser>()
                .Execute(CurrentUserId);

            var vehicle = vehicles.First(v => v.VehicleId == vehicleId);

            var newFillupEntry = new FillupEntryFormModel
                                    {
                                        Odometer = vehicle.Odometer.HasValue ? vehicle.Odometer.Value : 0
                                    };

            var fillups = Using<GetFillupsForVehicle>()
                .Execute(vehicleId)
                .OrderByDescending(f => f.Date);

            var viewModel = new FillupAddViewModel
            {
                VehicleList = new VehicleListViewModel(vehicles, vehicleId) {IsCollapsed = true},
                FillupEntry = newFillupEntry,
                Fillups = new SelectedItemList<Model.FillupEntry>(fillups),
            };

            ViewBag.IsFirstFillup = (!fillups.Any());

            return View(viewModel); 
        }

        //
        // POST: /Fillups/Add/5
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int vehicleId, FillupEntryFormModel model)
        {
            var vehicles = Using<GetVehicleListForUser>()
                .Execute(CurrentUserId );

            if (ModelState.IsValid)
            {
                var errors = Using<CanAddFillup>()
                    .Execute(CurrentUserId, vehicleId, model);

                ModelState.AddModelErrors(errors, "AddFillup");

                if (ModelState.IsValid)
                {
                    Using<AddFillupToVehicle>().Execute(CurrentUserId, vehicleId, model);

                    TempData["LastActionMessage"] = Resources.VehicleController_AddFillupSuccessMessage;
                    return RedirectToAction("List", "Fillup", new { vehicleId = vehicleId });
                }
            }

            var fillups = Using<GetFillupsForVehicle>()
                .Execute(vehicleId)
                .OrderByDescending(f => f.Date);

            var viewModel = new FillupAddViewModel
            {
                VehicleList = new VehicleListViewModel(vehicles, vehicleId) { IsCollapsed = true },
                FillupEntry = model,
                Fillups = new SelectedItemList<Model.FillupEntry>(fillups),
            };

            ViewBag.IsFirstFillup = (!fillups.Any());

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult JsonList(int vehicleId)
        {
            var fillupEntries = Using<GetFillupsForVehicle>()
                .Execute(vehicleId)
                .OrderByDescending(f => f.Date);

            var fillups = ToJsonFillupViewModel(fillupEntries);
            return Json(new
            {
                VehicleId = vehicleId,
                Fillups = fillups
            });
        }

        public static List<JsonFillupViewModel> ToJsonFillupViewModel(IEnumerable<Model.FillupEntry> fillupEntries)
        {
            return fillupEntries.Select(entry => new JsonFillupViewModel
            {
                FillupEntryId = entry.FillupEntryId,
                Date = String.Format("{0:d MMM yyyy}", entry.Date),
                TotalUnits = String.Format("{0:#00.000}", entry.TotalUnits),
                Odometer = entry.Odometer,
                TransactionFee = String.Format("{0:C}", entry.TransactionFee),
                PricePerUnit = String.Format("{0:0.000}", entry.PricePerUnit),
                Remarks = entry.Remarks,
                Vendor = entry.Vendor,
                TotalCost = String.Format("{0:C}", entry.TotalCost)
            }).ToList();
        }
    }
}