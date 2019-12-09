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
using System.Data;
using System.Linq;
using MileageStats.Model;

namespace MileageStats.Data.SqlCe.Repositories
{
    public class VehicleRepository : BaseRepository, IVehicleRepository
    {
        public VehicleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(int userId, Vehicle vehicle)
        {
            vehicle.UserId = userId;
            this.GetDbSet<Vehicle>().Add(vehicle);
            this.UnitOfWork.SaveChanges();
        }

        public Vehicle GetVehicle(int userId, int vehicleId)
        {
            return this.GetDbSet<Vehicle>()
                .Include("Fillups")
                .Include("Reminders")
                .Where(v => v.VehicleId == vehicleId && v.UserId == userId)
                .Single();
        }

        public IEnumerable<Vehicle> GetVehicles(int userId)
        {
            return this.GetDbSet<Vehicle>()
                .Include("Fillups")
                .Include("Reminders")
                .Where(v => v.UserId == userId)
                .ToList();
        }

        public void Update(Vehicle updatedVehicle)
        {
            Vehicle vehicleToUpdate =
                this.GetDbSet<Vehicle>()
                        .Where(v => v.VehicleId == updatedVehicle.VehicleId)
                        .First();

            vehicleToUpdate.Name = updatedVehicle.Name;
            vehicleToUpdate.Year = updatedVehicle.Year;
            vehicleToUpdate.MakeName = updatedVehicle.MakeName;
            vehicleToUpdate.ModelName = updatedVehicle.ModelName;
            vehicleToUpdate.SortOrder = updatedVehicle.SortOrder;
            vehicleToUpdate.PhotoId = updatedVehicle.PhotoId;

            this.SetEntityState(vehicleToUpdate, vehicleToUpdate.VehicleId == 0
                                                     ? EntityState.Added
                                                     : EntityState.Modified);
            this.UnitOfWork.SaveChanges();
        }

        public void Delete(int vehicleId)
        {
            Vehicle vehicle = 
                this.GetDbSet<Vehicle>()
                    .Where(v => v.VehicleId == vehicleId)
                    .Single();
            
            this.GetDbSet<Vehicle>().Remove(vehicle);

            this.UnitOfWork.SaveChanges();
        }
    }
}
