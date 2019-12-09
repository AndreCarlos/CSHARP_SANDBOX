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
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Handlers;
using MileageStats.Domain.Models;
using MileageStats.Web.Models;
using MileageStats.Model;

namespace MileageStats.Web.Controllers
{
    [Authorize]
    public class ReminderController : AuthorizedController
    {

        public ReminderController(IUserServices userServices, IServiceLocator serviceLocator)
            : base(userServices, serviceLocator)
        {
        }

        //
        // POST: /Reminder/OverdueList
        [HttpPost]
        public ActionResult OverdueList()
        {
            var list = Using<GetOverdueRemindersForUser>().Execute(CurrentUserId);

            var reminders = from reminder in list
                            let vehicle = Using<GetVehicleById>().Execute(CurrentUserId, reminder.VehicleId)
                            let title = GetFullTitle(reminder, vehicle)
                            select new OverdueReminderViewModel { FullTitle = title, Reminder = reminder };

            var viewModel = new JsonRemindersOverdueListViewModel { Reminders = reminders.ToList() };

            if (Request.IsAjaxRequest())
            {
                if (Request.ContentType == "application/json")
                {
                    return Json(viewModel);
                }
            }

            return new EmptyResult();
        }

        private static string GetFullTitle(ReminderModel overdueReminder, VehicleModel vehicle)
        {
            string fullTitle = overdueReminder.Title + " | " + vehicle.Name + " @ ";

            if (overdueReminder.DueDate != null)
            {
                fullTitle += overdueReminder.DueDate.Value.ToString("d");
            }

            if (overdueReminder.DueDistance != null)
            {
                if (overdueReminder.DueDate != null)
                {
                    fullTitle += " or ";
                }

                fullTitle += overdueReminder.DueDistance.Value.ToString();
            }

            return fullTitle;
        }

        //
        // GET: /Reminder/Details/1
        public ActionResult Details(int id)
        {
            var reminder = Using<GetReminder>()
                .Execute(id);

            var vehicles = Using<GetVehicleListForUser>()
                .Execute(CurrentUserId);

            var vehicle = vehicles.First(v => v.VehicleId == reminder.VehicleId);

            var reminders = Using<GetUnfulfilledRemindersForVehicle>()
                .Execute(CurrentUserId, reminder.VehicleId, vehicle.Odometer ?? 0)
                .Select(r => new ReminderSummaryModel(r, r.IsOverdue ?? false));

            var viewModel = new ReminderDetailsViewModel
                                {
                                    VehicleList = new VehicleListViewModel(vehicles, vehicle.VehicleId) { IsCollapsed = true },
                                    Reminder = ToFormModel(reminder),
                                    Reminders = new SelectedItemList<ReminderSummaryModel>(reminders, x => x.First(item => item.ReminderId == id)),
                                };

            return View(viewModel);
        }

        //
        // GET: /Reminder/List/1
        public ActionResult List(int vehicleId)
        {
            var vehicles = Using<GetVehicleListForUser>()
                .Execute(CurrentUserId);

            var vehicle = vehicles
                .First(v => v.VehicleId == vehicleId);

            var reminders = Using<GetUnfulfilledRemindersForVehicle>()
                .Execute(CurrentUserId, vehicleId, vehicle.Odometer ?? 0);

            var list = reminders
                .Select(x => new ReminderSummaryModel(x, x.IsOverdue ?? false))
                .ToList();

            var viewModel = new ReminderDetailsViewModel
            {
                VehicleList = new VehicleListViewModel(vehicles, vehicle.VehicleId) { IsCollapsed = true },
                Reminder = ToFormModel(reminders.FirstOrDefault()),
                Reminders = new SelectedItemList<ReminderSummaryModel>(list, Enumerable.FirstOrDefault),
            };

            return View(viewModel);
        }

        //
        // GET: /Reminder/Add/1
        public ActionResult Add(int vehicleId)
        {
            var vehicles = Using<GetVehicleListForUser>()
                .Execute(CurrentUserId);

            var vehicle = vehicles.First(v => v.VehicleId == vehicleId);

            var reminders = Using<GetUnfulfilledRemindersForVehicle>()
                .Execute(CurrentUserId, vehicleId, vehicle.Odometer ?? 0)
                .Select(r => new ReminderSummaryModel(r, r.IsOverdue ?? false));

            var viewModel = new ReminderAddViewModel
                                {
                                    VehicleList = new VehicleListViewModel(vehicles, vehicleId) { IsCollapsed = true },
                                    Reminder = new ReminderFormModel(),
                                    Reminders = new SelectedItemList<ReminderSummaryModel>(reminders),
                                };

            return View(viewModel);
        }

