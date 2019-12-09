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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlServerCe;
using System.IO;

namespace MileageStats.Data.SqlCe.Tests
{
    internal static class DatabaseTestUtility
    {
        public static void DropCreateMileageStatsDatabase()
        {
            MileageStatsDbContext context = GetDatabaseContext();
            DropAndCreateDatabase(context);
            context.SaveChanges();
        }

        public static void DropCreateAndSeedMileageStatsDatabase()
        {
            MileageStatsDbContext context = GetDatabaseContext();
            DropAndCreateDatabase(context);
            SeedDatabase(context);

            context.SaveChanges();
        }

        private static MileageStatsDbContext GetDatabaseContext()
        {
            // This uses the configuration values in the app.config which point to a TestData.sdf file.
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            return new MileageStatsDbContext();
        }

        private static void DropAndCreateDatabase(MileageStatsDbContext context)
        {
            var replacedContext = ReplaceSqlCeConnection(context);

            if (replacedContext.Database.Exists())
            {
                replacedContext.Database.Delete();
            }
            context.Database.Create();
        }

        private static void SeedDatabase(MileageStatsDbContext context)
        {
            ISeedDatabase seeder = context as ISeedDatabase;
            if (seeder != null)
            {
                seeder.Seed();
            }
        }

        private static DbContext ReplaceSqlCeConnection(DbContext context)
        {
            if (context.Database.Connection is SqlCeConnection)
            {
                SqlCeConnectionStringBuilder builder =
                    new SqlCeConnectionStringBuilder(context.Database.Connection.ConnectionString);
                if (!String.IsNullOrWhiteSpace(builder.DataSource))
                {
                    builder.DataSource = ReplaceDataDirectory(builder.DataSource);
                    return new DbContext(builder.ConnectionString);
                }
            }
            return context;
        }

        private static string ReplaceDataDirectory(string inputString)
        {
            string str = inputString.Trim();
            if (string.IsNullOrEmpty(inputString) ||
                !inputString.StartsWith("|DataDirectory|", StringComparison.InvariantCultureIgnoreCase))
            {
                return str;
            }
            string data = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
            if (string.IsNullOrEmpty(data))
            {
                data = AppDomain.CurrentDomain.BaseDirectory ?? Environment.CurrentDirectory;
            }
            if (string.IsNullOrEmpty(data))
            {
                data = string.Empty;
            }
            int length = "|DataDirectory|".Length;
            if ((inputString.Length > "|DataDirectory|".Length) && ('\\' == inputString["|DataDirectory|".Length]))
            {
                length++;
            }
            return Path.Combine(data, inputString.Substring(length));
        }
    }
}