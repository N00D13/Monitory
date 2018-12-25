using System;
using System.Collections.Generic;
using System.Text;
using Hangfire;
using Hangfire.Dashboard;
using Monitory.Identity.Helpers;
using Monitory.Core.Models;

namespace Monitory.Identity.Filter
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            string username = httpContext.User.Identity.Name;
            var userRole = AccountHelper.GetCurrentUserRoleAsync(username);


            if (userRole.ToString() == "Administrator")
            {
                return httpContext.User.Identity.IsAuthenticated;
            }

            return httpContext.User.Identity.IsAuthenticated;   
        }
    }

}
