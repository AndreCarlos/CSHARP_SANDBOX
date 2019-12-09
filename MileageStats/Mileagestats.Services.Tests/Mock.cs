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
using Moq;

namespace MileageStats.Services.Tests
{
    public static class Mock
    {
        public static Mock<HttpPostedFileBase> MockPhotoStream()
        {
            var photoStream = new Mock<HttpPostedFileBase>();
            photoStream.Setup(x => x.InputStream).Returns(CreateImageStream());
            photoStream.Setup(x => x.ContentType).Returns("stuff");
            return photoStream;
        }

        private static MemoryStream CreateImageStream()
        {
            var ms = new MemoryStream();
            var bitmap = new Bitmap(10, 10);
            bitmap.Save(ms, ImageFormat.Bmp);
            return ms;
        }
        
    }
}