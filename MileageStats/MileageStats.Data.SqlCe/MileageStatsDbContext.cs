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
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using MileageStats.Model;

namespace MileageStats.Data.SqlCe
{
    /// <summary>
    /// The context for the MileageStats database.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly",
        Justification = "Inherited from IDbContext interface, which exists to support using.")]
    public partial class MileageStatsDbContext : DbContext, IUnitOfWork
    {        
        /// <summary>
        /// This method sets up the database appropriately for the available model objects.
        /// This method only sets up the data tier.  
        /// Any shared or model level requirements (data validations, etc) are on the model objects themselves.
        /// </summary>
        /// <param name="modelBuilder">The model builder object for creating the data model.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetupUserEntity(modelBuilder);

            SetupVehicleEntity(modelBuilder);

            SetupVehiclePhotoEntity(modelBuilder);

            SetupFillupEntryEntity(modelBuilder);

            SetupVehicleManufacturerEntity(modelBuilder);

            SetupCountryEntity(modelBuilder);

            SetupReminderEntity(modelBuilder);
        }

        private static void SetupReminderEntity(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Reminder>().HasKey(r => new { r.VehicleId, r.ReminderId });
            modelBuilder.Entity<Reminder>().Property(r => r.ReminderId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Reminder>().Property(r => r.Title).IsRequired();
            modelBuilder.Entity<Reminder>().Property(r => r.Title).HasMaxLength(50);
            modelBuilder.Entity<Reminder>().Property(r => r.Remarks).HasMaxLength(250);
        }

        private static void SetupCountryEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasKey(c => c.CountryId);
            modelBuilder.Entity<Country>().Property(c => c.CountryId).HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Country>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<Country>().Property(c => c.Name).HasMaxLength(50);
        }

        private static void SetupVehicleManufacturerEntity(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<VehicleManufacturerInfo>().ToTable("VehicleManufacturerInfos");
            modelBuilder.Entity<VehicleManufacturerInfo>().HasKey(vmi => vmi.VehicleManufacturerInfoId);
            modelBuilder.Entity<VehicleManufacturerInfo>().Property(vmi => vmi.VehicleManufacturerInfoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<VehicleManufacturerInfo>().Property(u => u.Year).IsRequired();
            modelBuilder.Entity<VehicleManufacturerInfo>().Property(u => u.MakeName).IsRequired();
            modelBuilder.Entity<VehicleManufacturerInfo>().Property(u => u.MakeName).HasMaxLength(50);
            modelBuilder.Entity<VehicleManufacturerInfo>().Property(u => u.ModelName).IsRequired();
            modelBuilder.Entity<VehicleManufacturerInfo>().Property(u => u.ModelName).HasMaxLength(50);
        }

        private static void SetupFillupEntryEntity(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<FillupEntry>().HasKey(fe => new { fe.VehicleId, fe.FillupEntryId });
            modelBuilder.Entity<FillupEntry>().Property(fe => fe.FillupEntryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<FillupEntry>().Property(fe => fe.Date).IsRequired();
            modelBuilder.Entity<FillupEntry>().Property(fe => fe.Odometer).IsRequired();            
            modelBuilder.Entity<FillupEntry>().Property(fe => fe.PricePerUnit).IsRequired();
            modelBuilder.Entity<FillupEntry>().Property(fe => fe.TotalUnits).IsRequired();            
            modelBuilder.Entity<FillupEntry>().Property(fe => fe.TransactionFee);            
            modelBuilder.Entity<FillupEntry>().Property(fe => fe.Vendor).HasMaxLength(50);
            modelBuilder.Entity<FillupEntry>().Property(fe => fe.Remarks).HasMaxLength(250);
            modelBuilder.Entity<FillupEntry>().Property(fe => fe.Distance).IsOptional();          
        }

        private static void SetupVehiclePhotoEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehiclePhoto>().ToTable("VehiclePhotos");            
            modelBuilder.Entity<VehiclePhoto>().HasKey(p => new { p.VehicleId, p.VehiclePhotoId });
            modelBuilder.Entity<VehiclePhoto>().Property(p => p.VehiclePhotoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<VehiclePhoto>().Property(p => p.Image).IsMaxLength().IsRequired();
            modelBuilder.Entity<VehiclePhoto>().Property(p => p.ImageMimeType).IsRequired();
            modelBuilder.Entity<VehiclePhoto>().Property(p => p.ImageMimeType).HasMaxLength(100);
        }

        private static void SetupVehicleEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasKey(v => v.VehicleId);
            modelBuilder.Entity<Vehicle>().Property(v => v.VehicleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Vehicle>().Property(v => v.Name).IsRequired();
            modelBuilder.Entity<Vehicle>().Property(v => v.Name).HasMaxLength(100);
            modelBuilder.Entity<Vehicle>().Property(v => v.SortOrder);
            modelBuilder.Entity<Vehicle>().Property(v => v.MakeName).HasMaxLength(50);
            modelBuilder.Entity<Vehicle>().Property(v => v.ModelName).HasMaxLength(50);
            modelBuilder.Entity<Vehicle>().HasOptional(v => v.Photo);
            modelBuilder.Entity<Vehicle>().HasMany(v => v.Fillups);
            modelBuilder.Entity<Vehicle>().HasMany(v => v.Reminders);
        }

        private static void SetupUserEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<User>().Property(u => u.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>().Property(u => u.DisplayName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.DisplayName).HasMaxLength(15);
            modelBuilder.Entity<User>().Property(u => u.AuthorizationId).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.AuthorizationId).HasMaxLength(255);
            modelBuilder.Entity<User>().Property(u => u.Country).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.PostalCode).HasMaxLength(10);
        }


        /// <summary>
        /// Gets or sets the database set of vehicles.
        /// </summary>
        /// <value>
        /// An entity set of vehicles.
        /// </value>
        public DbSet<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Gets or sets the database set of users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the database set of vehicle photos.
        /// </summary>
        /// <value>
        /// An entity set of vehicles.
        /// </value>
        public DbSet<VehiclePhoto> VehiclePhotos { get; set; }

        /// <summary>
        /// Gets or sets the database set of vehicle manufacturer info.
        /// </summary>
        /// <value>
        /// An entity set of vehicle manufacturer information.
        /// </value>
        public DbSet<VehicleManufacturerInfo> VehicleManufacturerInfos { get; set; }

        /// <summary>
        /// Gets or sets the database set of allowed countries.
        /// </summary>
        /// <value>An entity set of strings</value>
        public DbSet<Country> Countries { get; set; }

        /// <summary>
        /// Allows saving changes via the IUnitOfWork interface.
        /// </summary>
        void IUnitOfWork.SaveChanges()
        {
            base.SaveChanges();
        }        
    }
}