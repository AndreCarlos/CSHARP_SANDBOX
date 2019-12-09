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
using System.Linq;
using MileageStats.Domain.Models;

namespace MileageStats.Web.Models
{
    public class VehicleListViewModel
    {
        public VehicleListViewModel(IEnumerable<VehicleModel> vehicles)
            : this(vehicles, -1)
        {
        }

        public VehicleListViewModel(IEnumerable<VehicleModel> vehicles, int selectedVehicleId)
        {
            var selected = vehicles.FirstOrDefault(x => x.VehicleId == selectedVehicleId);
            Vehicles = new SelectedItemList<VehicleModel>(vehicles, selected);
            IsCollapsed = (selected != null);
        }

        public SelectedItemList<VehicleModel> Vehicles { get; private set; }

        public VehicleModel SelectedVehicle
        {
            get { return Vehicles.SelectedItem; }
        }

        public bool IsCollapsed { get; set; }
    }
}