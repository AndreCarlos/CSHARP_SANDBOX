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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Models;
using MileageStats.Model;

namespace MileageStats.Domain.Handlers
{
    public static class ViewModelExtensions
    {
        public static Model.VehiclePhoto ConvertToEntity(this HttpPostedFileBase photoFile)
        {
            var stream = photoFile.InputStream;
            var contentType = photoFile.ContentType;

            byte[] buffer = null;

            // load the stream as an Image to verify it is valid.
            var image = Image.FromStream(stream);

            // restream the image to prevent any dependency on photoFile.ContentLength.
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, new ImageFormat(image.RawFormat.Guid));
                buffer = memoryStream.ToArray();
            }
            return new Model.VehiclePhoto { ImageMimeType = contentType, Image = buffer };
        }

        public static Vehicle ConvertToEntity(this ICreateVehicleCommand vehicleForm, int userId, bool includeVehicleId = false)
        {
            if (vehicleForm == null)
            {
                return null;
            }

            var vehicle = new Vehicle
                              {
                                  MakeName = vehicleForm.MakeName,
                                  ModelName = vehicleForm.ModelName,
                                  Name = vehicleForm.Name,
                                  SortOrder = vehicleForm.SortOrder,
                                  Year = vehicleForm.Year,
                                  UserId = userId
                              };

            if (includeVehicleId) vehicle.VehicleId = vehicleForm.VehicleId;

            return vehicle;
        }
    }
}