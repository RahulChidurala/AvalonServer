using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AvalonServer.CreateGame;
using AvalonServer.Entities;
using System.Collections.Generic;
using AvalonServer.SessionWorkers;
using AvalonServer.Tests.Session;

namespace AvalonServer.Tests.CreateGame
{
    [TestClass]
    public class CreateGameRoomTests
    {
        ICreateGameInteractor sut;
        ICreateGameValidator validator = new CreateGameValidator();
        SessionGatewaySpy SessionGateway = new SessionGatewaySpy(); 
        GameGatewaySpy GameGateway = new GameGatewaySpy();

        Entities.Session storedSession;

        [TestInitialize]
        public void Setup()
        {
            sut = MakeSut();
            SessionGateway = new SessionGatewaySpy();

            // Insert fake session
            var session = new Entities.Session()
            {
                GameId = 1,
                Id = "", // Assigned by gateway
                PlayerId = 1
            };

            SessionGateway.CreateSession(session);
            storedSession = session;
        }

        private ICreateGameInteractor MakeSut()
        {
            return new CreateGameInteractor(validator, SessionGateway, GameGateway);
        }

        // TODO: Make validator throw expected exceptions
        // TODO: Refactor theses tests
        // CreateGameRoom Request Validator tests
        [TestMethod]
        public void Test_CreateGameRoom_WhenRequestIsValid_ShouldRespondTrue()
        {

            var request = new CreateGameMessages.Request()
            {
                Session = storedSession,
                AccessLevel = GameSettings.GameAccessLevel.FriendsOnly,
                GameName = "GameRoom1"
            };
            var response = sut.Handle(request);

            Assert.IsTrue(response.Success, "Should have succeeded. Exception: " + response.Exception.Message);
        }

        [TestMethod]
        public void Test_CreateGameRoom_WhenWithEmptyNameAndPublic_ShouldRespondFalse()
        {
            var request = new CreateGameMessages.Request()
            {
                AccessLevel = Entities.GameSettings.GameAccessLevel.Public,
                GameName = String.Empty
            };

            var response = sut.Handle(request);

            Assert.IsFalse(response.Success);
        }

        // CreateGameRoom Gateway tests       
        [TestMethod]
        public void Test_CreateGameRoom_WhenValidRequest_ShouldSaveGameRoom()
        {
            var request = new CreateGameMessages.Request()
            {
                AccessLevel = Entities.GameSettings.GameAccessLevel.Public,
                GameName = "GameRoom1"
            };

            var response = sut.Handle(request);
            Assert.IsTrue(response.Success, "Should have succeeded. Exception: " + response.Exception.Message);
            Assert.IsNotNull(response.GameId, "GameId is not null");
            Assert.IsTrue(GameGateway.GetGame((int) response.GameId) != null);
        }

        [TestMethod]
        public void Test_CreateGameRoom_WhenInvalidRequest_ShouldNotSaveGameRoom()
        {
            var request = new CreateGameMessages.Request()
            {
                AccessLevel = Entities.GameSettings.GameAccessLevel.Public,
                GameName = ""
            };
            var response = sut.Handle(request);

            Assert.IsTrue(GameGateway.games.Count == 0);
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

                games.Remove(game.GameId);
            }

            public Game[] GetAllGames()
            {
                throw new NotImplementedException();
            }

            public void UpdateGame(Game game)
            {
                throw new NotImplementedException();
            }

            public Game GetGameBy(string name)
            {
                throw new NotImplementedException();
            }
        }
    }
}
