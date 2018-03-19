using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AvalonServer.Gameplay.JoinGame;
using AvalonServer.Entities;
using System.Collections.Generic;
using AvalonServer.Gameplay.CreatePlayer;

namespace AvalonServer.Tests.CreatePlayer
{
    [TestClass]
    class CreatePlayerInteractorTests
    {
        ICreatePlayerInteractor interactor;
        IPlayerGateway gateway;
        ICreatePlayerValidator validator;

        [TestInitialize]
        public void Setup()
        {
            gateway = new PlayerGatewayInMemory();
            validator = new CreatePlayerValidator();
            interactor = new CreatePlayerInteractor(gateway, validator);
        }

        [TestMethod]
        public void Test_CreatePlayer_InvalidUsername_RespondsFail()
        {
            var request = new CreatePlayerMessages.Request()
            {
                Username = ""
            };

            var response = interactor.Handle(request);
            Assert.IsFalse(response.Success);

            request.Username = String.Empty;
            var response2 = interactor.Handle(request);
            Assert.IsFalse(response2.Success);
        }

        [TestMethod]
        public void Test_CreatePlayer_UniqueUsername_RespondsSuccess()
        {
            var request = new CreatePlayerMessages.Request()
            {
                Username = "uniqueUsername"
            };

            var response = interactor.Handle(request);

            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void Test_CreatePlayer_DuplicateUsername_RespondsFail()
        {
            var request = new CreatePlayerMessages.Request()
            {
                Username = "uniqueUsername"
            };

            interactor.Handle(request);
            var response = interactor.Handle(request);

            Assert.IsFalse(response.Success);
        }
    }
}
