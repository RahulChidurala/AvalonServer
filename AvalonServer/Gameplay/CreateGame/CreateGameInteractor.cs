using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AvalonServer.Entities;

namespace AvalonServer.Gameplay.CreateGame
{
   
    public interface ICreateGamePresenter
    {
        void Handle(CreateGameMessages.Response response);
    }

    public interface ICreateGameValidator
    {
        bool Validate(CreateGameMessages.Request request);
    }

    public class CreateGameValidator : ICreateGameValidator
    {
        public bool Validate(CreateGameMessages.Request request)
        {
            var valid = true;

            // Check game name
            if(String.IsNullOrEmpty(request.name) == true)
            {
                valid = false;
            }
            
            // request.accessLevel is never false

            return valid;
        }
    }

    public interface ICreateGameInteractor
    {
        IGameGateway Gateway { get; }
        ICreateGameValidator Validator { get; }

        CreateGameMessages.Response Handle(CreateGameMessages.Request request);
    }

    public class CreateGameInteractor : ICreateGameInteractor
    {
        public IGameGateway Gateway { get; }
        public ICreateGameValidator Validator { get; }
        

        public CreateGameInteractor(ICreateGameValidator validator, IGameGateway gateway)
        {
            this.Gateway = gateway;
            this.Validator = validator;
        }

        public CreateGameMessages.Response Handle(CreateGameMessages.Request request)
        {
            var valid = Validator.Validate(request);
            var response = new CreateGameMessages.Response();
            response.success = valid;
            if(valid == false)
            {
                String[] messages = new String[1];
                messages[0] = "Invalid request!";        
                response.message = messages;

            } else
            {
                var gameSettings = new GameSettings()
                {
                    gameAccessLevel = request.accessLevel
                };
                throw new NotImplementedException();
                //var game = new Game(request.name, gameSettings);
                //var gameId = Gateway.CreateGame(game);
                //response.gameId = gameId;
            }
            
            return response;
        }
    }

    public class CreateGameExceptions : Exception
    {
        public CreateGameExceptions() { }
        public CreateGameExceptions(string message): base(message) {  }
    }
}