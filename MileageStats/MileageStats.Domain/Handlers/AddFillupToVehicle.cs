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
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Models;
using MileageStats.Domain.Properties;
using MileageStats.Model;

namespace MileageStats.Domain.Handlers
{
    public class AddFillupToVehicle
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IFillupRepository _fillupRepository;

        public AddFillupToVehicle(IVehicleRepository vehicleRepository, IFillupRepository fillupRepository)
        {
            _vehicleRepository = vehicleRepository;
            _fillupRepository = fillupRepository;
        }

        public virtual void Execute(int userId, int vehicleId, ICreateFillupEntryCommand newFillup)
        {
            if (newFillup == null) throw new ArgumentNullException("newFillup");

            try
            {
                var vehicle = _vehicleRepository.GetVehicle(userId, vehicleId);

                if (vehicle != null)
                {
                    newFillup.VehicleId = vehicleId;
                    var fillup = newFillup;

                    var entity = ToEntry(fillup);
                    AdjustSurroundingFillupEntries(entity);

                    _fillupRepository.Create(userId, vehicleId, entity);

                    newFillup.Distance = entity.Distance;   // update calculated value
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.UnableToAddFillupToVehicleExceptionMessage, ex);
            }
        }

        private void AdjustSurroundingFillupEntries(FillupEntry newFillup)
        {
            if (newFillup == null) throw new ArgumentNullException("newFillup");

            var fillups = _fillupRepository.GetFillups(newFillup.VehicleId);

            // Prior fillups are ordered descending so that FirstOrDefault() chooses the one closest to the new fillup.
            // Secondary ordering is by entry ID ensure a consistent ordering/
            var priorFillup = fillups
                .OrderByDescending(f => f.Date).ThenByDescending(f => f.FillupEntryId)
                .Where(f => (f.Date <= newFillup.Date) && (f.FillupEntryId != newFillup.FillupEntryId)).FirstOrDefault();

            // Prior fillups are ordered ascending that FirstOrDefault() chooses the one closest to the new fillup.
            // Secondary ordering is by entry ID ensure a consistent ordering.
            var nextFillup = fillups
                .OrderBy(f => f.Date).ThenBy(f => f.FillupEntryId)
                .Where(f => (f.Date >= newFillup.Date) && (f.FillupEntryId != newFillup.FillupEntryId)).FirstOrDefault();

            CalculateInterFillupStatistics(newFillup, priorFillup);
            CalculateInterFillupStatistics(nextFillup, newFillup);
        }

        private static void CalculateInterFillupStatistics(FillupEntry fillup, FillupEntry priorFillup)
        {
            if (priorFillup != null && fillup != null)
            {
                fillup.Distance = Math.Abs(fillup.Odometer - priorFillup.Odometer);
            }
        }

        private static FillupEntry ToEntry(ICreateFillupEntryCommand source)
        {
            if (source == null)
            {
                return null;
            }

            var fillup = new FillupEntry
                             {
                                 FillupEntryId = source.FillupEntryId,
                                 Date = source.Date,
                                 Distance = source.Distance,
                                 Odometer = source.Odometer,
                                 PricePerUnit = source.PricePerUnit,
                                 Remarks = source.Remarks,
                                 TotalUnits = source.TotalUnits,
                                 TransactionFee = source.TransactionFee,
                                 VehicleId = source.VehicleId,
                                 Vendor = source.Vendor,
                                 UnitOfMeasure = source.UnitOfMeasure
                             };
            return fillup;
        }
    }
}