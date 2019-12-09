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
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Handlers;
using Moq;
using Xunit;

namespace MileageStats.Services.Tests
{
    public class WhenGettingFillups
    {
        private readonly Mock<IUserServices> _userServicesMock;
        private readonly Mock<IVehicleRepository> _vehicleRepo;
        private readonly Mock<IFillupRepository> _fillupRepositoryMock;
        private const int DefaultUserId = 99;
        private const int DefaultVehicleId = 88;
        private const int DefaultFillupId = 88;

        public WhenGettingFillups()
        {
            _userServicesMock = new Mock<IUserServices>();
            _vehicleRepo = new Mock<IVehicleRepository>();
            _fillupRepositoryMock = new Mock<IFillupRepository>();
        }

        [Fact]
        public void WhenGettingFillupsAndRepositoryThrows_ThenWrapsException()
        {
            _fillupRepositoryMock
                .Setup(f => f.GetFillups(DefaultVehicleId))
                .Throws<InvalidOperationException>();

            var handler = new GetFillupsForVehicle(_fillupRepositoryMock.Object);

            var ex = Assert.Throws<BusinessServicesException>(() => handler.Execute(DefaultVehicleId));
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

        [Fact]
        public void WhenGettingFillup_ThenDelegatesToFillupRepository()
        {
            _fillupRepositoryMock
                .Setup(x => x.GetFillup(DefaultFillupId))
                .Verifiable();

            var handler = new GetFillupById(_fillupRepositoryMock.Object);
           handler.Execute(DefaultFillupId);

            _fillupRepositoryMock
                .Verify(r => r.GetFillup(DefaultFillupId), Times.Once());
        }

        [Fact]
        public void WhenGettingFillupAndErrorOccurs_ThenThrows()
        {
            _fillupRepositoryMock
                .Setup(f => f.GetFillup(DefaultFillupId))
                .Throws<InvalidOperationException>();

            var handler = new GetFillupById(_fillupRepositoryMock.Object);

            var ex = Assert.Throws<BusinessServicesException>(() => handler.Execute(DefaultFillupId));
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }
    }
}