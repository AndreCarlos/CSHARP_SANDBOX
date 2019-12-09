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
using System.Linq;
using System.Reflection;
using MileageStats.Domain.Handlers;
using Xunit;

namespace Mileagestats.Services.Tests
{
    public class WhenValidatingVehiclePhoto
    {
        private readonly Assembly _assembly = Assembly.GetExecutingAssembly();

        [Fact]
        public void ThenSetsPhotoAndReturnsEmptyValidationResults()
        {
            var stream = _assembly.GetManifestResourceStream("MileageStats.Services.Tests.TestContent.TestVehiclePhoto.png");
            var contentLength = (int) stream.Length;
            var contentType = "/image/png";

            var handler = new CanAddPhoto();
            var result = handler.Execute(stream, contentLength, contentType);

            Assert.Empty(result);
        }

        [Fact]
        public void WithNonImageFile_ThenReturnsValidationError()
        {
            var stream = _assembly.GetManifestResourceStream("MileageStats.Services.Tests.TestContent.NotAnImage.bin");
            var contentLength = (int) stream.Length;
            var contentType = "/image/png";

            var handler = new CanAddPhoto();
            var result = handler.Execute(stream, contentLength, contentType);

            Assert.Equal(1, result.Count());
            Assert.Contains("not an image", result.First().Message, StringComparison.CurrentCultureIgnoreCase);
        }

        [Fact]
        public void WithPhotoThatIsTooLarge_ThenReturnsValidationError()
        {
            var stream = _assembly.GetManifestResourceStream("MileageStats.Services.Tests.TestContent.FileTooBig.jpg");
            var contentLength = (int) stream.Length;
            var contentType = "/image/png";

            var handler = new CanAddPhoto();
            var result = handler.Execute(stream, contentLength, contentType);

            Assert.Equal(1, result.Count());
            Assert.Contains("must be less than", result.First().Message, StringComparison.CurrentCultureIgnoreCase);
        }

        [Fact]
        public void WhenValidateVehiclePhotoWithPhotoThatIsTooLargeAndFakeContentLength_ThenReturnsValidationError()
        {
            var stream = _assembly.GetManifestResourceStream("MileageStats.Services.Tests.TestContent.FileTooBig.jpg");
            var contentLength = 990;
            var contentType = "/image/png";

            var handler = new CanAddPhoto();
            var result = handler.Execute(stream, contentLength, contentType);

            Assert.Equal(1, result.Count());
            Assert.Contains("must be less than", result.First().Message, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}