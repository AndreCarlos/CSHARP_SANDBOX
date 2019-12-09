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
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Models;
using MileageStats.Web.Models;

namespace MileageStats.Web
{
    public static class UserServicesExtensions
    {
        public static User GetUserFromIdentity(this IUserServices services, MileageStatsIdentity identity)
        {
            var user = services.GetUserByClaimedIdentifier(identity.Name);
            return user;
        }
    }
}