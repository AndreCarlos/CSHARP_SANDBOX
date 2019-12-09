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

namespace MileageStats.Model
{
    public enum FillupUnits
    {
        Gallons,
        Litres
    }

    public class FillupEntry
    {
        private static int tempKey = 0;

        public FillupEntry()
        {
            this.UnitOfMeasure = FillupUnits.Gallons;
            this.Date = DateTime.UtcNow;
            this.FillupEntryId = --tempKey;
        }

        /// <summary>
        /// Identifier for FillupEntry.  Should be unique once persisted.
        /// </summary>
        public int FillupEntryId { get; set; }

        /// <summary>
        /// Identifier for the Vehicle this fillup is related to.  
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// Date of the fillup.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Odometer reading for the fillup.
        /// </summary>
        public int Odometer { get; set; }

        /// <summary>
        /// Price per unit.
        /// </summary>
        public double PricePerUnit { get; set; }

        /// <summary>
        /// Total number of units.
        /// </summary>
        public double TotalUnits { get; set; }

        public FillupUnits UnitOfMeasure { get; set; }

        public string Vendor { get; set; }

        public double TransactionFee { get; set; }

        public string Remarks { get; set; }

        /// <summary>
        /// Total cost of fillup.
        /// </summary>
        public double TotalCost
        {
            get { return (this.PricePerUnit*this.TotalUnits) + this.TransactionFee; }
        }       

        #region Cached Calculations

        /// <summary>
        /// Gets or sets the distance from last fillup.  
        /// </summary>
        public int? Distance { get; set; }

        #endregion
    }
}