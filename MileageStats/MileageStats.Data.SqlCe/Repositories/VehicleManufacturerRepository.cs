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
using System.Linq;
using MileageStats.Model;

namespace MileageStats.Data.SqlCe.Repositories
{
    /// <summary>
    /// A repository of vehicle year/make/model lookup data.
    /// </summary>
    /// <remarks>
    /// This implementation provides a static instance of the data (cached for the application domain).
    /// </remarks>
    public class VehicleManufacturerRepository : BaseRepository, IVehicleManufacturerRepository
    {
        private static Dictionary<int, Dictionary<string, List<string>>> yearsMakesAndModels;
        private static int[] years;
        private static readonly object syncLock = new object();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VehicleManufacturerRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this.EnsureDataOnce();
        }

        /// <summary>
        /// Gets the manufacturer years for all vehicles.
        /// </summary>
        /// <returns></returns>
        public int[] GetYears()
        {
            return years;
        }

        /// <summary>
        /// Gets the vehicle makes for the specified manufacturer year.
        /// </summary>
        /// <param name="year">The year the vehicle was manufactured.</param>
        /// <returns>
        /// A list of makes for that year (if found); otherwise null.
        /// </returns>
        public string[] GetMakes(int year)
        {
            Dictionary<string, List<string>> makesAndModels;
            if (yearsMakesAndModels.TryGetValue(year, out makesAndModels))
            {
                return makesAndModels.Keys.ToArray();
            }

            return null;
        }

        /// <summary>
        /// Gets the vehicle models for the specified manufacturer year and make.
        /// </summary>
        /// <param name="year">The year the vehicle was manufactured.</param>
        /// <param name="make">The make of the vehicle.</param>
        /// <returns>
        /// A list of models for that year and make; otherwise null.
        /// </returns>
        public string[] GetModels(int year, string make)
        {
            Dictionary<string, List<string>> makesAndModels;
            if (yearsMakesAndModels.TryGetValue(year, out makesAndModels))
            {
                List<string> models;
                if (makesAndModels.TryGetValue(make, out models))
                {
                    return models.ToArray();
                }
            }

            return null;
        }

        /// <summary>
        /// Determines whether the specified year is valid.
        /// </summary>
        /// <param name="year">The year to check.</param>
        /// <returns>
        ///   <c>true</c> if the year is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidYear(int year)
        {
            return yearsMakesAndModels.ContainsKey(year);
        }

        /// <summary>
        /// Determines whether the specified year and make are valid.
        /// </summary>
        /// <param name="year">The year to check.</param>
        /// <param name="make">The make to check.</param>
        /// <returns>
        ///   <c>true</c> if the year and make is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidMake(int year, string make)
        {
            Dictionary<string, List<string>> makesAndModels;
            if (yearsMakesAndModels.TryGetValue(year, out makesAndModels))
            {
                return makesAndModels.ContainsKey(make);
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified year, make, and model are valid.
        /// </summary>
        /// <param name="year">The year to check.</param>
        /// <param name="make">The make to check.</param>
        /// <param name="model">The model to check.</param>
        /// <returns>
        ///   <c>true</c> if the year, make, and model is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidModel(int year, string make, string model)
        {
            Dictionary<string, List<string>> makesAndModels;
            if (yearsMakesAndModels.TryGetValue(year, out makesAndModels))
            {
                List<string> models;
                if (makesAndModels.TryGetValue(make, out models))
                {
                    return models.Contains(model, StringComparer.OrdinalIgnoreCase);
                }
            }

            return false;
        }

        private void EnsureDataOnce()
        {
            // Test and Set Lock approach here prevent duplicate initialization of a static on multiple threads.
            if (yearsMakesAndModels == null)
            {
                lock (syncLock)
                {
                    if (yearsMakesAndModels == null)
                    {
                        yearsMakesAndModels = LoadData(this.GetDbSet<VehicleManufacturerInfo>().AsEnumerable());

                        // Rather than return a new array for years on each call of GetYears(), we cache the array.
                        // Caching Makes/Models is avoided because they are not all likely to be used.
                        if (yearsMakesAndModels.Keys.Count > 0)
                        {
                            years = yearsMakesAndModels.Keys.ToArray();
                        }
                    }
                }
            }
        }

        private static Dictionary<int, Dictionary<string, List<string>>> LoadData(
            IEnumerable<VehicleManufacturerInfo> infos)
        {
            // Sort by year, make, and model
            List<VehicleManufacturerInfo> infoList = new List<VehicleManufacturerInfo>(infos);
            infoList.Sort((x, y) =>
                              {
                                  // Compare Year
                                  int result = x.Year.CompareTo(y.Year);
                                  if (result != 0)
                                  {
                                      return result;
                                  }

                                  // Compare Make
                                  result = string.Compare(x.MakeName, y.MakeName);
                                  if (result != 0)
                                  {
                                      return result;
                                  }

                                  // Compare Model
                                  return string.Compare(x.ModelName, y.ModelName);
                              });

            // Load into a dictionary of dictionaries for high-performance lookup.
            Dictionary<int, Dictionary<string, List<string>>> yearsMakesAndModels =
                new Dictionary<int, Dictionary<string, List<string>>>();

            foreach (var info in infoList)
            {
                AddYearMakeModel(info.Year, info.MakeName, info.ModelName, yearsMakesAndModels);
            }

            return yearsMakesAndModels;
        }

        // A helper routine to ensure uniqueness in the year, make, model hierarchy.
        private static void AddYearMakeModel(int year, string make, string model,
                                             Dictionary<int, Dictionary<string, List<string>>> yearsMakesAndModels)
        {
            //Ensure makes exist for year
            Dictionary<string, List<string>> makesAndModels;
            if (!yearsMakesAndModels.TryGetValue(year, out makesAndModels))
            {
                makesAndModels = new Dictionary<string, List<string>>(StringComparer.CurrentCultureIgnoreCase);
                yearsMakesAndModels.Add(year, makesAndModels);
            }

            // Ensure models exist for a make
            List<string> models;
            if (!makesAndModels.TryGetValue(make, out models))
            {
                models = new List<string>();
                makesAndModels.Add(make, models);
            }

            // Ensure model in the list
            if (!models.Contains(model))
            {
                models.Add(model);
            }
        }
    }
}