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
using System.Web.Mvc;
using MileageStats.Domain.Models;
using MileageStats.Domain.Validators;
using Xunit;

namespace MileageStats.ServicesModel.Tests.Validators
{
    public class PostalCodeValidatorAttributeFixture
    {
        [Fact]
        public void WhenCountryIsNotSetNorIsPostalCodeSet_ThenCustomerIsValid()
        {
            User user = new User() {DisplayName = "Name", AuthorizationId = "AuthId"};

            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = ValidateUserPostalCode(user, ref results);

            Assert.True(isValid);
            Assert.Equal(0, results.Count);
        }

        [Fact]
        public void WhenCountryIsNotUnitedStates_ThenAlphaNumericPostalCodeAllowed()
        {
            User user = new User()
                            {DisplayName = "Name", AuthorizationId = "AuthId", Country = "none", PostalCode = "a1b2c3"};

            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = ValidateUserPostalCode(user, ref results);

            Assert.True(isValid);
            Assert.Equal(0, results.Count);
        }

        [Fact]
        public void WhenCountryIsNotSet_ThenAlphaNumericPostalCodeAllowed()
        {
            User user = new User() {DisplayName = "Name", AuthorizationId = "AuthId", PostalCode = "a1b2c3"};

            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = ValidateUserPostalCode(user, ref results);
            Assert.True(isValid);
            Assert.Equal(0, results.Count);
        }

        [Fact]
        public void WhenCountryIsUnitedStates_ThenAlphaNumericPostalCodeIsNotAllowed()
        {
            User user = new User()
                            {
                                DisplayName = "Name",
                                AuthorizationId = "AuthId",
                                Country = "United States",
                                PostalCode = "a1b2c3"
                            };
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = ValidateUserPostalCode(user, ref results);

            Assert.False(isValid);
            Assert.Contains("PostalCode", results[0].MemberNames);
        }

        [Fact]
        public void WhenCountryIsUnitedStates_ThenShortNumericPostalCodeIsNotAllowed()
        {
            User user = new User()
                            {
                                DisplayName = "Name",
                                AuthorizationId = "AuthId",
                                Country = "United States",
                                PostalCode = "125"
                            };
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = ValidateUserPostalCode(user, ref results);

            Assert.False(isValid);
            Assert.Contains("PostalCode", results[0].MemberNames);
        }

        [Fact]
        public void WhenCountryIsUnitedStates_ThenLongNumericPostalCodeIsNotAllowed()
        {
            User user = new User()
                            {
                                DisplayName = "Name",
                                AuthorizationId = "AuthId",
                                Country = "United States",
                                PostalCode = "1234567"
                            };
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = ValidateUserPostalCode(user, ref results);
            Assert.False(isValid);
            Assert.Contains("PostalCode", results[0].MemberNames);
        }

        [Fact]
        public void WhenCountryIsUnitedStates_ThenFiveDigitNumericPostalCodeIsAllowed()
        {
            User user = new User()
                            {
                                DisplayName = "Name",
                                AuthorizationId = "AuthId",
                                Country = "United States",
                                PostalCode = "12345"
                            };

            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = ValidateUserPostalCode(user, ref results);
            Assert.True(isValid);
            Assert.Equal(0, results.Count);
        }

        [Fact]
        public void WhenCreated_ThenImplementsIClientValidatable()
        {
            var validator = new PostalCodeValidatorAttribute();
            Assert.IsAssignableFrom<IClientValidatable>(validator);
        }

        [Fact]
        public void WhenGetClientValidationRules_ThenReturnsValidationRule()
        {
            var validationRules = new PostalCodeValidatorAttribute().GetClientValidationRules(null, null).ToList();
            Assert.Equal(1, validationRules.Count());
            Assert.Equal("postalcode", validationRules[0].ValidationType);
            Assert.Equal(4, validationRules[0].ValidationParameters.Count);
        }

        private static bool ValidateUserPostalCode(User user, ref List<ValidationResult> results)
        {
            var context = new ValidationContext(user, null, null);
            return Validator.TryValidateObject(user, context, results, true);
        }
    }
}