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

namespace MileageStats.Model
{
    public class VehiclePhoto
    {
        /// <summary>
        /// Gets or sets the entity ID of vehicle photo.
        /// </summary>
        /// <value>
        /// An integer identifying the entity.
        /// </value>
        public int VehiclePhotoId { get; set; }

        /// <summary>
        /// Gets or sets the entity ID of vehicle the photo is for.
        /// </summary>
        /// <value>
        /// An integer identifying the entity.
        /// </value>
        public int VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the set of bytes that is the image.
        /// </summary>
        /// <value>
        /// An array of bytes.
        /// </value>        
        public byte[] Image { get; set; }

        /// <summary>
        /// Gets or sets the MIME type of the image (e.g. image/bmp, image/gif, image/jpeg, or image/png)
        /// </summary>
        /// <value>
        /// A string.       
        /// </value>
        public string ImageMimeType { get; set; }
    }
}