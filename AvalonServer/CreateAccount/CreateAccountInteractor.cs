using AvalonServer.Entities;
using AvalonServer.Gameplay.CreatePlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.CreateAccount
{
    public interface ICreateAccountInteractor
    {
        CreateAccountMessages.Response Handle(CreateAccountMessages.Request request);
    }

    public class CreateAccountInteractor : ICreateAccountInteractor
    {
        private ICreateAccountValidator Validator;
        private IAccountGateway AccountGateway;
        private IPlayerGateway PlayerGateway;

        public CreateAccountInteractor(ICreateAccountValidator validator, IAccountGateway accountGateway, IPlayerGateway playerGateway)
        {
            Validator = validator;
            AccountGateway = accountGateway;
            PlayerGateway = playerGateway;
        }

        public CreateAccountMessages.Response Handle(CreateAccountMessages.Request request)
        {

            // Validate request
            try
            {
                Validator.Validate(request);
            }
            catch (CreateAccountValidationException ex)
            {
                var errorResponse = new CreateAccountMessages.Response()
                {
                    Success = false,
                    Exception = ex
                };
                return errorResponse;
            }

            // Check for duplicate usernames
            var accountExists = AccountGateway.GetAccount(request.Username);
            if(accountExists != null)
            {
                var errorResponse = new CreateAccountMessages.Response()
                {
                    Success = false,
                    Exception = new CreateAccountException(CreateAccountException.GetMessage(CreateAccountExceptions.UsernameAlreadyExists, request))
                };

                return errorResponse;
            }

            // Store in Gateway
            int accountId;
            try
            {
                var validatedAccount = new Account()
                {
                    Username = request.Username,
                    Password = request.Password
                };

                accountId = AccountGateway.CreateAccount(validatedAccount);

            }
            catch (AccountGatewayException ex)
            {
                var errorResponse = new CreateAccountMessages.Response()
                {
                    Success = false,
                    Exception = ex
                };
                return errorResponse;
            }

            // Create associated Player
            try
            {
                var player = new Player()
                {
                    AccountId = accountId,
                    DisplayName = request.Username,                    
                };

                PlayerGateway.CreatePlayer(player);

            } catch(PlayerGatewayException ex)
            {
                var errorResponse = new CreateAccountMessages.Response()
                {
                    Success = false,
                    Exception = ex
                };
                return errorResponse;
            }

            var response = new CreateAccountMessages.Response()
            {
                Success = true,
                Exception = null
            };

            return response;
        }
    }

    public class CreateAccountException : Exception
    {
        public CreateAccountException() {  }
        public CreateAccountException(string message): base(message) { }

        public static string GetMessage(CreateAccountExceptions exception, CreateAccountMessages.Request request)
        {
            String exceptionMessage = "Unknown error.";
            switch(exception)
            {
                case CreateAccountExceptions.UsernameAlreadyExists:
                    exceptionMessage = "Username " + request.Username + " already exists! Please choose another username!";
                    break;
            }

            return exceptionMessage;
        }
    }

    public enum CreateAccountExceptions
    {
        UsernameAlreadyExists
    }
}