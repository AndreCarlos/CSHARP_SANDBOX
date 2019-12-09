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
using MileageStats.Domain;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Models;
using Moq;
using Xunit;

namespace MileageStats.Services.Tests
{
    public class UserServicesFixture
    {
        [Fact]
        public void WhenConstructingWithNullUserRepository_ThenThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                new UserServices(null));
        }

        [Fact]
        public void WhenGettingUserWithNullToken_ThenThrowsArgumentNullException()
        {
            var services = new UserServices(new Mock<IUserRepository>().Object);

            var ex = Assert.Throws<BusinessServicesException>(() => services.GetUserByClaimedIdentifier(null));
            Assert.IsType<ArgumentNullException>(ex.InnerException);
        }

        [Fact]
        public void WhenGettingUserWithEmptyToken_ThenThrowsArgumentNullException()
        {
            var services = new UserServices(new Mock<IUserRepository>().Object);
            var ex = Assert.Throws<BusinessServicesException>(() => services.GetUserByClaimedIdentifier(string.Empty));
            Assert.IsType<ArgumentException>(ex.InnerException);
        }

        [Fact]
        public void WhenGettingUser_ThenReturnsUser()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var dataUser = new Model.User {UserId = 1, DisplayName = "test"};

            userRepositoryMock
                .Setup(ur => ur.GetByAuthenticatedId(It.IsAny<string>()))
                .Returns(dataUser);

            var services = new UserServices(userRepositoryMock.Object);

            var returnedUser = services.GetUserByClaimedIdentifier("something");

            Assert.NotNull(returnedUser);
            Assert.Equal("test", returnedUser.DisplayName);
        }

        [Fact]
        public void WhenCreatingUser_ThenDelegatesToUserRepository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            var services = new UserServices(userRepositoryMock.Object);

            services.CreateUser(new User() {AuthorizationId = "authId"});

            userRepositoryMock.Verify(r => r.Create(It.IsAny<MileageStats.Model.User>()), Times.Once());
        }

        [Fact]
        public void WhenCreatingUserAndRepositoryThrows_ThenWrapsException()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Create(It.IsAny<MileageStats.Model.User>()))
                .Throws<InvalidOperationException>();

            var services = new UserServices(userRepositoryMock.Object);

            var ex = Assert.Throws<BusinessServicesException>(
                () => services.CreateUser(new User() {AuthorizationId = "authId"}));
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

        [Fact]
        public void WhenUpdatingUserAndRepositoryThrows_ThenWrapsException()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Update(It.IsAny<MileageStats.Model.User>()))
                .Throws<InvalidOperationException>();

            var services = new UserServices(userRepositoryMock.Object);

            var ex = Assert.Throws<BusinessServicesException>(
                () => services.UpdateUser(new User() {AuthorizationId = "authId"}));
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

        [Fact]
        public void WhenGettingUserAndRepositoryThrows_ThenWrapsException()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.GetByAuthenticatedId(It.IsAny<string>()))
                .Throws<InvalidOperationException>();

            var services = new UserServices(userRepositoryMock.Object);

            var ex = Assert.Throws<BusinessServicesException>(() => services.GetUserByClaimedIdentifier("test"));
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

        [Fact]
        public void WhenCreatingUser_ThenReturnsUpdatedUserWithUserId()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Create(It.IsAny<MileageStats.Model.User>()))
                .Callback<MileageStats.Model.User>(user => user.UserId = 12);
            var services = new UserServices(userRepositoryMock.Object);

            User servicesUser = new User() {AuthorizationId = "authId"};
            Assert.Equal(0, servicesUser.UserId);
            User createdUser = services.CreateUser(servicesUser);

            Assert.Equal(12, createdUser.UserId);
        }

        [Fact]
        public void WhenUpdatingUser_ThenDelegatesToUserRepository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Create(It.IsAny<MileageStats.Model.User>()))
                .Callback<MileageStats.Model.User>(u => u.UserId = 12);

            var services = new UserServices(userRepositoryMock.Object);

            User user = new User() {AuthorizationId = "authId"};
            services.CreateUser(user);
            user.AuthorizationId = "newAuthId";
            services.UpdateUser(user);

            userRepositoryMock.Verify(r => r.Update(It.IsAny<MileageStats.Model.User>()), Times.Once());
        }

        [Fact]
        public void WhenCallingGetOrCreateUserWithNoMatchingUser_ThenUserIsCreated()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Create(It.IsAny<MileageStats.Model.User>()))
                .Callback<MileageStats.Model.User>(user => user.UserId = 12);

            Model.User userToReturn = null;
            userRepositoryMock.Setup(u => u.GetByAuthenticatedId(It.IsAny<string>())).Returns(userToReturn);
            userRepositoryMock.Setup(u => u.Create(It.IsAny<Model.User>()))
                .Callback<MileageStats.Model.User>(user => user.UserId = 12);

            var services = new UserServices(userRepositoryMock.Object);

            User createdUser = services.GetOrCreateUser("claimedId");

            userRepositoryMock.Verify(u => u.Create(It.IsAny<Model.User>()), Times.Once());
        }

        [Fact]
        public void WhenCallingGetOrCreateUserWithNoMatchingUser_ThenUserIsReturned()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Create(It.IsAny<MileageStats.Model.User>()))
                .Callback<MileageStats.Model.User>(user => user.UserId = 12);

            Model.User userToReturn = null;
            userRepositoryMock.Setup(u => u.GetByAuthenticatedId(It.IsAny<string>())).Returns(userToReturn);
            userRepositoryMock.Setup(u => u.Create(It.IsAny<Model.User>()))
                .Callback<MileageStats.Model.User>(user => user.UserId = 12);

            var services = new UserServices(userRepositoryMock.Object);

            User createdUser = services.GetOrCreateUser("claimedId");

            Assert.Equal(12, createdUser.UserId);
        }

        [Fact]
        public void WhenCallingGetOrCreateUserWithMatchingUser_ThenExistingUserIsReturned()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.Create(It.IsAny<MileageStats.Model.User>()))
                .Callback<MileageStats.Model.User>(user => user.UserId = 12);
            string claimedId = "claimedId";

            Model.User userToReturn = new Model.User() {AuthorizationId = claimedId};
            userRepositoryMock.Setup(u => u.GetByAuthenticatedId(It.IsAny<string>())).Returns(userToReturn);

            var services = new UserServices(userRepositoryMock.Object);

            User createdUser = services.GetOrCreateUser(claimedId);

            userRepositoryMock.Verify(u => u.Create(It.IsAny<Model.User>()), Times.Never());
            Assert.Equal(claimedId, createdUser.AuthorizationId);
        }
    }
}