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
using MileageStats.Model;

namespace MileageStats.Data
{
    /// <summary>
    /// A repository for vehicle photos.
    /// </summary>
    /// <remarks>
    /// This repository allows for working with vehicle photos directly without requiring loading a user or vehicle.
    /// </remarks>
    public interface IVehiclePhotoRepository
    {
        /// <summary>
        /// Gets the vehicle photo for the specified ID.
        /// </summary>
        /// <param name="id">The ID of the vehicle photo.</param>
        /// <returns>A vehicle photo if found; otherwise null.</returns>
        VehiclePhoto Get(int id);

        /// <summary>
        /// Creates a vehicle photo
        /// </summary>
        /// <param name="vehicleId">The vehicle the photo is for</param>
        /// <param name="photo">The photo to create</param>
        void Create(int vehicleId, VehiclePhoto photo);

        /// <summary>
        /// Deletes a vehicle photo
        /// </summary>
        /// <param name="photoId">The photo to delete</param>
        void Delete(int photoId);
    }
}