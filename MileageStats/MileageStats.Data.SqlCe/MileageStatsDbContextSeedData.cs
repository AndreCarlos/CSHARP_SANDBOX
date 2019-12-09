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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using MileageStats.Data.SqlCe.Properties;
using MileageStats.Model;
using System.Linq;

namespace MileageStats.Data.SqlCe {
    public partial class MileageStatsDbContext : ISeedDatabase {

        #region Seed Data Arrays

        readonly Double[] _price = new[] { 3.5, 3.75, 3.75, 3.65, 3.45, 3.75, 3.75, 3.70, 3.5, 3.65, 3.70, 3.35 };
        readonly Int32[] _distance = new[] { 350, 310, 360, 220, 310, 360, 350, 340, 375, 410, 270, 330 };
        readonly Double[] _units = new[] { 17, 14, 16, 12, 17, 18, 16.5, 17, 17, 19, 14, 17 };
        readonly Double[] _fee = new[] { .45, 0, .50, 0, 0, 0, .30, .45, .50, 0, .45, 0 };
        readonly String[] _vendor = new[] { "Fabrikam", "Contoso", "Margie's Travel", "Adventure Works", "Fabrikam", "Contoso", "Margie's Travel", "Adventure Works", "Fabrikam", "Contoso", "Margie's Travel", "Adventure Works" };

        #endregion

        public void Seed() {
            this.SeedVehicleManufacturers();
            this.SeedCountries();
            this.SaveChanges();
            this.SeedVehicles(SeedUser());
        }

