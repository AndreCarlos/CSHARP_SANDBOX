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
using System.Data;
using System.Linq;
using MileageStats.Model;

namespace MileageStats.Data.SqlCe.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Retrieves the user based on their <paramref name="claimedIdentifier"/>.
        /// </summary>
        /// <param name="claimedIdentifier"></param>
        /// <returns>The <see cref="User"/> populated with a shallow version of a Vehicles and the Vehicle's photo.</returns>
        public User GetByAuthenticatedId(string claimedIdentifier)
        {
            return
                this.GetDbSet<User>()
                    .Where(u => u.AuthorizationId == claimedIdentifier)
                    .Single();
        }

        /// <summary>
        /// Creates a new user in the repository.
        /// </summary>
        /// <param name="newUser"></param>
        public void Create(User newUser)
        {
            this.GetDbSet<User>().Add(newUser);
            this.UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// Updates an existing user in the repository. 
        /// </summary>
        /// <param name="updatedUser"></param>
        /// <remarks>
        /// This will only update the user, not the rest of the object graph.
        /// </remarks>
        public void Update(User updatedUser)
        {
            // For now, we make a simple assumption that the user and vehicles are dirty or new based
            // on if their Id == 0.  This may result in the entire graph being saved again which could
            // be more database activity than needed.
            var oldUser = this.GetDbSet<User>().Where(u => u.UserId == updatedUser.UserId).First();

            oldUser.AuthorizationId = updatedUser.AuthorizationId;
            oldUser.Country = updatedUser.Country;
            oldUser.DisplayName = updatedUser.DisplayName;
            oldUser.PostalCode = updatedUser.PostalCode;
            oldUser.HasRegistered = updatedUser.HasRegistered;

            this.SetEntityState(oldUser, oldUser.UserId == 0
                                             ? EntityState.Added
                                             : EntityState.Modified);
            this.UnitOfWork.SaveChanges();
        }
    }
}
