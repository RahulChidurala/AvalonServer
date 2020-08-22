using Microsoft.VisualStudio.TestTools.UnitTesting;
using AvalonRestApi.Controllers;
using AvalonServer.CreateGame;
using Newtonsoft.Json;
using AvalonServer.Tests.Session;

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
            var session = new SessionGatewaySpy();
            var interactor = new CreateGameInteractor(validator, session, gateway);

            controller = new CreateGameController(interactor);
        }

        [TestMethod]
        public void Test_CreateGame_PublicAndUniqueName_CreatesGame()
        {
            var request = new CreateGameMessages.Request()
            {
                AccessLevel = AvalonServer.Entities.GameSettings.GameAccessLevel.Public,
                GameName = "UniqueGameRoomName"
            };

            var jsonResponse = controller.Post("Public", "Game1");
            var response = JsonConvert.DeserializeObject<CreateGameMessages.Response>(jsonResponse);

            Assert.IsTrue(response.Success);
        }
    }
}
