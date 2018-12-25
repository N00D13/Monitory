using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Monitory.Core.DBContext;

namespace Monitory.Identity.Filter
{
    public class AdministrationAuthorizationFilter
    {
        public async System.Threading.Tasks.Task<bool> AuthorizeAsync(MonitoryContext context, HttpContext httpContext)
        {
            string username = httpContext.User.Identity.Name;
            var user = await context.Accounts.FirstOrDefaultAsync(u => u.Username == username);

            if(user.Role == "Administrator")
            {
                return true;
            }

            return false;
        }
    }
}
