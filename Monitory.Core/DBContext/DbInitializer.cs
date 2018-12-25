using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Monitory.Core.Models;

namespace Monitory.Core.DBContext
{
    public class DbInitializer
    {
        public static void Initialize(MonitoryContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Accounts.Any())
            {
                return;   // DB has been seeded
            }

            var accounts = new Account[]
            {
            new Account{Username="Alex",Password="YxZmgn/IAPKDbBTX6HDhBdmdkBJ0oRKU8fQs7F7VJz4="},
            };
            foreach (Account s in accounts)
            {
                context.Accounts.Add(s);
            }
            context.SaveChanges();

        }
    }
}
