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
using System.Security.Principal;
using System.Web.Security;
using MileageStats.Domain.Models;

namespace MileageStats.Web.Models
{
    // Note: On a development server (Cassini) you may get a serializationexception
    // with custom identities.  
    //
    // See http://connect.microsoft.com/VisualStudio/feedback/details/274696/using-custom-identities-in-asp-net-fails-when-using-the-asp-net-developement-server
    // for more information.
    [Serializable]
    public class MileageStatsIdentity : IIdentity
    {
        public MileageStatsIdentity(string name, string displayName, int userId)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.UserId = userId;
        }

        public MileageStatsIdentity(string name, UserInfo userInfo)
            : this(name, userInfo.DisplayName, userInfo.UserId)
        {
            if (userInfo == null) throw new ArgumentNullException("userInfo");
            this.UserId = userInfo.UserId;
        }

        public MileageStatsIdentity(FormsAuthenticationTicket ticket)
            : this(ticket.Name, UserInfo.FromString(ticket.UserData))
        {
            if (ticket == null) throw new ArgumentNullException("ticket");
        }

        public string Name { get; private set; }

        public string AuthenticationType
        {
            get { return "MileageStats"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string DisplayName { get; private set; }

        public int UserId { get; private set; }
    }
}