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
            throw new NotImplementedException();
            /*var gateway = new PlayerGatewaySpy();
            var validator = new CreatePlayerValidator();
            var interactor = new CreatePlayerInteractor(gateway, validator);
            controller = new CreatePlayerController(interactor);*/
        }

        [TestMethod]
        public void Test_CreatePlayer_UniqueUsername_CreateNewPlayer()
        {            
            var jsonResponse = controller.Post("uniqueUsername");

            var response = JsonConvert.DeserializeObject<CreatePlayerMessages.Response>(jsonResponse);

            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void Test_CreatePlayerController_DuplicateUsername_DoesNotCreatePlayer()
        {            
            var username = "uniqueUsername";
            controller.Post(username);

            var jsonResponse = controller.Post(username);

            var response = JsonConvert.DeserializeObject<CreatePlayerMessages.Response>(jsonResponse);

            Assert.IsFalse(response.Success);
        }
    }
}
