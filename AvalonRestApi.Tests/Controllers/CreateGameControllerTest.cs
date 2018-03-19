using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AvalonRestApi;
using AvalonRestApi.Controllers;
using AvalonServer.Gameplay.CreateGame;
using Newtonsoft.Json;

namespace AvalonRestApi.Tests.Controllers
{
    [TestClass]
    public class CreateGameControllerTest
    {

        CreateGameController controller;
        
        [TestInitialize]
        public void Setup()
        {
            var gateway = new GameGatewayInMemory();
            var validator = new CreateGameValidator();
            var interactor = new CreateGameInteractor(validator, gateway);

            controller = new CreateGameController(interactor);
        }

        [TestMethod]
        public void Test_CreateGame_PublicAndUniqueName_CreatesGame()
        {
            var request = new CreateGameMessages.Request()
            {
                gameAccessLevel = AvalonServer.Entities.GameSettings.GameAccessLevel.GamePublic,
                name = "UniqueGameRoomName"
            };

            var jsonResponse = controller.Post(request);
            var response = JsonConvert.DeserializeObject<CreateGameMessages.Response>(jsonResponse);

            Assert.IsTrue(response.success);
        }
    }
}
