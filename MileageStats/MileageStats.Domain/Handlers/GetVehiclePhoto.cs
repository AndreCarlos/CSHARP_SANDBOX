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
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Properties;
using MileageStats.Model;

namespace MileageStats.Domain.Handlers
{
    public class GetVehiclePhoto
    {
        private readonly IVehiclePhotoRepository _photoRepository;

        public GetVehiclePhoto(IVehiclePhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public virtual VehiclePhoto Execute(int photoId)
        {
            try
            {
                return _photoRepository.Get(photoId);
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.UnableToRetrieveVehiclePhoto, ex);
            }
        }
    }
}