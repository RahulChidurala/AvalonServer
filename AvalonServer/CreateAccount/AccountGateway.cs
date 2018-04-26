using AvalonServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.CreateAccount
{
    public interface IAccountGateway
    {
        /// <summary>
        /// Creates an account if there is no duplicate username.
        /// </summary>
        /// <exception cref="">Throws AccountGatewayException</exception>
        /// <param name="account"></param>
        int CreateAccount(Account account);
        Account GetAccount(string username);

        /// <summary>
        /// Updates game.
        /// </summary>
        /// <exception>Throws GameGatewayException</exception>
        /// <param name="account"></param>
        void UpdateAccount(Account account);
        void DeleteAccount(int accountId);
    }
    
    public class AccountGatewayException : Exception
    {
        public AccountGatewayException() { }
        public AccountGatewayException(string message) : base(message) { }

        public static string GetMessage(AccountGatewayExceptionMessages exception, Account account, String extraMessage = null)
        {
            String exceptionMessage = "";
            switch(exception)
            {
                case AccountGatewayExceptionMessages.AccountDoesNotExist:
                    exceptionMessage = "Account does not exist for username: " + account.Username;
                    break;                
                case AccountGatewayExceptionMessages.DeleteAccountFailed:
                    exceptionMessage = "Could not delete account with username " + account.Username;
                    break;
                case AccountGatewayExceptionMessages.UpdateAccountFailed:
                    exceptionMessage = "Could not update account with username " + account.Username;
                    break;
            }

            if(String.IsNullOrWhiteSpace(extraMessage) == false)
            {
                exceptionMessage = exceptionMessage + " Error: " + extraMessage;
            }

            return exceptionMessage;
        }
    }

    public enum AccountGatewayExceptionMessages {
        
        CreateAccountFailed,
        DeleteAccountFailed,
        AccountDoesNotExist,
        UpdateAccountFailed
    }
}