        void SeedCountries() {
            this.Countries.Add(new Country() { Name = "Afghanistan" });
            this.Countries.Add(new Country() { Name = "Albania" });
            this.Countries.Add(new Country() { Name = "Algeria" });
            this.Countries.Add(new Country() { Name = "American Samoa" });
            this.Countries.Add(new Country() { Name = "Andorra" });
            this.Countries.Add(new Country() { Name = "Angola" });
            this.Countries.Add(new Country() { Name = "Anguilla" });
            this.Countries.Add(new Country() { Name = "Antarctica" });
            this.Countries.Add(new Country() { Name = "Antigua and Barbuda" });
            this.Countries.Add(new Country() { Name = "Argentina" });
            this.Countries.Add(new Country() { Name = "Armenia" });
            this.Countries.Add(new Country() { Name = "Aruba" });
            this.Countries.Add(new Country() { Name = "Australia" });
            this.Countries.Add(new Country() { Name = "Austria" });
            this.Countries.Add(new Country() { Name = "Azerbaijan" });
            this.Countries.Add(new Country() { Name = "Bahamas, The" });
            this.Countries.Add(new Country() { Name = "Bahrain" });
            this.Countries.Add(new Country() { Name = "Bangladesh" });
            this.Countries.Add(new Country() { Name = "Barbados" });
            this.Countries.Add(new Country() { Name = "Belarus" });
            this.Countries.Add(new Country() { Name = "Belgium" });
            this.Countries.Add(new Country() { Name = "Belize" });
            this.Countries.Add(new Country() { Name = "Benin" });
            this.Countries.Add(new Country() { Name = "Bermuda" });
            this.Countries.Add(new Country() { Name = "Bhutan" });
            this.Countries.Add(new Country() { Name = "Bolivia" });
            this.Countries.Add(new Country() { Name = "Bosnia and Herzegovina" });
            this.Countries.Add(new Country() { Name = "Botswana" });
            this.Countries.Add(new Country() { Name = "Bouvet Island" });
            this.Countries.Add(new Country() { Name = "Brazil" });
            this.Countries.Add(new Country() { Name = "British Indian Ocean Territory" });
            this.Countries.Add(new Country() { Name = "Brunei" });
            this.Countries.Add(new Country() { Name = "Bulgaria" });
            this.Countries.Add(new Country() { Name = "Burkina Faso" });
            this.Countries.Add(new Country() { Name = "Burundi" });
            this.Countries.Add(new Country() { Name = "Cambodia" });
            this.Countries.Add(new Country() { Name = "Cameroon" });
            this.Countries.Add(new Country() { Name = "Canada" });
            this.Countries.Add(new Country() { Name = "Cape Verde" });
            this.Countries.Add(new Country() { Name = "Cayman Islands" });
            this.Countries.Add(new Country() { Name = "Central African Republic" });
            this.Countries.Add(new Country() { Name = "Chad" });
            this.Countries.Add(new Country() { Name = "Chile" });
            this.Countries.Add(new Country() { Name = "China" });
            this.Countries.Add(new Country() { Name = "Christmas Island" });
            this.Countries.Add(new Country() { Name = "Cocos Islands" });
            this.Countries.Add(new Country() { Name = "Colombia" });
            this.Countries.Add(new Country() { Name = "Comoros" });
            this.Countries.Add(new Country() { Name = "Congo" });
            this.Countries.Add(new Country() { Name = "Cook Islands" });
            this.Countries.Add(new Country() { Name = "Costa Rica" });
            this.Countries.Add(new Country() { Name = "Cote d'Ivoire" });
            this.Countries.Add(new Country() { Name = "Croatia" });
            this.Countries.Add(new Country() { Name = "Cyprus" });
            this.Countries.Add(new Country() { Name = "Czech Republic" });
            this.Countries.Add(new Country() { Name = "Denmark" });
            this.Countries.Add(new Country() { Name = "Djibouti" });
            this.Countries.Add(new Country() { Name = "Dominica" });
            this.Countries.Add(new Country() { Name = "Dominican Republic" });
            this.Countries.Add(new Country() { Name = "Ecuador" });
            this.Countries.Add(new Country() { Name = "Egypt" });
            this.Countries.Add(new Country() { Name = "El Salvador" });
            this.Countries.Add(new Country() { Name = "Equatorial Guinea" });
            this.Countries.Add(new Country() { Name = "Eritrea" });
            this.Countries.Add(new Country() { Name = "Estonia" });
            this.Countries.Add(new Country() { Name = "Ethiopia" });
            this.Countries.Add(new Country() { Name = "Falkland Islands" });
            this.Countries.Add(new Country() { Name = "Faroe Islands" });
            this.Countries.Add(new Country() { Name = "Fiji" });
            this.Countries.Add(new Country() { Name = "Finland" });
            this.Countries.Add(new Country() { Name = "France" });
            this.Countries.Add(new Country() { Name = "French Guiana" });
            this.Countries.Add(new Country() { Name = "French Polynesia" });
            this.Countries.Add(new Country() { Name = "French Southern and Antarctic Lands" });
            this.Countries.Add(new Country() { Name = "Gabon" });
            this.Countries.Add(new Country() { Name = "Gambia, The" });
            this.Countries.Add(new Country() { Name = "Georgia" });
            this.Countries.Add(new Country() { Name = "Germany" });
            this.Countries.Add(new Country() { Name = "Ghana" });
            this.Countries.Add(new Country() { Name = "Gibraltar" });
            this.Countries.Add(new Country() { Name = "Greece" });
            this.Countries.Add(new Country() { Name = "Greenland" });
            this.Countries.Add(new Country() { Name = "Grenada" });
            this.Countries.Add(new Country() { Name = "Guadeloupe" });
            this.Countries.Add(new Country() { Name = "Guam" });
            this.Countries.Add(new Country() { Name = "Guatemala" });
            this.Countries.Add(new Country() { Name = "Guernsey" });
            this.Countries.Add(new Country() { Name = "Guinea" });
            this.Countries.Add(new Country() { Name = "Guinea-Bissau" });
            this.Countries.Add(new Country() { Name = "Guyana" });
            this.Countries.Add(new Country() { Name = "Haiti" });
            this.Countries.Add(new Country() { Name = "Heard Island and McDonald Islands" });
            this.Countries.Add(new Country() { Name = "Honduras" });
            this.Countries.Add(new Country() { Name = "Hong Kong SAR" });
            this.Countries.Add(new Country() { Name = "Hungary" });
            this.Countries.Add(new Country() { Name = "Iceland" });
            this.Countries.Add(new Country() { Name = "India" });
            this.Countries.Add(new Country() { Name = "Indonesia" });
            this.Countries.Add(new Country() { Name = "Iraq" });
            this.Countries.Add(new Country() { Name = "Ireland" });
            this.Countries.Add(new Country() { Name = "Isle of Man" });
            this.Countries.Add(new Country() { Name = "Israel" });
            this.Countries.Add(new Country() { Name = "Italy" });
            this.Countries.Add(new Country() { Name = "Jamaica" });
            this.Countries.Add(new Country() { Name = "Japan" });
            this.Countries.Add(new Country() { Name = "Jersey" });
            this.Countries.Add(new Country() { Name = "Jordan" });
            this.Countries.Add(new Country() { Name = "Kazakhstan" });
            this.Countries.Add(new Country() { Name = "Kenya" });
            this.Countries.Add(new Country() { Name = "Kiribati" });
            this.Countries.Add(new Country() { Name = "Korea" });
            this.Countries.Add(new Country() { Name = "Kuwait" });
            this.Countries.Add(new Country() { Name = "Kyrgyzstan" });
            this.Countries.Add(new Country() { Name = "Laos" });
            this.Countries.Add(new Country() { Name = "Latvia" });
            this.Countries.Add(new Country() { Name = "Lebanon" });
            this.Countries.Add(new Country() { Name = "Lesotho" });
            this.Countries.Add(new Country() { Name = "Liberia" });
            this.Countries.Add(new Country() { Name = "Libya" });
            this.Countries.Add(new Country() { Name = "Liechtenstein" });
            this.Countries.Add(new Country() { Name = "Lithuania" });
            this.Countries.Add(new Country() { Name = "Luxembourg" });
            this.Countries.Add(new Country() { Name = "Macao SAR" });
            this.Countries.Add(new Country() { Name = "Macedonia, Former Yugoslav Republic of" });
            this.Countries.Add(new Country() { Name = "Madagascar" });
            this.Countries.Add(new Country() { Name = "Malawi" });
            this.Countries.Add(new Country() { Name = "Malaysia" });
            this.Countries.Add(new Country() { Name = "Maldives" });
            this.Countries.Add(new Country() { Name = "Mali" });
            this.Countries.Add(new Country() { Name = "Malta" });
            this.Countries.Add(new Country() { Name = "Marshall Islands" });
            this.Countries.Add(new Country() { Name = "Martinique" });
            this.Countries.Add(new Country() { Name = "Mauritania" });
            this.Countries.Add(new Country() { Name = "Mauritius" });
            this.Countries.Add(new Country() { Name = "Mayotte" });
            this.Countries.Add(new Country() { Name = "Mexico" });
            this.Countries.Add(new Country() { Name = "Micronesia" });
            this.Countries.Add(new Country() { Name = "Moldova" });
            this.Countries.Add(new Country() { Name = "Monaco" });
            this.Countries.Add(new Country() { Name = "Mongolia" });
            this.Countries.Add(new Country() { Name = "Montenegro" });
            this.Countries.Add(new Country() { Name = "Montserrat" });
            this.Countries.Add(new Country() { Name = "Morocco" });
            this.Countries.Add(new Country() { Name = "Mozambique" });
            this.Countries.Add(new Country() { Name = "Myanmar" });
            this.Countries.Add(new Country() { Name = "Namibia" });
            this.Countries.Add(new Country() { Name = "Nauru" });
            this.Countries.Add(new Country() { Name = "Nepal" });
            this.Countries.Add(new Country() { Name = "Netherlands" });
            this.Countries.Add(new Country() { Name = "Netherlands Antilles" });
            this.Countries.Add(new Country() { Name = "New Caledonia" });
            this.Countries.Add(new Country() { Name = "New Zealand" });
            this.Countries.Add(new Country() { Name = "Nicaragua" });
            this.Countries.Add(new Country() { Name = "Niger" });
            this.Countries.Add(new Country() { Name = "Nigeria" });
            this.Countries.Add(new Country() { Name = "Niue" });
            this.Countries.Add(new Country() { Name = "Norfolk Island" });
            this.Countries.Add(new Country() { Name = "Northern Mariana Islands" });
            this.Countries.Add(new Country() { Name = "Norway" });
            this.Countries.Add(new Country() { Name = "Oman" });
            this.Countries.Add(new Country() { Name = "Pakistan" });
            this.Countries.Add(new Country() { Name = "Palau" });
            this.Countries.Add(new Country() { Name = "Palestinian Authority" });
            this.Countries.Add(new Country() { Name = "Panama" });
            this.Countries.Add(new Country() { Name = "Papua New Guinea" });
            this.Countries.Add(new Country() { Name = "Paraguay" });
            this.Countries.Add(new Country() { Name = "Peru" });
            this.Countries.Add(new Country() { Name = "Philippines" });
            this.Countries.Add(new Country() { Name = "Pitcairn Islands" });
            this.Countries.Add(new Country() { Name = "Poland" });
            this.Countries.Add(new Country() { Name = "Portugal" });
            this.Countries.Add(new Country() { Name = "Puerto Rico" });
            this.Countries.Add(new Country() { Name = "Qatar" });
            this.Countries.Add(new Country() { Name = "Reunion" });
            this.Countries.Add(new Country() { Name = "Romania" });
            this.Countries.Add(new Country() { Name = "Russia" });
            this.Countries.Add(new Country() { Name = "Rwanda" });
            this.Countries.Add(new Country() { Name = "Saint Helena" });
            this.Countries.Add(new Country() { Name = "Saint Kitts and Nevis" });
            this.Countries.Add(new Country() { Name = "Saint Lucia" });
            this.Countries.Add(new Country() { Name = "Saint Pierre and Miquelon" });
            this.Countries.Add(new Country() { Name = "Saint Vincent and the Grenadines" });
            this.Countries.Add(new Country() { Name = "Samoa" });
            this.Countries.Add(new Country() { Name = "San Marino" });
            this.Countries.Add(new Country() { Name = "Sao Tome and Principe" });
            this.Countries.Add(new Country() { Name = "Saudi Arabia" });
            this.Countries.Add(new Country() { Name = "Senegal" });
            this.Countries.Add(new Country() { Name = "Serbia" });
            this.Countries.Add(new Country() { Name = "Seychelles" });
            this.Countries.Add(new Country() { Name = "Sierra Leone" });
            this.Countries.Add(new Country() { Name = "Singapore" });
            this.Countries.Add(new Country() { Name = "Slovakia" });
            this.Countries.Add(new Country() { Name = "Slovenia" });
            this.Countries.Add(new Country() { Name = "Solomon Islands" });
            this.Countries.Add(new Country() { Name = "Somalia" });
            this.Countries.Add(new Country() { Name = "South Africa" });
            this.Countries.Add(new Country() { Name = "South Georgia and the South Sandwich Islands" });
            this.Countries.Add(new Country() { Name = "Spain" });
            this.Countries.Add(new Country() { Name = "Sri Lanka" });
            this.Countries.Add(new Country() { Name = "Suriname" });
            this.Countries.Add(new Country() { Name = "Svalbard" });
            this.Countries.Add(new Country() { Name = "Swaziland" });
            this.Countries.Add(new Country() { Name = "Sweden" });
            this.Countries.Add(new Country() { Name = "Switzerland" });
            this.Countries.Add(new Country() { Name = "Taiwan" });
            this.Countries.Add(new Country() { Name = "Tajikistan" });
            this.Countries.Add(new Country() { Name = "Tanzania" });
            this.Countries.Add(new Country() { Name = "Thailand" });
            this.Countries.Add(new Country() { Name = "Timor-Leste" });
            this.Countries.Add(new Country() { Name = "Togo" });
            this.Countries.Add(new Country() { Name = "Tokelau" });
            this.Countries.Add(new Country() { Name = "Tonga" });
            this.Countries.Add(new Country() { Name = "Trinidad and Tobago" });
            this.Countries.Add(new Country() { Name = "Tunisia" });
            this.Countries.Add(new Country() { Name = "Turkey" });
            this.Countries.Add(new Country() { Name = "Turkmenistan" });
            this.Countries.Add(new Country() { Name = "Turks and Caicos Islands" });
            this.Countries.Add(new Country() { Name = "Tuvalu" });
            this.Countries.Add(new Country() { Name = "U.S. Minor Outlying Islands" });
            this.Countries.Add(new Country() { Name = "Uganda" });
            this.Countries.Add(new Country() { Name = "Ukraine" });
            this.Countries.Add(new Country() { Name = "United Arab Emirates" });
            this.Countries.Add(new Country() { Name = "United Kingdom" });
            this.Countries.Add(new Country() { Name = "United States" });
            this.Countries.Add(new Country() { Name = "Uruguay" });
            this.Countries.Add(new Country() { Name = "Uzbekistan" });
            this.Countries.Add(new Country() { Name = "Vanuatu" });
            this.Countries.Add(new Country() { Name = "Holy See" });
            this.Countries.Add(new Country() { Name = "Venezuela" });
            this.Countries.Add(new Country() { Name = "Vietnam" });
            this.Countries.Add(new Country() { Name = "Virgin Islands, British" });
            this.Countries.Add(new Country() { Name = "Virgin Islands" });
            this.Countries.Add(new Country() { Name = "Wallis and Futuna" });
            this.Countries.Add(new Country() { Name = "Yemen" });
            this.Countries.Add(new Country() { Name = "Zambia" });
            this.Countries.Add(new Country() { Name = "Zimbabwe" });
            this.Countries.Add(new Country() { Name = "Saint Barthelemy" });
            this.Countries.Add(new Country() { Name = "Saint Martin" });
        }

