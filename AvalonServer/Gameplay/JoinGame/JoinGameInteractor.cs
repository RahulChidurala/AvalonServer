using AvalonServer.Gameplay.CreateGame;
using AvalonServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AvalonServer.Gameplay.CreatePlayer;

namespace AvalonServer.Gameplay.JoinGame
{

    public interface IJoinGameInteractor
    {
        JoinGameMessages.Response Handle(JoinGameMessages.Request request);
    }

    public interface IJoinGameValidator
    {
        bool Validate(JoinGameMessages.Request request);
    }

    public class JoinGameValidator : IJoinGameValidator
    {
        public bool Validate(JoinGameMessages.Request request)
        {
            var valid = true;
            if (request.gameId < 0)
            {
                valid = valid && false;
            }

            return valid;
        }
    }

    public interface IJoinGamePresenter
    {
        JoinGameMessages.ViewModel Handle(JoinGameMessages.Response response);
    }

    public class JoinGameInteractor : IJoinGameInteractor
    {

        private IJoinGameValidator Validator { get; }
        private IGameGateway GameGateway { get; }
        private IPlayerGateway PlayerGateway { get; }
        
        public JoinGameInteractor(IJoinGameValidator validator, IGameGateway gameGateway, IPlayerGateway playerGateway)
        {
            this.Validator = validator;
            this.GameGateway = gameGateway;
            this.PlayerGateway = playerGateway;
        }

        // TODO
        public JoinGameMessages.Response Handle(JoinGameMessages.Request request)
        {

            throw new NotImplementedException();
            /*JoinGameMessages.Response response = new JoinGameMessages.Response();

            var retrievedGame = GameGateway.GetGame(request.gameId);
            if (retrievedGame == null)
            {
                response.JoinedGame = false;
                response.ErrorMessage = "Game with id " + request.gameId + " does not exist!";

            } else
            {
                var playerExists = PlayerGateway.GetPlayer(request.playerUsername);
                if (playerExists != null)
                {
                    //// retrievedGame
                    //if(retrievedGame.Players == null)
                    //{
                    //    retrievedGame.Players = new List<Player>();
                    //}

                    //retrievedGame.Players.Add(playerExists);
                    //playerExists.GameId = request.gameId;
                    throw new NotImplementedException();

                    // DB transactions
                    GameGateway.UpdateGame(retrievedGame);
                    PlayerGateway.UpdatePlayer(playerExists);

                    response.JoinedGame = true;

                } else
                {
                    response.ErrorMessage = "Player with the username '" + request.playerUsername + "' does not exist!";
                    response.JoinedGame = false;
                }
            }            

            return response;*/
        }
    }
}