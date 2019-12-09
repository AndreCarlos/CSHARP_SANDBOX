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
using System.Linq;
using MileageStats.Model;

namespace MileageStats.Data.SqlCe.Repositories
{
    /// <summary>
    /// A repository for vehicle photos.
    /// </summary>
    public class VehiclePhotoRepository : BaseRepository, IVehiclePhotoRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclePhotoRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for the repository to use when persisting changes.</param>
        public VehiclePhotoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Gets the specified vehicle photo id.
        /// </summary>
        /// <param name="vehiclePhotoId">The vehicle photo id.</param>
        /// <returns>A vehicle photo if found; otherwise null.</returns>
        public VehiclePhoto Get(int vehiclePhotoId)
        {
            return this.GetDbSet<VehiclePhoto>()
                .Where(u => u.VehiclePhotoId == vehiclePhotoId)
                .Single();
        }

        public void Create(int vehicleId, VehiclePhoto photo)
        {
            photo.VehicleId = vehicleId;
            this.GetDbSet<VehiclePhoto>().Add(photo);
            this.UnitOfWork.SaveChanges();
        }

        public void Delete(int photoId)
        {
            var photo = this.Get(photoId);
            if (photo != null)
            {
                this.GetDbSet<VehiclePhoto>().Remove(photo);
                this.UnitOfWork.SaveChanges();
            }
        }
    }
}