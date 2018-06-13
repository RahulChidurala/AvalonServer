using AvalonServer.Entities;
using AvalonServer.SessionWorkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.CreateGame
{
    using Response = CreateGameMessages.Response;
    public interface ICreateGameInteractor
    {
        CreateGameMessages.Response Handle(CreateGameMessages.Request request);
    }

    public class CreateGameInteractor : ICreateGameInteractor
    {
        ICreateGameValidator Validator;
        ISessionGateway SessionGateway;
        IGameGateway GameGateway;

        public CreateGameInteractor(ICreateGameValidator validator, ISessionGateway sessionGateway, IGameGateway gameGateway)
        {
            Validator = validator;
            SessionGateway = sessionGateway;
            GameGateway = gameGateway;
        }

        public CreateGameMessages.Response Handle(CreateGameMessages.Request request)
        {
            try
            {
                Validator.Validate(request);

            } catch(Exception e)
            {
                return new Response()
                {
                    Success = false,
                    Exception = e
                };
            }

            // Validate session
            try
            {
                var sessionExists = SessionGateway.GetSession(request.Session.Id);
                if(sessionExists == null) // TODO: Gateway should throw
                {
                    throw new Exception("Session does not valid!");
                }

            } catch(Exception e)
            {
                return new Response()
                {
                    Success = false,
                    Exception = e
                };
            }

            // Validate game name is unique
            try
            {
                var gameExists = GameGateway.GetGameBy(request.GameName); // does not throw
                if(gameExists != null)
                {
                    throw new Exception("Game with name already exists!");
                }
            }
            catch (Exception e)
            {
                return new Response()
                {
                    Success = false,
                    Exception = e
                };
            }

            // Save game
            int gameId;
            try
            {
                var game = new Game()
                {
                    GameId = 0, // assigned by gateway
                    Name = request.GameName,
                    AccessLevel = request.AccessLevel
                };

                gameId = GameGateway.CreateGame(game);

            } catch(Exception e)
            {
                return new Response()
                {
                    Success = false,
                    Exception = e
                };
            }

            return new Response()
            {
                Success = true,
                GameId = gameId
            };
        }
    }
}