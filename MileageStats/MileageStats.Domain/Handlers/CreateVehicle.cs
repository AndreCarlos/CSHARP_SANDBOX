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
using System.Web;
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Models;
using MileageStats.Domain.Properties;

namespace MileageStats.Domain.Handlers
{
    public class CreateVehicle
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehiclePhotoRepository _photoRepository;

        public CreateVehicle(IVehicleRepository vehicleRepository, IVehiclePhotoRepository photoRepository)
        {
            _vehicleRepository = vehicleRepository;
            _photoRepository = photoRepository;
        }

        public virtual void Execute(int userId, ICreateVehicleCommand vehicleForm, HttpPostedFileBase photoFile)
        {
            if (vehicleForm == null) throw new ArgumentNullException("vehicleForm");

            try
            {
                var vehicle = vehicleForm.ConvertToEntity(userId);
                _vehicleRepository.Create(userId, vehicle);

                if (photoFile == null) return;

                // the double reference between vehicle and photo is a potential source of pain
                var photo = photoFile.ConvertToEntity();
                _photoRepository.Create(vehicle.VehicleId, photo);
                vehicle.PhotoId = photo.VehiclePhotoId;

                _vehicleRepository.Update(vehicle);
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.UnableToCreateVehicleExceptionMessage, ex);
            }
        }
    }
}