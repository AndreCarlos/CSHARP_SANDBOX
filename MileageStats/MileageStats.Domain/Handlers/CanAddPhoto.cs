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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Properties;

namespace MileageStats.Domain.Handlers
{
    public class CanAddPhoto
    {
        private const int MaxPhotoSizeBytes = 1048576; //1024*1024 = 1 MB

        public virtual IEnumerable<ValidationResult> Execute(Stream stream, int contentLength, string contentType)
        {
            if (stream == null)
            {
                yield return new ValidationResult(Resources.InvalidVehiclePhoto);
            }
            else if (contentLength > MaxPhotoSizeBytes)
            {
                // check ContentLength first, even though it could be deliberately set incorrectly.
                yield return new ValidationResult(Resources.VehiclePhotoTooLarge);
            }
            else if (stream.Length > MaxPhotoSizeBytes)
            {
                // recheck the actual buffer since I cannot trust ContentLength
                yield return new ValidationResult(Resources.VehiclePhotoTooLarge);
            } else
            {
                bool isValidFormat;
                try
                {
                    // load the stream as an Image to verify it is valid.
                    Image.FromStream(stream);
                    isValidFormat = true;
                }
                catch
                {
                    isValidFormat = false;
                }

                if (!isValidFormat) yield return new ValidationResult(Resources.InvalidVehiclePhoto);
            }
        }
    }
}