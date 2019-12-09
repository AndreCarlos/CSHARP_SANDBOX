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
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Properties;
using MileageStats.Model;

namespace MileageStats.Domain.Handlers
{
    public class GetFillupById
    {
        private readonly IFillupRepository _fillupRepository;

        public GetFillupById(IFillupRepository fillupRepository)
        {
            _fillupRepository = fillupRepository;
        }

        public virtual FillupEntry Execute(int fillupId)
        {
            try
            {
                return _fillupRepository.GetFillup(fillupId);
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.UnableToRetrieveServiceHistory, ex);
            }
        }
    }
}