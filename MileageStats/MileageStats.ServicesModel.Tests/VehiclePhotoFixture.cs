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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MileageStats.Domain.Models;
using Xunit;

namespace MileageStats.ServicesModel.Tests
{
    public class VehiclePhotoFixture
    {
        [Fact]
        public void WhenConstructed_ThenPopulated()
        {
            VehiclePhoto actual = new VehiclePhoto();

            Assert.NotNull(actual);
            Assert.Equal(0, actual.VehiclePhotoId);
            Assert.Null(actual.Image);
            Assert.Null(actual.ImageMimeType);
        }

        [Fact]
        public void WehnVehiclePhotoIdSet_ThenValueUpdated()
        {
            VehiclePhoto target = new VehiclePhoto();

            target.VehiclePhotoId = 4;

            int actual = target.VehiclePhotoId;
            Assert.Equal(4, actual);
        }

        [Fact]
        public void WhenImageSet_ThenValueUpdated()
        {
            VehiclePhoto target = new VehiclePhoto();
            byte[] expected = new byte[] {1, 2, 3};

            target.Image = expected;

            byte[] actual = target.Image;
            Assert.Equal(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        [Fact]
        public void WhenImageSetToNull_ThenValueUpdated()
        {
            VehiclePhoto target = new VehiclePhoto();
            target.Image = new byte[] {1, 2, 3};

            target.Image = null;

            byte[] actual = target.Image;
            Assert.Null(actual);
        }

        [Fact]
        public void WhenImageSetToValidValue_ThenValidationPasses()
        {
            VehiclePhoto target = new VehiclePhoto();
            target.ImageMimeType = "ImageMimeType";

            target.Image = new byte[1];

            var validationContext = new ValidationContext(target, null, null);
            var validationResults = new List<ValidationResult>();
            bool actual = Validator.TryValidateObject(target, validationContext, validationResults, true);

            Assert.True(actual);
            Assert.Equal(0, validationResults.Count);
        }

        [Fact]
        public void WhenImageSetToNull_ThenValidationFails()
        {
            VehiclePhoto target = new VehiclePhoto();
            target.ImageMimeType = "ImageMimeType";

            target.Image = null;

            var validationContext = new ValidationContext(target, null, null);
            var validationResults = new List<ValidationResult>();
            bool actual = Validator.TryValidateObject(target, validationContext, validationResults, true);

            Assert.False(actual);
            Assert.Equal(1, validationResults.Count);
            Assert.Equal(1, validationResults[0].MemberNames.Count());
            Assert.Equal("Image", validationResults[0].MemberNames.First());
        }

        [Fact]
        public void WhenImageMimeTypeSet_ThenValueUpdated()
        {
            VehiclePhoto target = new VehiclePhoto();

            target.ImageMimeType = "ImageMimeType";

            string actual = target.ImageMimeType;
            Assert.Equal("ImageMimeType", actual);
        }

        [Fact]
        public void WhenImageMimeTypeSetToNull_ThenUpdatesValue()
        {
            VehiclePhoto target = new VehiclePhoto();
            target.ImageMimeType = "ImageMimeType";

            target.ImageMimeType = null;

            string actual = target.ImageMimeType;
            Assert.Null(actual);
        }

        [Fact]
        public void WhenImageMimeTypeSetToValidValue_ThenValidationPasses()
        {
            VehiclePhoto target = new VehiclePhoto();
            target.Image = new byte[1];
            target.ImageMimeType = "ImageMimeType";

            var validationContext = new ValidationContext(target, null, null);
            var validationResults = new List<ValidationResult>();
            bool actual = Validator.TryValidateObject(target, validationContext, validationResults, true);

            Assert.True(actual);
            Assert.Equal(0, validationResults.Count);
        }

        [Fact]
        public void WhenImageMimeTypeSetToNull_ThenValidationFails()
        {
            VehiclePhoto target = new VehiclePhoto();
            target.Image = new byte[1];

            target.ImageMimeType = null;

            var validationContext = new ValidationContext(target, null, null);
            var validationResults = new List<ValidationResult>();
            bool actual = Validator.TryValidateObject(target, validationContext, validationResults, true);

            Assert.False(actual);
            Assert.Equal(1, validationResults.Count);
            Assert.Equal(1, validationResults[0].MemberNames.Count());
            Assert.Equal("ImageMimeType", validationResults[0].MemberNames.First());
        }

        [Fact]
        public void WhenImageMimeTypeSetToEmpty_ThenValidationFails()
        {
            VehiclePhoto target = new VehiclePhoto();
            target.Image = new byte[1];
            target.ImageMimeType = string.Empty;

            var validationContext = new ValidationContext(target, null, null);
            var validationResults = new List<ValidationResult>();
            bool actual = Validator.TryValidateObject(target, validationContext, validationResults, true);

            Assert.False(actual);
            Assert.Equal(1, validationResults.Count);
            Assert.Equal(1, validationResults[0].MemberNames.Count());
            Assert.Equal("ImageMimeType", validationResults[0].MemberNames.First());
        }

        [Fact]
        public void WhenImageMimeTypeSetTo101Characters_ThenValidationFails()
        {
            VehiclePhoto target = new VehiclePhoto();
            target.Image = new byte[1];
            target.ImageMimeType = new string('1', 101);

            var validationContext = new ValidationContext(target, null, null);
            var validationResults = new List<ValidationResult>();
            bool actual = Validator.TryValidateObject(target, validationContext, validationResults, true);

            Assert.False(actual);
            Assert.Equal(1, validationResults.Count);
            Assert.Equal(1, validationResults[0].MemberNames.Count());
            Assert.Equal("ImageMimeType", validationResults[0].MemberNames.First());
        }
    }
}