using Microsoft.EntityFrameworkCore;
using Monitory.Core.DBContext;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Monitory.Core.Models;

namespace Monitory.Identity.Helpers
{
    public class AccountHelper
    {
        private static Account account;

        public static async Task<string> GetCurrentUserRoleAsync(string username)
        {
            string role = "";

            using (var context = new MonitoryContext())
            {
                account = await context.Accounts
                    .FirstOrDefaultAsync(m => m.Username == username);
            }

            if(account != null)
            {
                role = account.Role;
            }

            return role;
        }

    }
}
