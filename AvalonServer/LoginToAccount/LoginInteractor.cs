using AvalonServer.CreateAccount;
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
        private IAccountGateway AccountGateway;

        public LoginInteractor(ILoginValidator validator, IAccountGateway accountGateway)
        {
            Validator = validator;
            AccountGateway = accountGateway;
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

            // Authenticate IAccount
            try
            {

                var account = AccountGateway.GetAccount(request.Username);
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

            var response = new LoginMessages.Response()
            {
                Success = true,
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