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
using MileageStats.Domain.Validators;
using Xunit;

namespace MileageStats.Services.Tests.Validators
{
    public class TextMultilineInputValidatorAttributeFixture
    {
        [Fact]
        public void WhenTextInputLineHasMultipleLines_ThenPassesValidation()
        {
            var validator = new TextMultilineValidatorAttribute();

            validator.Validate("\r\r\n\r", "TestName");
        }

        [Fact]
        public void WhenTextInputLineHasMultipleValidLines_ThenPassesValidation()
        {
            var validator = new TextMultilineValidatorAttribute();

            validator.Validate("ABCDEFGHIJKLMNOPQRSTUVWXYZ\nabcdefghijklmnopqrstuvwxyz_'-,.", "TestName");
        }

        [Fact]
        public void WhenTextInputLineHasAnInvalidLine_ThenFailsValidation()
        {
            var validator = new TextMultilineValidatorAttribute();

            Assert.Throws<ValidationException>(() =>
                                               validator.Validate(
                                                   "ABCDEFGHIJKLMNOPQRSTUVWXYZ\nabcdefghijklmnopqrstuvwxyz$", "TestName"));
        }

        [Fact]
        public void WhenTextInputLineHasDoubleHyphen_ThenFailsValidation()
        {
            var validator = new TextMultilineValidatorAttribute();

            Assert.Throws<ValidationException>(() =>
                                               validator.Validate(
                                                   "ABCDEF--GHIJKLMNOPQRSTUVWXYZ\nabcdefghijklmnopqrstuvwx--yz$", "TestName"));
        }
    }
}