using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.LoginToAccount
{
    public interface ILoginValidator
    {
        /// <summary>
        /// <exception>Throws LoginValidationException</exception>
        /// </summary>
        void Validate(LoginMessages.Request request);
    }

    public class LoginValidator : ILoginValidator
    {
        public void Validate(LoginMessages.Request request)
        {
            var username = request.Username;
            var password = request.Password;
            
            if(String.IsNullOrEmpty(username) == true)
            {
                throw LoginValidationException.CreateException(LoginValidationExceptions.InvalidUsername);
            }

            if (String.IsNullOrEmpty(password) == true)
            {
                throw LoginValidationException.CreateException(LoginValidationExceptions.InvalidPassword);
            }
        }
    }

    public class LoginValidationException : Exception
    {

        private LoginValidationException() { }
        public LoginValidationException(String message) : base(message) { }

        static public LoginValidationException CreateException(LoginValidationExceptions exception)
        {
            return new LoginValidationException(LoginValidationException.ValidationMessageFor(exception));
        }

        static public string ValidationMessageFor(LoginValidationExceptions exception)
        {
            var message = "Unknown exception!";
            switch (exception)
            {
                case LoginValidationExceptions.InvalidUsername:
                    message = "Invalid username. Username cannot be empty.";
                    break;
                case LoginValidationExceptions.InvalidPassword:
                    message = "Invalid password. Password cannot be empty";
                    break;
            }

            return message;
        }
    }

    public enum LoginValidationExceptions
    {
        InvalidUsername,
        InvalidPassword
    }
}