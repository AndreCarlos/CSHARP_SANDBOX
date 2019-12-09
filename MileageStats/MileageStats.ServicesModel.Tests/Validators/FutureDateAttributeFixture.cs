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
using System.Linq;
using System.Text;
using Xunit;
using MileageStats.Domain.Validators;
using System.ComponentModel.DataAnnotations;

namespace MileageStats.ServicesModel.Tests
{
    public class FutureDateValidationAttributeFixture
    {
        [Fact]
        public void WhenValueNull_ValidationSucceeds()
        {
            var validator = new FutureDateAttribute();

            var instance = new object();

            var datetime = (DateTime?)null;

            var context = new ValidationContext(instance, null, null);
            ValidationResult result = validator.GetValidationResult(datetime, context);

            Assert.Same(ValidationResult.Success, result);
        }

        [Fact]
        public void WhenValueBeforeDate_ValidationSucceeds()
        {
            var validator = new FutureDateAttribute();

            var instance = new object();

            var datetime = (DateTime?)DateTime.UtcNow.AddDays(1);

            var context = new ValidationContext(instance, null, null);
            ValidationResult result = validator.GetValidationResult(datetime, context);

            Assert.Same(ValidationResult.Success, result);
        }

        [Fact]
        public void WhenValueToday_ValidationSucceeds()
        {
            var validator = new FutureDateAttribute();

            var instance = new object();

            var datetime = (DateTime?)DateTime.UtcNow;

            var context = new ValidationContext(instance, null, null);
            ValidationResult result = validator.GetValidationResult(datetime, context);

            Assert.Same(ValidationResult.Success, result);
        }

        [Fact]
        public void WhenValueInPast_ValidationFails()
        {
            var validator = new FutureDateAttribute();

            var instance = new object();

            var datetime = (DateTime?)DateTime.UtcNow.AddDays(-2);

            var context = new ValidationContext(instance, null, null);
            context.MemberName = "MyDate";
            ValidationResult result = validator.GetValidationResult(datetime, context);

            Assert.NotSame(ValidationResult.Success, result);
            Assert.NotNull(result.ErrorMessage);            
        }
    }
}
