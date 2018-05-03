using AvalonServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonServer.Tests
{
    public static class EntityIdExtension
    {
        public static int GetId(this IAccount account)
        {
            return account.Id;
        }
    }
    public abstract class GatewaySpy<Value>
    {        
        public Dictionary<int, Value> repo = new Dictionary<int, Value>();
        public int maxId = 0;

        #region Implementation of Gateway
        public int Create(Value value)
        {
            maxId++;
            repo.Add(maxId, value);
            return maxId;
        }

        public void Delete(int key)
        {
            repo.Remove(key);
        }

        public Value Get(int key)
        {
            var value = GetValueInternal(key);

            return value;
        }        
        #endregion

        #region Helper functions
        private Value GetValueInternal(int key)
        {           
            Value value = repo[key];
            return value;
        }
        #endregion
    }
}
