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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MileageStats.Domain.Validators;
using Xunit;

namespace MileageStats.ServicesModel.Tests.Validators
{
    public class AtLeastOnePropertyValidatorFixture
    {
        [Fact]
        public void WhenAllValuesNull_ValidationFails()
        {
            var validator = new AtLeastOneNonNullPropertyValidationAttribute("FirstValue", "SecondValue");
            var instance = new ForValidation
                               {
                                   FirstValue = null,
                                   SecondValue = null
                               };
            var context = new ValidationContext(instance, null, null);
            ValidationResult result = validator.GetValidationResult(instance, context);

            Assert.NotSame(ValidationResult.Success, result);
            Assert.NotNull(result.ErrorMessage);
            Assert.False(result.MemberNames.Except(new[] {"FirstValue", "SecondValue"}).Any());
        }

        [Fact]
        public void WhenOneValueSet_ValidationPasses()
        {
            var validator = new AtLeastOneNonNullPropertyValidationAttribute("FirstValue", "SecondValue");
            var instance = new ForValidation
                               {
                                   FirstValue = 1,
                                   SecondValue = null
                               };
            var context = new ValidationContext(instance, null, null);
            ValidationResult result = validator.GetValidationResult(instance, context);

            Assert.Same(ValidationResult.Success, result);
        }

        [Fact]
        public void WhenAllValuesSet_ValidationPasses()
        {
            var validator = new AtLeastOneNonNullPropertyValidationAttribute("FirstValue", "SecondValue");
            var instance = new ForValidation
                               {
                                   FirstValue = 1,
                                   SecondValue = 2
                               };
            var context = new ValidationContext(instance, null, null);
            ValidationResult result = validator.GetValidationResult(instance, context);

            Assert.Same(ValidationResult.Success, result);
        }

        #region Nested type: ForValidation

        private class ForValidation
        {
            public int? FirstValue { get; set; }
            public int? SecondValue { get; set; }
        }

        #endregion
    }
}