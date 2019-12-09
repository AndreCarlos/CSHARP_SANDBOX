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
namespace MileageStats.Data
{
    /// <summary>
    /// A repository of vehicle year/make/model lookup data.
    /// </summary>
    public interface IVehicleManufacturerRepository
    {
        /// <summary>
        /// Gets the manufacturer years for all vehicles.
        /// </summary>
        /// <returns></returns>
        int[] GetYears();

        /// <summary>
        /// Gets the vehicle makes for the specified manufacturer year.
        /// </summary>
        /// <param name="year">The year the vehicle was manufactured.</param>
        /// <returns>A list of makes for that year (if found); otherwise null.</returns>
        string[] GetMakes(int year);

        /// <summary>
        /// Gets the vehicle models for the specified manufacturer year and make.
        /// </summary>
        /// <param name="year">The year the vehicle was manufactured.</param>
        /// <param name="make">The make of the vehicle.</param>
        /// <returns>A list of models for that year and make; otherwise null.</returns>
        string[] GetModels(int year, string make);

        /// <summary>
        /// Determines whether the specified year is valid.
        /// </summary>
        /// <param name="year">The year to check.</param>
        /// <returns>
        ///   <c>true</c> if the year is valid; otherwise, <c>false</c>.
        /// </returns>
        bool IsValidYear(int year);

        /// <summary>
        /// Determines whether the specified year and make are valid.
        /// </summary>
        /// <param name="year">The year to check.</param>
        /// <param name="make">The make to check.</param>
        /// <returns>
        ///   <c>true</c> if the year and make is valid; otherwise, <c>false</c>.
        /// </returns>
        bool IsValidMake(int year, string make);

        /// <summary>
        /// Determines whether the specified year, make, and model are valid.
        /// </summary>
        /// <param name="year">The year to check.</param>
        /// <param name="make">The make to check.</param>
        /// <param name="model">The model to check.</param>
        /// <returns>
        ///   <c>true</c> if the year, make, and model is valid; otherwise, <c>false</c>.
        /// </returns>
        bool IsValidModel(int year, string make, string model);
    }
}