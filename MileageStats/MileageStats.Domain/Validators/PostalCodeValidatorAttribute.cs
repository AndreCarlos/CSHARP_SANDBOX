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
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using MileageStats.Domain.Models;
using MileageStats.Domain.Properties;

namespace MileageStats.Domain.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class PostalCodeValidatorAttribute : ValidationAttribute, IClientValidatable
    {
        private static readonly Regex USPostalCodeRegex = new Regex(Resources.USPostalCodeRegex, RegexOptions.Compiled);

        private static readonly Regex InternationalPostalCodeRegex = new Regex(Resources.InternationalPostalCodeRegex,
                                                                               RegexOptions.Compiled);

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var userToValidate = context.ObjectInstance as User;
            var memberNames = new List<string>() {context.MemberName};

            if (userToValidate != null)
            {
                if (string.IsNullOrEmpty(userToValidate.Country) && string.IsNullOrEmpty(userToValidate.PostalCode))
                {
                    return ValidationResult.Success;
                }
                if (string.IsNullOrEmpty(userToValidate.PostalCode))
                {
                    return ValidationResult.Success;
                }
                if (userToValidate.Country == Resources.UnitedStatesDisplayString)
                {
                    if (USPostalCodeRegex.IsMatch(userToValidate.PostalCode))
                    {
                        return ValidationResult.Success;
                    }
                    return new ValidationResult(Resources.USPostalCodeValidationErrorMessage, memberNames);
                }
                else
                {
                    if (InternationalPostalCodeRegex.IsMatch(userToValidate.PostalCode))
                    {
                        return ValidationResult.Success;
                    }
                    return new ValidationResult(Resources.InternationalPostalCodeValidationErrorMessage,
                                                memberNames);
                }
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = Resources.InvalidInputCharacter,
                ValidationType = "postalcode"
            };

            rule.ValidationParameters.Add("internationalerrormessage", Resources.InternationalPostalCodeValidationErrorMessage);
            rule.ValidationParameters.Add("unitedstateserrormessage", Resources.USPostalCodeValidationErrorMessage);
            rule.ValidationParameters.Add("internationalpattern", Resources.InternationalPostalCodeRegex);
            rule.ValidationParameters.Add("unitedstatespattern", Resources.USPostalCodeRegex);

            return new List<ModelClientValidationRule>() { rule };
        }
    }
}