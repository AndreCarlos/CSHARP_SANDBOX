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
using System.Web.Mvc;
using MileageStats.Domain.Validators;
using Xunit;

namespace MileageStats.ServicesModel.Tests.Validators
{
    public class TextLineValidatorAttributeFixture
    {
        [Fact]
        public void WhenTextInputLineHasValidCharacters_ThenPassesValidation()
        {
            var validator = new TextLineInputValidatorAttribute();

            validator.Validate(@"ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz_'-,.", "TestName");
        }

        [Fact]
        public void WhenTextInputLineHasMultipleLines_ThenFailsValidation()
        {
            var validator = new TextLineInputValidatorAttribute();

            Assert.Throws<ValidationException>(
                () => validator.Validate("ABCDEFGHIJKLMNOPQRSTUVWXYZ\nabcdefghijklmnopqrstuvwxyz", "TestName"));
        }

        [Fact]
        public void WhenTextInputLineHasInvalidCharacters_ThenFailsValidation()
        {
            var validator = new TextLineInputValidatorAttribute();

            Assert.Throws<ValidationException>(
                () => validator.Validate("AB<>C", "TestName"));
        }

        [Fact]
        public void WhenTextInputLineHasDoubleHyphen_ThenFailsValidation()
        {
            var validator = new TextLineInputValidatorAttribute();

            Assert.Throws<ValidationException>(
                () => validator.Validate("AB--C", "TestName"));
        }

        [Fact]
        public void WhenCreated_ThenImplementsIClientValidatable()
        {
            var validator = new TextLineInputValidatorAttribute();
            Assert.IsAssignableFrom<IClientValidatable>(validator);
        }

        [Fact]
        public void WhenGetClientValidationRules_ThenReturnsValidationRule()
        {
            var validationRules = new TextLineInputValidatorAttribute().GetClientValidationRules(null, null).ToList();
            Assert.Equal(1, validationRules.Count());
            Assert.Equal("textlineinput", validationRules[0].ValidationType);
            Assert.Equal("Only alpha-numeric characters and [.,_-'] are allowed.", validationRules[0].ErrorMessage);
            Assert.Equal(1, validationRules[0].ValidationParameters.Count);
            Assert.Equal(@"^(?!.*--)[A-Za-z0-9\.,'_ \-]*$", validationRules[0].ValidationParameters["pattern"]);
        }
    }
}