        //
        // POST: /Reminder/Add/1
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int vehicleId, ReminderFormModel reminder)
        {
            if ((reminder != null) && ModelState.IsValid)
            {
                var errors = Using<CanAddReminder>().Execute(CurrentUserId, reminder);
                ModelState.AddModelErrors(errors, "Add");

                if (ModelState.IsValid)
                {
                    Using<AddReminderToVehicle>().Execute(CurrentUserId, vehicleId, reminder);
                    return RedirectToAction("Details", "Reminder", new { id = reminder.ReminderId });
                }
            }

            var vehicles = Using<GetVehicleListForUser>()
                .Execute(CurrentUserId);

            var vehicle = vehicles.First(v => v.VehicleId == vehicleId);

            var reminders = Using<GetUnfulfilledRemindersForVehicle>()
                .Execute(CurrentUserId, vehicleId, vehicle.Odometer ?? 0)
                .Select(r => new ReminderSummaryModel(r, r.IsOverdue ?? false));

            var viewModel = new ReminderAddViewModel
            {
                VehicleList = new VehicleListViewModel(vehicles, vehicleId) { IsCollapsed = true },
                Reminder = reminder,
                Reminders = new SelectedItemList<ReminderSummaryModel>(reminders),
            };

            return View(viewModel);
        }

        //
        // POST: /Reminder/Delete/1
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var reminder = Using<GetReminder>().Execute(id);
            var vehicleId = reminder.VehicleId;

            Using<DeleteReminder>().Execute(CurrentUserId, id);

            return RedirectToAction("List", "Reminder", new { vehicleId = vehicleId });
        }

        //
        // GET: /Reminder/Fulfill/1        
        public ActionResult Fulfill(int id)
        {
            var reminder = Using<GetReminder>().Execute(id);
            var vehicleId = reminder.VehicleId;

            Using<FulfillReminder>().Execute(id);

            return RedirectToAction("List", "Reminder", new { vehicleId = vehicleId });
        }

        //
        // GET: /Reminder/List/1
        public JsonResult JsonList(int vehicleId)
        {
            var vehicles = Using<GetVehicleListForUser>()
                .Execute(CurrentUserId);

            var vehicle = vehicles
                .First(v => v.VehicleId == vehicleId);

            var reminders = Using<GetUnfulfilledRemindersForVehicle>()
                .Execute(CurrentUserId, vehicleId, vehicle.Odometer ?? 0);

            var remindersList = reminders.Select(reminder => ToJsonReminderViewModel(reminder, vehicle)).ToList();

            var viewModel = new JsonRemindersViewModel
                                {
                                    VehicleId = vehicleId,
                                    Reminders = remindersList
                                };
            return Json(viewModel);
        }

        public static JsonReminderViewModel ToJsonReminderViewModel(Reminder reminder, VehicleModel vehicle)
        {
            return new JsonReminderViewModel
                       {
                           VehicleName = vehicle.Name,
                           ReminderId = reminder.ReminderId,
                           VehicleId = vehicle.VehicleId,
                           Title = reminder.Title,
                           Remarks = reminder.Remarks,
                           DueDate = reminder.DueDate.HasValue
                                                    ? String.Format("{0:d MMM yyyy}", reminder.DueDate)
                                                    : null,
                           DueDistance = reminder.DueDistance,
                           IsOverdue = reminder.IsOverdue ?? false,
                           IsFulfilled = reminder.IsFulfilled,
                       };
        }

        //
        // GET: /Reminder/Fulfill/1        
        public JsonResult JsonFulfill(int id)
        {
            Using<FulfillReminder>().Execute(id);

            return new JsonResult();
        }

        [HttpPost]
        public JsonResult JsonImminentReminders()
        {
            var imminentReminders = Using<GetImminentRemindersForUser>().Execute(CurrentUserId, DateTime.UtcNow);
            return Json(imminentReminders);
        }

        static ReminderFormModel ToFormModel(Reminder source)
        {
            if (source == null)
            {
                return null;
            }

            return new ReminderFormModel
            {
                DueDate = source.DueDate,
                DueDistance = source.DueDistance,
                Remarks = source.Remarks,
                ReminderId = source.ReminderId,
                Title = source.Title,
                VehicleId = source.VehicleId,
                IsFulfilled = source.IsFulfilled
            };
        }
    }
}