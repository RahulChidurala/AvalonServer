using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AvalonServer.Gameplay.JoinGame;
using AvalonServer.Entities;
using AvalonServer.CreateGame;
using AvalonServer.Gameplay.CreatePlayer;

namespace AvalonServer.Tests.JoinGame
{
    [TestClass]
    public class JoinGameInteractorTests
    {
        IJoinGameInteractor sut;
        IJoinGameValidator validator = new JoinGameValidator();
        IGameGateway gameGateway;
        IPlayerGateway playerGateway;

        [TestInitialize]
        public void Setup()
        {
            gameGateway = new GameGatewayInMemory();
            sut = MakeSut();
        }

        private int createFakeGame()
        {
            var gameSettings = new GameSettings()
            {
                gameAccessLevel = GameSettings.GameAccessLevel.Public
            };
            //var game = new Game("GameRoom1", gameSettings);
            //var gameId = gameGateway.CreateGame(game);

            //return gameId;
            throw new NotImplementedException();
        }

        private IJoinGameInteractor MakeSut()
        {
            return new JoinGameInteractor(validator, gameGateway, playerGateway);
        }

        [TestMethod]
        public void Test_JoinGameRoom_WhenJoinExistingGame_ShouldJoinGame()
        {
            var fakeGameId = createFakeGame();
            var request = new JoinGameMessages.Request()
            {
                gameId = fakeGameId
            };

            var response = sut.Handle(request);

            Assert.IsTrue(response.JoinedGame);
        }

        [TestMethod]
        public void Test_JoinGameRoom_WhenJoinNonExistingGame_ShouldNotJoinGame()
        {            
            var request = new JoinGameMessages.Request()
            {
                gameId = 0
            };

            var response = sut.Handle(request);

            Assert.IsFalse(response.JoinedGame);
        }

        [TestMethod]
        public void Test_JoinGameRoom_WhenPlayerJoinsExistingGame_ShouldAddPlayerToGame()
        {
            var fakeGameId = createFakeGame();
            var request = new JoinGameMessages.Request()
            {
                gameId = fakeGameId
            };
            var response = sut.Handle(request);

            var game = gameGateway.GetGame(fakeGameId);

            throw new NotImplementedException();
            //Assert.IsTrue(game.Players.Count == 1);
        }
    }
}
