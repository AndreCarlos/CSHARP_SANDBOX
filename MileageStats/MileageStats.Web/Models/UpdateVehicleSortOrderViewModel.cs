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

namespace MileageStats.Web.Models
{
    public class UpdateVehicleSortOrderViewModel
    {
        public string SortOrder { get; set; }

        public int[] VehicleSortOrder
        {
            get
            {
                var newOrderStrings = this.SortOrder.Split(',');

                var newOrder = new int[newOrderStrings.Length];

                for (var i = 0; i < newOrderStrings.Length; i++)
                {
                    newOrder[i] = Convert.ToInt32(newOrderStrings[i]);
                }
                return newOrder;
            }
        }
    }
}