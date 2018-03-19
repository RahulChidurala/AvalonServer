using System;
using AvalonRestApi.Controllers;
using AvalonServer.Gameplay.CreatePlayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace AvalonRestApi.Tests.Controllers
{
    [TestClass]
    public class CreatePlayerControllerTests
    {
        private CreatePlayerController controller;
        
        [TestInitialize]
        public void Setup()
        {
            var gateway = new PlayerGatewayInMemory();
            var validator = new CreatePlayerValidator();
            var interactor = new CreatePlayerInteractor(gateway, validator);
            controller = new CreatePlayerController(interactor);
        }

        [TestMethod]
        public void Test_CreatePlayer_UniqueUsername_CreateNewPlayer()
        {
            var request = new CreatePlayerMessages.Request()
            {
                Username = "uniqueUsername"
            };
            var jsonResponse = controller.Post(request);

            var response = JsonConvert.DeserializeObject<CreatePlayerMessages.Response>(jsonResponse);

            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void Test_CreatePlayer_DuplicateUsername_DoesNotCreatePlayer()
        {
            var request = new CreatePlayerMessages.Request()
            {
                Username = "uniqueUsername"
            };
            controller.Post(request);

            var jsonResponse = controller.Post(request);

            var response = JsonConvert.DeserializeObject<CreatePlayerMessages.Response>(jsonResponse);

            Assert.IsFalse(response.Success);
        }
    }
}
