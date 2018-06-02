using AvalonServer.CreateAccount;
using AvalonServer.Entities;
using AvalonServer.Gameplay.CreatePlayer;
using AvalonServer.SessionWorkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.LoginToAccount
{
    public interface ILoginInteractor
    {
        LoginMessages.Response Handle(LoginMessages.Request request);
    }

    public class LoginInteractor : ILoginInteractor
    {
        private ILoginValidator Validator;
        private AccountGateway AccountGateway;
        private IPlayerGateway PlayerGateway;
        private ISessionGateway SessionGateway;
        private ISessionTokenCreator<string> SessionCreator;

        public LoginInteractor(ILoginValidator validator, AccountGateway accountGateway, IPlayerGateway playerGateway, ISessionGateway sessionGateway, ISessionTokenCreator<string> sessionCreator)
        {
            Validator = validator;
            AccountGateway = accountGateway;
            PlayerGateway = playerGateway;
            SessionGateway = sessionGateway;
            SessionCreator = sessionCreator;
        }

        public LoginMessages.Response Handle(LoginMessages.Request request)
        {
            // Validate request
            try
            {
                Validator.Validate(request);

            } catch (LoginValidationException ex)
            {
                var errorResponse = new LoginMessages.Response()
                {
                    Success = false,
                    Exception = ex
                };

                return errorResponse;
            }

            // Authenticate Account
            Account account; 
            try
            {

                account = AccountGateway.GetAccount(request.Username);
                if (account == null)
                {
                    throw LoginException.Create(LoginExceptions.IncorrectCredentials);
                }

                if(account.Password != request.Password)
                {
                    throw LoginException.Create(LoginExceptions.IncorrectCredentials);
                }

            } catch (LoginException ex)
            {
                var errorResponse = new LoginMessages.Response()
                {
                    Success = false,
                    Exception = ex
                };

                return errorResponse;
            }

            var sessionString = SessionCreator.CreateSession();
            var session = new Session
            {
                Id = sessionString,
                PlayerId = account.PlayerId, 
                GameId = null
            };

            try
            {
                SessionGateway.CreateSession(session);
            } catch(Exception e)
            {
                var message = e.Message;
                var errorResponse = new LoginMessages.Response()
                {
                    Success = false,
                    Exception = new Exception("Could not create a session! Error: " + message)
                };

                return errorResponse;
            }

            var response = new LoginMessages.Response()
            {
                Success = true,
                Session = sessionString,
                Exception = null
            };

            return response;
        }
    }

    public class LoginException: Exception {

        private LoginException() { }
        public LoginException(string message): base(message) { }

        static public LoginException Create(LoginExceptions exception)
        {
            LoginException loginException = new LoginException("Unknown error.");
            switch(exception)
            {
                case LoginExceptions.IncorrectCredentials:
                    loginException = new LoginException(GetMessageFor(LoginExceptions.IncorrectCredentials));
                    break;
            }

            return loginException;
        }

        static public string GetMessageFor(LoginExceptions exception)
        {
            var message = "Unknown exception.";
            switch(exception)
            {
                case LoginExceptions.IncorrectCredentials:
                    message = "Wrong username or password.";
                    break;
            }

            return message;
        }
    }

    public enum LoginExceptions
    {
        IncorrectCredentials
    }
}