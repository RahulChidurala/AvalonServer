using AvalonServer.Gameplay.CreateGame;
using AvalonServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        private IGameGateway Gateway { get; }

        public JoinGameInteractor(IJoinGameValidator validator, IGameGateway gateway)
        {
            this.Validator = validator;
            this.Gateway = gateway;
        }

        public JoinGameMessages.Response Handle(JoinGameMessages.Request request)
        {
            JoinGameMessages.Response response = new JoinGameMessages.Response();

            var retrievedGame = Gateway.GetGame(request.gameId);
            if (retrievedGame == null)
            {
                response.joinedGame = false;

            } else
            {
                retrievedGame.Players.Add(new Player());
                response.joinedGame = true;
            }            

            return response;
        }
    }
}