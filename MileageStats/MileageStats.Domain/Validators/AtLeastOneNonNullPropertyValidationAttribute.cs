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
using System.ComponentModel.DataAnnotations;
using MileageStats.Domain.Properties;

namespace MileageStats.Domain.Validators
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class AtLeastOneNonNullPropertyValidationAttribute : ValidationAttribute
    {
        private readonly string[] propertyNames;

        public AtLeastOneNonNullPropertyValidationAttribute(params string[] propertyName)
        {
            if (propertyName == null) throw new ArgumentNullException("propertyName");
            if (propertyName.Length < 2)
                throw new ArgumentException("There should be at least two properties specified.", "propertyName");
            this.propertyNames = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Since this attribute can only be applied to a class level, value will always be
            // the object under test.
            var instance = value;

            var targetType = instance.GetType();
            foreach (var propertyName in this.propertyNames)
            {
                var propertyInfo = targetType.GetProperty(propertyName);
                var propertyValue = propertyInfo.GetValue(instance, null);
                if (propertyValue != null) return ValidationResult.Success;
            }

            var errorMessage = this.ErrorMessageString;
            if (String.IsNullOrEmpty(errorMessage))
            {
                errorMessage = Resources.AtLeastOneNonNullPropertyValidationAttribute_IsValid_OnePropertyMustHaveValue;
            }

            return new ValidationResult(errorMessage, this.propertyNames);
        }
    }
}