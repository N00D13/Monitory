using System;
using System.Collections.Generic;
using System.Text;
using Hangfire;
using Hangfire.Dashboard;
using Monitory.Identity.Helpers;
using Monitory.Core.Models;
using Monitory.Core.DBContext;

namespace Monitory.Identity.Filter
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            string username = httpContext.User.Identity.Name;

            if (username == "Alex")
            {
                return httpContext.User.Identity.IsAuthenticated;
            }

            return false; 
        }
    }

}
