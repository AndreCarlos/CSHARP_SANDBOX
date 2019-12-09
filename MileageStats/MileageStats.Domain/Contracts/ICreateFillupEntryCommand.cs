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
using MileageStats.Model;

namespace MileageStats.Domain.Contracts
{
    public interface ICreateFillupEntryCommand
    {
        /// <summary>
        /// Identifier for FillupEntry.  Should be unique once persisted.
        /// </summary>
        int FillupEntryId { get; set; }

        /// <summary>
        /// Identifier for the Vehicle this fillup is related to.  
        /// </summary>
        int VehicleId { get; set; }

        /// <summary>
        /// Date of the fillup.
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        /// Odometer reading for the fillup.
        /// </summary>
        int Odometer { get; set; }

        /// <summary>
        /// Price per unit.
        /// </summary>
        double PricePerUnit { get; set; }

        /// <summary>
        /// Total number of units.
        /// </summary>
        double TotalUnits { get; set; }

        FillupUnits UnitOfMeasure { get; set; }

        string Vendor { get; set; }

        double TransactionFee { get; set; }

        string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the distance from last fillup.  This is a cached value
        /// and is not expected to be set directly.
        /// </summary>        
        int? Distance { get; set; }
    }
}