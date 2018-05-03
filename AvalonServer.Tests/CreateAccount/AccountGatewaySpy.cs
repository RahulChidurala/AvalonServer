using AvalonServer.CreateAccount;
using AvalonServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonServer.Tests.CreateAccount
{
    public class AccountGatewaySpy : IAccountGateway
    {
        public Dictionary<int, IAccount> repo = new Dictionary<int, IAccount>();
        public int maxId = 0;

        #region Implementation of AccountGateway
        public int CreateAccount(IAccount account)
        {
            maxId++;
            account.Id = maxId;
            repo.Add(maxId, account);
            return maxId;
        }

        public void DeleteAccount(int accountId)
        {
            repo.Remove(accountId);
        }

        public IAccount GetAccount(string username)
        {
            var account = GetAccountInternal(username);            

            return account;
        }

        public void UpdateAccount(IAccount account)
        {
            var accountExists = GetAccountInternal(account.Username);

            if(accountExists == null)
            {
                throw new AccountGatewayException(AccountGatewayException.GetMessage(AccountGatewayExceptionMessages.AccountDoesNotExist, account));
            }

            var accountId = accountExists.Id;
            repo[accountId] = account;
        }
        #endregion

        #region Helper functions
        private IAccount GetAccountInternal(string username)
        {

            IAccount account = null;

            foreach (KeyValuePair<int, IAccount> entry in repo)
            {

                if (entry.Value.Username == username)
                {
                    account = entry.Value;
                    break;
                }
            }

            return account;
        }
        #endregion
    }
}
