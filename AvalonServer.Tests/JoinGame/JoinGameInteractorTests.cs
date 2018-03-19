using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AvalonServer.Gameplay.JoinGame;
using AvalonServer.Entities;
using System.Collections.Generic;
using AvalonServer.Gameplay.CreateGame;

namespace AvalonServer.Tests.JoinGame
{
    [TestClass]
    public class JoinGameInteractorTests
    {
        IJoinGameInteractor sut;
        IJoinGameValidator validator = new JoinGameValidator();
        IGameGateway gateway;

        [TestInitialize]
        public void Setup()
        {
            gateway = new GameGatewayInMemory();
            sut = MakeSut();
        }

        private int createFakeGame()
        {
            var gameSettings = new GameSettings()
            {
                gameAccessLevel = GameSettings.GameAccessLevel.GamePublic
            };
            var game = new Game("GameRoom1", gameSettings);
            var gameId = gateway.CreateGame(game);

            return gameId;
        }

        private IJoinGameInteractor MakeSut()
        {
            return new JoinGameInteractor(validator, gateway);
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

            Assert.IsTrue(response.joinedGame);
        }

        [TestMethod]
        public void Test_JoinGameRoom_WhenJoinNonExistingGame_ShouldNotJoinGame()
        {            
            var request = new JoinGameMessages.Request()
            {
                gameId = 0
            };

            var response = sut.Handle(request);

            Assert.IsFalse(response.joinedGame);
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

            var game = gateway.GetGame(fakeGameId);

            Assert.IsTrue(game.Players.Count == 1);
        }
    }
}