        void SeedVehicleManufacturers() {
            // Team cars
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 1997, MakeName = "Honda", ModelName = "Accord LX" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2003, MakeName = "BMW", ModelName = "330xi" });

            // Well-known cars
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2010, MakeName = "Audi", ModelName = "A4" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2010, MakeName = "Audi", ModelName = "A6" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2010, MakeName = "Audi", ModelName = "A8" });

            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2010, MakeName = "BMW", ModelName = "330i" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2010, MakeName = "BMW", ModelName = "335i" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2010, MakeName = "BMW", ModelName = "550i" });

            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2010, MakeName = "Honda", ModelName = "Accord" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2010, MakeName = "Honda", ModelName = "CRV" });

            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2010, MakeName = "Toyota", ModelName = "Prius" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2010, MakeName = "Toyota", ModelName = "Sienna" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2010, MakeName = "Toyota", ModelName = "Tacoma" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2010, MakeName = "Toyota", ModelName = "Tundra" });

            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2011, MakeName = "Chevrolet", ModelName = "Camero" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2011, MakeName = "Chevrolet", ModelName = "Colorado" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2011, MakeName = "Chevrolet", ModelName = "Corevette" });

            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2011, MakeName = "Dodge", ModelName = "Challenger" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2011, MakeName = "Dodge", ModelName = "Grand Caravan" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2011, MakeName = "Dodge", ModelName = "Viper" });

            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2011, MakeName = "Ford", ModelName = "Explorer" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2011, MakeName = "Ford", ModelName = "Focus" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2011, MakeName = "Ford", ModelName = "Fusion" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2011, MakeName = "Ford", ModelName = "Mustang" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2011, MakeName = "Ford", ModelName = "Taurus" });

            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2011, MakeName = "Jeep", ModelName = "Grand Cherokee" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2011, MakeName = "Jeep", ModelName = "Liberty" });
            this.VehicleManufacturerInfos.Add(new VehicleManufacturerInfo() { Year = 2011, MakeName = "Jeep", ModelName = "Wrangler" });
        }

        Int32 SeedUser() {
            var user = new User { AuthorizationId = "http://oturner.myidprovider.org/", DisplayName = "Sample User", Country = "United States" };
            Users.Add(user);
            SaveChanges();
            return user.UserId;
        }

        VehiclePhoto CreateVehiclePhoto(Image image, Int32 vehicleId) {
            byte[] buffer;
            using(var memoryStream = new MemoryStream()) {
                image.Save(memoryStream, new ImageFormat(image.RawFormat.Guid));
                buffer = memoryStream.ToArray();
            }
            var vehiclePhoto = new VehiclePhoto { ImageMimeType = "image/jpeg", Image = buffer, VehicleId = vehicleId };
            VehiclePhotos.Add(vehiclePhoto);
            SaveChanges();
            return vehiclePhoto;
        }

        void SeedVehicles(Int32 userId) {

            var vehicle = new Vehicle { UserId = userId, Name = "Hot Rod", SortOrder = 1, Year = 2003, MakeName = "BMW", ModelName = "330xi" };
            Vehicles.Add(vehicle);
            SaveChanges();

            vehicle.Photo = CreateVehiclePhoto(Resources.bmw, vehicle.VehicleId);
            vehicle.PhotoId = vehicle.Photo.VehiclePhotoId;
            SaveChanges();

            CreateFillups(1000, DateTime.Now.AddDays(-365), vehicle, 1, 1);
            CreateReminders(vehicle);

            vehicle = new Vehicle { UserId = userId, Name = "Soccer Mom's Ride", SortOrder = 2, Year = 1997, MakeName = "Honda", ModelName = "Accord LX" };
            Vehicles.Add(vehicle);
            SaveChanges();

            vehicle.Photo = CreateVehiclePhoto(Resources.soccermomcar, vehicle.VehicleId);
            vehicle.PhotoId = vehicle.Photo.VehiclePhotoId;
            SaveChanges();

            CreateFillups(500, DateTime.Now.AddDays(-370), vehicle, .9, 1.2);
            CreateReminders(vehicle);

            vehicle = new Vehicle { UserId = userId, Name = "Mud Lover", SortOrder = 3, Year = 2011, MakeName = "Jeep", ModelName = "Wrangler" };
            Vehicles.Add(vehicle);
            SaveChanges();

            vehicle.Photo = CreateVehiclePhoto(Resources.jeep, vehicle.VehicleId);
            vehicle.PhotoId = vehicle.Photo.VehiclePhotoId;
            SaveChanges();

            CreateFillups(750, DateTime.Now.AddDays(-373), vehicle, 1.2, .8);
            CreateReminders(vehicle);
        }

        void CreateReminders(Vehicle vehicle) {
            FillupEntry lastFillup = vehicle.Fillups.OrderByDescending(f => f.Date).FirstOrDefault();
            if(lastFillup == null) {
                return;
            }

            Reminder reminder;

            // create overdue by mileage reminder
            reminder = new Reminder { DueDate = null, DueDistance = lastFillup.Odometer - 10, IsFulfilled = false, Remarks = "Check air filter when oil is changed", Title = "Oil Change", VehicleId = vehicle.VehicleId };
            vehicle.Reminders.Add(reminder);

            // create overdue by date reminder
            reminder = new Reminder { DueDate = lastFillup.Date.AddDays(-10), DueDistance = null, IsFulfilled = false, Remarks = "Check condition of the wipers", Title = "Check Wiper Fluid", VehicleId = vehicle.VehicleId };
            vehicle.Reminders.Add(reminder);

            // create to be done soon by mileage reminder
            reminder = new Reminder { DueDate = null, DueDistance = lastFillup.Odometer + 400, IsFulfilled = false, Remarks = "Check air pressure", Title = "Rotate Tires", VehicleId = vehicle.VehicleId };
            vehicle.Reminders.Add(reminder);

            // create to be done soon by date reminder
            reminder = new Reminder { DueDate = DateTime.Now.AddDays(+10), DueDistance = null, IsFulfilled = false, Remarks = "Check air freshener", Title = "Vacuum Car", VehicleId = vehicle.VehicleId };
            vehicle.Reminders.Add(reminder);
        }

        /// <summary>
        /// Randomizes the elements of the array.
        /// </summary>
        /// <param name="array">An array of integers.</param>
        /// <returns>Randomly sorted array</returns>
        Int32[] RandomizeArray(Int32[] array) {
            var random = new Random();
            for(var i = array.Length - 1; i > 0; i--) {
                var swapPosition = random.Next(i + 1);
                var temp = array[i];
                array[i] = array[swapPosition];
                array[swapPosition] = temp;
            }
            return array;
        }

        /// <summary>
        /// Creates the fillups.
        /// </summary>
        /// <param name="odometer">The initial odometer reading</param>
        /// <param name="date">The first date to create a fill up for</param>
        /// <param name="vehicle">The vehicle object to create the fill ups for</param>
        /// <param name="unitsModifier">The units modifier is applied to the total gallons calculation.
        ///   By supplying a different value for each vehicle, the data will be different for each vehicle.
        /// </param>
        /// <param name="distanceModifier">The distance modifier is applied to the distance calculation.
        ///   By supplying a different value for each vehicle, the data will be different for each vehicle.
        /// </param>
        /// <remarks>
        /// Creates random fill up sample data for the vehicle. 
        /// Consumes the data arrays at the top of this class.
        /// Randomizes the index used to access data arrays by creating an array then randomly sorting the array elements.
        /// The "while" loop runs while calculated date is less than the current date.
        /// The date is recalculated each cycle of the while loop, adding a random number of days between 3 and 18 days to the previous value.
        /// </remarks>
        void CreateFillups(Int32 odometer, DateTime date, Vehicle vehicle, Double unitsModifier, Double distanceModifier) {
            var randomArray = RandomizeArray( new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 });
            var currentIndex = 0;
            var random = new Random();
            var isFirst = true;
            while(date < DateTime.Now) {
                var dataIndex = randomArray[currentIndex];
                var distance = (Int32)(_distance[dataIndex] * distanceModifier);
                var fillup = new FillupEntry();
                fillup.Date = date;
                if(isFirst) {
                    isFirst = false;
                    fillup.Distance = null;
                } else {
                    fillup.Distance = distance;
                }
                fillup.Odometer = odometer;
                fillup.PricePerUnit = _price[dataIndex];
                fillup.TotalUnits = _units[dataIndex] * unitsModifier;
                fillup.TransactionFee = _fee[dataIndex];
                fillup.VehicleId = vehicle.VehicleId;
                fillup.Vendor = _vendor[dataIndex];
                odometer += distance;
                vehicle.Fillups.Add(fillup);
                currentIndex += 1;
                if(currentIndex > 11) {
                    currentIndex = 0;
                }
                date = date.AddDays(random.Next(3, 14));
            }
            SaveChanges();
        }
    }
}