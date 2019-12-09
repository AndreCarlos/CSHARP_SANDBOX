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
using MileageStats.Data.SqlCe.Repositories;
using MileageStats.Model;
using Xunit;

namespace MileageStats.Data.SqlCe.Tests.Repositories
{
    public class UserRepositoryFixture
    {
        [Fact]
        public void WhenConstructingRepositoryWithNullContext_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => { var userRepository = new UserRepository(null); });
        }

        [Fact]
        public void WhenRequestingAvailableUserByAuthenticatedId_ThenReturnsUserFromRepository()
        {
            DatabaseTestUtility.DropCreateMileageStatsDatabase();

            User userData = new User
                                {
                                    AuthorizationId = "TestId",
                                    DisplayName = "TestDisplayName",
                                };

            using (var dbContext = new MileageStatsDbContext())
            {
                var set = dbContext.Users.Add(userData);
                dbContext.SaveChanges();
            }

            var userRepository = new UserRepository(new MileageStatsDbContext());

            var retrievedUser = userRepository.GetByAuthenticatedId(userData.AuthorizationId);

            Assert.NotNull(retrievedUser);
        }

        [Fact]
        public void WhenRequestingNonExistentUserByAuthenticationId_ThenThrowsInvalidOperationException()
        {
            DatabaseTestUtility.DropCreateMileageStatsDatabase();
            var userRepository = new UserRepository(new MileageStatsDbContext());

            Assert.Throws<InvalidOperationException>(() => userRepository.GetByAuthenticatedId("NotExistingId"));
        }

        [Fact]
        public void WhenAddingUser_ThenUserPersists()
        {
            DatabaseTestUtility.DropCreateMileageStatsDatabase();

            var userRepository = new UserRepository(new MileageStatsDbContext());

            var newUser = new User()
                              {
                                  AuthorizationId = "AnAuthorizationId",
                                  DisplayName = "TheDisplayName",
                              };

            userRepository.Create(newUser);

            Assert.NotNull(
                new MileageStatsDbContext().Users.Where(u => u.AuthorizationId == newUser.AuthorizationId).Single());
        }

        [Fact]
        public void WhenAddingUser_ThenUserReturnsPopulatedNewUser()
        {
            DatabaseTestUtility.DropCreateMileageStatsDatabase();

            var userRepository = new UserRepository(new MileageStatsDbContext());

            string authorizationId = "AnAuthorizationId";
            string displayName = "TheDisplayName";
            var newUser = new User()
                              {
                                  AuthorizationId = authorizationId,
                                  DisplayName = displayName,
                              };

            userRepository.Create(newUser);

            Assert.NotNull(newUser);
            Assert.Equal(1, newUser.UserId);
            Assert.Equal(authorizationId, newUser.AuthorizationId);
            Assert.Equal(displayName, newUser.DisplayName);
        }

        [Fact]
        public void WhenAddingUser_ThenUserReceivesNonZeroId()
        {
            DatabaseTestUtility.DropCreateMileageStatsDatabase();

            var userRepository = new UserRepository(new MileageStatsDbContext());

            var newUser = new User()
                              {
                                  AuthorizationId = "AnAuthorizationId",
                                  DisplayName = "TheDisplayName",
                              };

            Assert.Equal(0, newUser.UserId);
            userRepository.Create(newUser);

            Assert.True(newUser.UserId > 0, "User did not receive non-zero UserId when persisted.");
        }
    }
}