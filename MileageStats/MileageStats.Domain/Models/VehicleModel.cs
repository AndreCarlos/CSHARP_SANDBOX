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
using System.ComponentModel.DataAnnotations;
using MileageStats.Domain.Validators;
using MileageStats.Domain.Properties;
using MileageStats.Model;

namespace MileageStats.Domain.Models
{
    public class VehicleModel
    {
        public VehicleModel(Vehicle vehicle, VehicleStatisticsModel statistics)
        {
            Vehicle = vehicle;
            Statistics = statistics;
        }

        /// <summary>
        /// Gets or sets the entity ID of vehicle.
        /// </summary>
        /// <value>
        /// An integer identifying the entity.
        /// </value>
        public int VehicleId { get { return this.Vehicle.VehicleId; } }

        /// <summary>
        /// Gets or sets the name of the vehicle.
        /// </summary>
        /// <value>
        /// A string.
        /// </value>
        [StringLength(20, ErrorMessageResourceName = "VehicleNameStringLengthValidationError", ErrorMessageResourceType = typeof(Resources))]
        [TextLineInputValidator]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "VehicleNameRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "VehicleNameLabelText", ResourceType = typeof(Resources))]
        public string Name
        {
            get { return this.Vehicle.Name; }
        }

        /// <summary>
        /// Gets or sets the sort order relative to other vehicles.
        /// </summary>
        /// <value>
        /// A positive number up to 10,000 or zero.
        /// </value>
        [Range(0, 10000, ErrorMessageResourceName = "VehicleSortOrderRangeValidationError", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "VehicleSortOrderLabelText", ResourceType = typeof(Resources))]
        public int SortOrder
        {
            get { return this.Vehicle.SortOrder; }
        }

        /// <summary>
        /// Gets or sets the manufacturing year of the vehicle.
        /// </summary>
        /// <value>
        /// An integer after 1896.
        /// </value>    
        [Range(1900, 2100, ErrorMessageResourceName = "VehicleYearRangeValidationError", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "VehicleYearLabelText", ResourceType = typeof(Resources))]
        public int? Year
        {
            get { return this.Vehicle.Year; }
        }

        /// <summary>
        /// Gets or sets the make of the vehicle (e.g. Toyota, Ford).
        /// </summary>
        /// <value>
        /// A string.
        /// </value>
        [StringLength(50, ErrorMessageResourceName = "VehicleMakeStringLengthValidationError", ErrorMessageResourceType = typeof(Resources))]
        [TextLineInputValidator]
        [Display(Name = "VehicleMakeLabelText", ResourceType = typeof(Resources))]
        public string MakeName
        {
            get { return this.Vehicle.MakeName; }
        }

        /// <summary>
        /// Gets or sets the model of the vehicle (e.g. Camry, Fiesta)
        /// </summary>
        /// <value>
        /// A string.
        /// </value>
        [StringLength(50, ErrorMessageResourceName = "VehicleModelStringLengthValidationError", ErrorMessageResourceType = typeof(Resources))]
        [TextLineInputValidator]
        [Display(Name = "VehicleModelLabelText", ResourceType = typeof(Resources))]
        public string ModelName
        {
            get { return this.Vehicle.ModelName; }
        }

        /// <summary>
        /// Gets the highest odometer value recorded for the vehicle.
        /// </summary>
        /// <remarks>
        /// This is a calculated value and should not be set directly.
        /// </remarks>
        public int? Odometer
        {
            get { return this.Statistics.Odometer; }
        }

        /// <summary>
        /// The id of the vehicle's photo
        /// </summary>
        public int PhotoId { get { return this.Vehicle.PhotoId; } }

        private Model.Vehicle Vehicle {  get; set; }

        public VehicleStatisticsModel Statistics { get; private set; }
    }
}