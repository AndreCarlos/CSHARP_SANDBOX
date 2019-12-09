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
using System.Web.Mvc;
using MileageStats.Domain.Properties;

namespace MileageStats.Domain.Validators
{
    public class TextLineInputValidatorAttribute : RegularExpressionAttribute, IClientValidatable
    {
        public TextLineInputValidatorAttribute() : base(Resources.TextLineInputValidatorRegEx)
        {
            this.ErrorMessage = Resources.InvalidInputCharacter;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = Resources.InvalidInputCharacter,
                ValidationType = "textlineinput"
            };

            rule.ValidationParameters.Add("pattern", Resources.TextLineInputValidatorRegEx);
            return new List<ModelClientValidationRule>() {rule};
        }
    }
}