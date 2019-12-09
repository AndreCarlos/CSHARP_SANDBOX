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
namespace MileageStats.Web.Models
{
    public class JsonFillupViewModel
    {
        /// <summary>
        /// Identifier for FillupEntry.  Should be unique once persisted.
        /// </summary>
        public int FillupEntryId { get; set; }

        /// <summary>
        /// Date of the fillup.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Odometer reading for the fillup.
        /// </summary>
        public int Odometer { get; set; }

        /// <summary>
        /// Price per unit.
        /// </summary>
        public string PricePerUnit { get; set; }

        /// <summary>
        /// Total number of units.
        /// </summary>
        public string TotalUnits { get; set; }

        /// <summary>
        /// Name of the gas station
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// Any additional transaction fees
        /// </summary>
        public string TransactionFee { get; set; }

        /// <summary>
        /// Optional remarks for this fillup
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// Total cost of the fillup (includes transaction fee)
        /// </summary>
        public string TotalCost { get; set; }

    }
}