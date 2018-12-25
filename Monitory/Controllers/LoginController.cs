using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monitory.Core.DBContext;
using Monitory.Identity.Helpers;
using Monitory.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Monitory.Controllers
{
    public class LoginController : Controller
    {
        #region Properties
        private readonly MonitoryContext _context;

        public LoginController(MonitoryContext context)
        {
            _context = context;
        }
        #endregion

        #region Getter
        // GET: Login/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Setter
        // POST: Login/Index/
        [HttpPost]
        public async Task<IActionResult> Index(Account account)
        {
            if (await LoginUserAsync(account.Username, account.Password))
            {
                string userRole = await GetUserRoleAsync(account.Username);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, account.Username),
                    new Claim(ClaimTypes.Role, userRole)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(principal);

                //Just redirect to our index after logging in. 
                return Redirect("/");
            }
            return View();
        }
        #endregion

        #region Helpers

        private async Task<bool> LoginUserAsync(string username, string password)
        {
            var hash = Security.GenerateSaltedHash(password);
            string hashedString = Convert.ToBase64String(hash);

            var acnt = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Username == username);
            if (acnt == null)
            {
                return false;
            }

            if(acnt.Password == hashedString)
            {
                return true;
            }


            return false;
        }

        private async Task<string> GetUserRoleAsync(string username)
        {
            var acnt = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Username == username);
            if (acnt == null)
            {
                return "";
            }

            return acnt.Role;
        }

        #endregion

    }
}
