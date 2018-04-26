using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AvalonServer.Gameplay.CreateGame;
using AvalonServer.Entities;
using System.Collections.Generic;

namespace AvalonServer.Tests.CreateGame
{
    [TestClass]
    public class CreateGameRoomTests
    {
        ICreateGameInteractor sut;
        CreateGameValidator validator = new CreateGameValidator();
        GameGatewaySpy gateway = new GameGatewaySpy();

        [TestInitialize]
        public void Setup()
        {
            sut = MakeSut();
        }

        private ICreateGameInteractor MakeSut()
        {
            return new CreateGameInteractor(validator, gateway);
        }

        // TODO: Make validator throw expected exceptions
        // TODO: Refactor theses tests
        // CreateGameRoom Request Validator tests
        [TestMethod]
        public void Test_CreateGameRoom_WhenRequestIsValid_ShouldRespondTrue()
        {

            var request = new CreateGameMessages.Request()
            {
                accessLevel = GameSettings.GameAccessLevel.FriendsOnly,
                name = "GameRoom1"
            };
            var response = sut.Handle(request);

            Assert.IsTrue(response.success);
        }

        [TestMethod]
        public void Test_CreateGameRoom_WhenWithEmptyNameAndPublic_ShouldRespondFalse()
        {
            var request = new CreateGameMessages.Request()
            {
                accessLevel = Entities.GameSettings.GameAccessLevel.Public,
                name = String.Empty
            };

            var response = sut.Handle(request);

            Assert.IsFalse(response.success);
        }

        // CreateGameRoom Gateway tests       
        [TestMethod]
        public void Test_CreateGameRoom_WhenValidRequest_ShouldSaveGameRoom()
        {
            var request = new CreateGameMessages.Request()
            {
                accessLevel = Entities.GameSettings.GameAccessLevel.Public,
                name = "GameRoom1"
            };

            var response = sut.Handle(request);
            
            Assert.IsTrue(gateway.GetGame(response.gameId) != null);
        }

        [TestMethod]
        public void Test_CreateGameRoom_WhenInvalidRequest_ShouldNotSaveGameRoom()
        {
            var request = new CreateGameMessages.Request()
            {
                accessLevel = Entities.GameSettings.GameAccessLevel.Public,
                name = ""
            };
            var response = sut.Handle(request);

            Assert.IsTrue(gateway.games.Count == 0);
        }

        // TODO: Do duplicate GameRoom tests
        // CreateGameRoom Interactor tests
        /*
         [TestMethod]
        public void Test_CreateGameRoom_WhenDuplicateGameRoom_ShouldRespondFalseAndNotSaveGameRoom()
        {
            var request = new CreateGameMessages.Request()
            {
                accessLevel = Entities.GameSettings.GameAccessLevel.Public,
                name = "GameRoom1"
            };
            var response1 = sut.Handle(request);

            var response2 = sut.Handle(request);

            Assert.IsFalse(response2.success);
            Assert.IsTrue(gameGateway.games.Count == 1);
        } */

        // Helpers        
        private class GameGatewaySpy : IGameGateway
        {
            public Dictionary<int, Game> games = new Dictionary<int, Game>();
            private int highestId = 0;

            public int CreateGame(Game game)
            {
                var gameId = highestId;
                games[gameId] = game;
                highestId++;
                return gameId;
            }

            public Game GetGame(int gameId)
            {
                return games[gameId];
            }

            public void DeleteGame(Game game)
            {
                throw new NotImplementedException();
            }

            public Game[] GetAllGames()
            {
                throw new NotImplementedException();
            }

            public void UpdateGame(Game game)
            {
                throw new NotImplementedException();
            }
        }
    }
}
