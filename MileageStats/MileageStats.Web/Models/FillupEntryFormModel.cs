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
using System.ComponentModel.DataAnnotations;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Validators;
using MileageStats.Model;
using MileageStats.Domain.Properties;

namespace MileageStats.Web.Models
{
    public class FillupEntryFormModel : ICreateFillupEntryCommand
    {
        private static int tempKey = 0;

        public FillupEntryFormModel()
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
        [Required(ErrorMessageResourceName = "FillupEntryDateRequired", ErrorMessageResourceType = typeof(Resources))]
        [PastDate]
        [StoreRestrictedDate]
        public DateTime Date { get; set; }

        /// <summary>
        /// Odometer reading for the fillup.
        /// </summary>
        [Required(ErrorMessageResourceName = "FillupEntryOdometerRequired", ErrorMessageResourceType = typeof(Resources))]
        [Range(1, 1000000, ErrorMessageResourceName = "FillupEntryOdometerRangeValidationError", ErrorMessageResourceType = typeof(Resources))]
        public int Odometer { get; set; }

        /// <summary>
        /// Price per unit.
        /// </summary>
        [Required(ErrorMessageResourceName = "FillupEntryPricePerUnitRequired", ErrorMessageResourceType = typeof(Resources))]
        [Range(0.1d, 100.0d, ErrorMessageResourceName = "FillupEntryPricePerUnitRangeValidationError", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "FillupEntryPricePerUnitLabelText", ResourceType = typeof(Resources))]
        public double PricePerUnit { get; set; }

        /// <summary>
        /// Total number of units.
        /// </summary>
        [Required(ErrorMessageResourceName = "FillupEntryTotalUnitsRequired", ErrorMessageResourceType = typeof(Resources))]
        [Range(1.0d, 1000.0d, ErrorMessageResourceName = "FillupEntryTotalUnitsRangeValidationError", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "FillupEntryTotalUnitsLabelText", ResourceType = typeof(Resources))]
        public double TotalUnits { get; set; }

        [Required(ErrorMessageResourceName = "FillUpEntryUnitOfMeasureRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "FillupEntryUnitOfMeasureLabelText", ResourceType = typeof(Resources))]
        public FillupUnits UnitOfMeasure { get; set; }

        [TextLineInputValidator]
        [StringLength(20, ErrorMessageResourceName = "FillupEntryVendorStringLengthValidationError", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "FillUpEntryVendorLabelText", ResourceType = typeof(Resources))]
        public string Vendor { get; set; }

        [Required(ErrorMessageResourceName = "FillupEntryTransactionFeeRequired", ErrorMessageResourceType = typeof(Resources))]
        [Range(0.0d, 100.0d, ErrorMessageResourceName = "FillupEntryTransactionFeeRangeValidationError", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "FillupEntryTransactionFeeLabelText", ResourceType = typeof(Resources))]
        public double TransactionFee { get; set; }

        [TextMultilineValidator]
        [StringLength(250, ErrorMessageResourceName = "FillupEntryRemarksStringLengthValidationError", ErrorMessageResourceType = typeof(Resources))]
        public string Remarks { get; set; }

        /// <summary>
        /// Total cost of fillup.
        /// </summary>
        [Display(Name = "FillupEntryTotalCostLabelText", ResourceType = typeof(Resources))]
        public double TotalCost
        {
            get { return (this.PricePerUnit * this.TotalUnits) + this.TransactionFee; }
        }

        /// <summary>
        /// Total cost of fillup.
        /// </summary>
        [Display(Name = "FillupEntryTotalFuelCostLabelText", ResourceType = typeof(Resources))]
        public double TotalFuelCost
        {
            get { return this.PricePerUnit * this.TotalUnits; }
        }

        [Display(Name = "FillupEntryFuelEfficiencyLabelText", ResourceType = typeof(Resources))]
        public double? FuelEfficiency
        {
            get { return this.Distance / this.TotalUnits; }
        }

        [Display(Name = "FillupEntryCostPerUnitLabelText", ResourceType = typeof(Resources))]
        public double? CostPerUnit
        {
            get { return this.TotalCost / this.Distance; }
        }

        #region Cached Calculations

        /// <summary>
        /// Gets or sets the distance from last fillup.  This is a cached value
        /// and is not expected to be set directly.
        /// </summary>        
        public int? Distance { get; set; }

        #endregion
    }
}