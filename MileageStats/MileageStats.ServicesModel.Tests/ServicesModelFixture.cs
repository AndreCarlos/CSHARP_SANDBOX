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
using MileageStats.Domain.Models;
using Xunit;

namespace MileageStats.ServicesModel.Tests
{
    public class ServicesModelFixture
    {

        [Fact]
        public void WhenValidatingUserWithCountryAsUSAndShortNumericPostalCode_ThenValidationFails()
        {
            var user = new User()
                           {
                               AuthorizationId = "authId",
                               DisplayName = "name",
                               Country = "United States",
                               PostalCode = "123"
                           };
            var exception = Assert.Throws<ValidationException>(() => ValidateUser(user));
            Assert.Contains("PostalCode", exception.ValidationResult.MemberNames);
        }

        [Fact]
        public void WhenValidatingUserWithCountryAsUSAndLongNumericPostalCode_ThenValidationFails()
        {
            var user = new User()
                           {
                               AuthorizationId = "authId",
                               DisplayName = "name",
                               Country = "United States",
                               PostalCode = "123456"
                           };
            var exception = Assert.Throws<ValidationException>(() => ValidateUser(user));
            Assert.Contains("PostalCode", exception.ValidationResult.MemberNames);
        }

        private static void ValidateUser(User user)
        {
            var context = new ValidationContext(user, null, null);
            Validator.ValidateObject(user, context, true);
        }
    }
}