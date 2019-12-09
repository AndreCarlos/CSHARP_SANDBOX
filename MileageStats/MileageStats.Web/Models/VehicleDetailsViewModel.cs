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
using System.Collections.Generic;
using MileageStats.Domain.Models;

namespace MileageStats.Web.Models
{
    public class VehicleDetailsViewModel
    {
        private VehicleListViewModel _vehicleList;
        public VehicleListViewModel VehicleList
        {
            get { return _vehicleList; }
            set
            {
                _vehicleList = value;
                MakeConsistent();
            }
        }

        private VehicleModel _vehicle;
        public VehicleModel Vehicle
        {
            get { return _vehicle; }
            set
            {
                _vehicle = value;
                MakeConsistent();
            }
        }

        public int UserId { get; set; }
        public IEnumerable<ReminderSummaryModel> OverdueReminders { get; set; }

        void MakeConsistent()
        {
            if (Vehicle == null || VehicleList == null) return;

            VehicleList.Vehicles.SelectedItem = Vehicle;
        }
    }
}