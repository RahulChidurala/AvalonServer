using AvalonServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.Gameplay.CreatePlayer
{

    public interface ICreatePlayerValidator
    {
        bool Validate(CreatePlayerMessages.Request request);
    }

    public class CreatePlayerValidator : ICreatePlayerValidator
    {
        public bool Validate(CreatePlayerMessages.Request request)
        {
            var valid = true;
            
            if(String.IsNullOrWhiteSpace(request.Username))
            {
                valid = false;
            }

            return valid;
        }
    }

    public interface ICreatePlayerInteractor
    {
        IPlayerGateway Gateway { get; }
        ICreatePlayerValidator Validator { get; }

        CreatePlayerMessages.Response Handle(CreatePlayerMessages.Request request);
    }

    public class CreatePlayerInteractor : ICreatePlayerInteractor
    {
        public IPlayerGateway Gateway { get; }
        public ICreatePlayerValidator Validator { get; }        

        public CreatePlayerInteractor(IPlayerGateway gateway, ICreatePlayerValidator validator)
        {
            this.Gateway = gateway;
            this.Validator = validator;
        }

        public CreatePlayerMessages.Response Handle(CreatePlayerMessages.Request request)
        {
            var valid = Validator.Validate(request);
            var response = new CreatePlayerMessages.Response()
            {
                Success = false,
                Messages = new List<string>()
            };

            if(valid)
            {

                var player = new Player()
                {
                    Username = request.Username
                };

                try
                {
                    Gateway.CreatePlayer(player);
                    response.Success = true;
                }
                catch (PlayerGatewayException ex)
                {
                    response.Success = false;
                    response.Messages.Add(ex.Message);
                    return response;
                }                

            } else
            {
                response.Success = false;
                response.Messages.Add("Username not valid!");
            }
            
            return response;
        }
    }
}