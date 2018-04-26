using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.CreateAccount
{
    public interface ICreateAccountValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception>Throws CreateAccountValidation exception.</exception>
        /// <param name="request"></param>
        void Validate(CreateAccountMessages.Request request);
    }    

    public class CreateAccountValidator : ICreateAccountValidator
    {
        public static int usernameRequiredLength = 6;
        public static int passwordRequiredLength = 1;

        public void Validate(CreateAccountMessages.Request request)
        {
            var username = request.Username;
            var password = request.Password;

            if(String.IsNullOrWhiteSpace(username) || username.Length < usernameRequiredLength)
            {
                throw CreateAccountValidationException.CreateException(CreateAccountValidationExceptions.InvalidUsername);
            }            

            if(String.IsNullOrWhiteSpace(password) || password.Length < passwordRequiredLength)
            {
                throw CreateAccountValidationException.CreateException(CreateAccountValidationExceptions.InvalidPassword);
            }
        }
    }

    public class CreateAccountValidationException: Exception
    {
        
        private CreateAccountValidationException() { }
        public CreateAccountValidationException(String message): base(message) { }
        
        static public CreateAccountValidationException CreateException(CreateAccountValidationExceptions exception)
        {
            return new CreateAccountValidationException(CreateAccountValidationException.ValidationMessageFor(exception));
        }

        static public string ValidationMessageFor(CreateAccountValidationExceptions exception)
        {
            var message = "Unknown exception!";
            switch (exception)
            {
                case CreateAccountValidationExceptions.InvalidUsername:
                    message = "Invalid username. Username must be at least " + CreateAccountValidator.usernameRequiredLength + " characters.";
                    break;
                case CreateAccountValidationExceptions.InvalidPassword:
                    message = "Invalid password. Password must contain at least " + CreateAccountValidator.passwordRequiredLength + " character.";
                    break;
            }

            return message;
        }
    }

    public enum CreateAccountValidationExceptions
    {
        InvalidUsername,
        InvalidPassword
    }    
}