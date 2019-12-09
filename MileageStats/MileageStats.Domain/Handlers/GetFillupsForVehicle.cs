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
using System.Collections.ObjectModel;
using System.Linq;
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Properties;
using MileageStats.Model;

namespace MileageStats.Domain.Handlers
{
    public class GetFillupsForVehicle
    {
        private readonly IFillupRepository _fillupRepository;

        public GetFillupsForVehicle(IFillupRepository fillupRepository)
        {
            _fillupRepository = fillupRepository;
        }
        public virtual IEnumerable<FillupEntry> Execute(int vehicleId)
        {
            try
            {
                var fillups = _fillupRepository
                    .GetFillups(vehicleId)
                    .OrderBy(f => f.Date)
                    .ToList();

                return new ReadOnlyCollection<FillupEntry>(fillups);
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.UnableToRetireveFillupsExceptionMessage, ex);
            }
        }
    }
}