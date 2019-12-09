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
using MileageStats.Domain.Models;
using Xunit;

namespace MileageStats.ServicesModel.Tests
{
    public class UserFixture
    {
        [Fact]
        public void WhenConstructed_UserHasNotRegistered()
        {
            var actual = new User();

            Assert.False(actual.HasRegistered);
        }

        [Fact]
        public void WhenValidatingUserMissingAuthenticationId_ThenValidationFails()
        {
            var user = new User() {DisplayName = "name"};
            var exception = Assert.Throws<ValidationException>(() => ValidateUser(user));
            Assert.Contains("AuthorizationId", exception.ValidationResult.MemberNames);
        }
         
        [Fact]
        public void WhenValidatingUserWithLongDisplayName_ThenValidationFails()
        {
            var user = new User()
                           {AuthorizationId = "authId", DisplayName = new string('a', 16)};
            var exception = Assert.Throws<ValidationException>(() => ValidateUser(user));
            Assert.Contains("DisplayName", exception.ValidationResult.MemberNames);
        }

        [Fact]
        public void WhenValidatingUserWithLongCountry_ThenValidationFails()
        {
            var user = new User()
                           {
                               DisplayName = "name",
                               AuthorizationId = "authId",
                               Country = "ThisIsAVeryVeryVeryVeryVeryLongStringThatShouldNotBeAllowed"
                           };
            var exception = Assert.Throws<ValidationException>(() => ValidateUser(user));
            Assert.Contains("Country", exception.ValidationResult.MemberNames);
        }

        [Fact]
        public void WhenValidatingUserWithLongPostalCode_ThenValidationFails()
        {
            var user = new User()
                           {AuthorizationId = "authId", DisplayName = "name", PostalCode = "12345678901234567890"};
            var exception = Assert.Throws<ValidationException>(() => ValidateUser(user));
            Assert.Contains("PostalCode", exception.ValidationResult.MemberNames);
        }

        [Fact]
        public void WhenValidatingUserWithNoCountryAndAlphaNumericPostalCode_ThenValidationSucceeds()
        {
            var user = new User() {AuthorizationId = "authId", DisplayName = "name", PostalCode = "a1b2c3"};
            ValidateUser(user);
        }

        [Fact]
        public void WhenValidatingUserWithCountryAsUSAndAlphaNumericPostalCode_ThenValidationFails()
        {
            var user = new User()
                           {
                               AuthorizationId = "authId",
                               DisplayName = "name",
                               Country = "United States",
                               PostalCode = "a1b2c3"
                           };
            var exception = Assert.Throws<ValidationException>(() => ValidateUser(user));
            Assert.Contains("PostalCode", exception.ValidationResult.MemberNames);
        }

        [Fact]
        public void WhenValidatingUserWithCountryAsUSAndFiveDigitNumericPostalCode_ThenValidationSucceeds()
        {
            var user = new User()
                           {
                               AuthorizationId = "authId",
                               DisplayName = "name",
                               Country = "United States",
                               PostalCode = "12345"
                           };
            ValidateUser(user);
        }

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