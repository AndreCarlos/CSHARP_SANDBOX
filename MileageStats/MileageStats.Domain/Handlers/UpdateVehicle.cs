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
using MileageStats.Domain.Properties;

namespace MileageStats.Domain.Handlers
{
    public class UpdateVehicle
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehiclePhotoRepository _photoRepository;

        public UpdateVehicle(IVehicleRepository vehicleRepository, IVehiclePhotoRepository photoRepository)
        {
            _vehicleRepository = vehicleRepository;
            _photoRepository = photoRepository;
        }

        public virtual void Execute(int userId, ICreateVehicleCommand vehicleForm, HttpPostedFileBase photoFile)
        {
            try
            {
                var existing = _vehicleRepository.GetVehicle(userId, vehicleForm.VehicleId);
                int? photoId = null;

                if (existing != null)
                {
                    if (photoFile != null)
                    {
                        if (existing.PhotoId > 0)
                        {
                            _photoRepository.Delete(existing.PhotoId);
                        }

                        var dataPhoto = photoFile.ConvertToEntity();

                        // should we put this in its own try block?
                        _photoRepository.Create(vehicleForm.VehicleId, dataPhoto);
                        photoId = dataPhoto.VehiclePhotoId = dataPhoto.VehiclePhotoId;
                    }

                    var vehicle = vehicleForm.ConvertToEntity(userId, includeVehicleId: true);
                    vehicle.PhotoId = (photoId != null) ? photoId.Value : existing.PhotoId;
                    _vehicleRepository.Update(vehicle);
                }
                else
                {
                    throw new BusinessServicesException(Resources.UnableToUpdateVehicleExceptionMessage);
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.CannotFindVehicleToUpdateExceptionMessage, ex);
            }
        }
    }
}