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
using System.Linq;
using MileageStats.Model;

namespace MileageStats.Data.SqlCe.Repositories
{
    public class FillupRepository : BaseRepository, IFillupRepository
    {
        public FillupRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(int userId, int vehicleId, FillupEntry fillup)
        {
            fillup.VehicleId = vehicleId;
            this.GetDbSet<FillupEntry>().Add(fillup);
            this.UnitOfWork.SaveChanges();
        }

        public FillupEntry GetFillup(int fillupId)
        {
            return this.GetDbSet<FillupEntry>()
                .Where(v => v.FillupEntryId == fillupId)
                .Single();
        }

        public IEnumerable<FillupEntry> GetFillups(int vehicleId)
        {
            return this.GetDbSet<FillupEntry>()
                .Where(v => v.VehicleId == vehicleId)
                .ToList();
        }
    }
}