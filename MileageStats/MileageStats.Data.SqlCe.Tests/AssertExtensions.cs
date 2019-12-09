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
using System.Linq;
using Xunit.Sdk;

namespace MileageStats.Data.SqlCe.Tests
{
    public static class AssertExtensions
    {
        /// <summary>
        /// Compares two objects set of properties
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <param name="properties"></param>
        public static void PropertiesAreEqual<T>(T object1, T object2, params string[] properties)
            where T : class
        {
            if (object1 == null) throw new ArgumentNullException("object1");
            if (object2 == null) throw new ArgumentNullException("object2");

            var compareProperties = typeof(T).GetProperties().Where(p => properties.Contains(p.Name));

            foreach (var property in compareProperties)
            {
                var object1PropertyValue = property.GetValue(object1, null);
                var object2PropertyValue = property.GetValue(object2, null);

                if (!object.Equals(object1PropertyValue, object2PropertyValue))
                {
                    throw new AssertActualExpectedException(object1PropertyValue, object2PropertyValue,
                                                            "Property values are not equal.");
                }
            }
        }
    }
}