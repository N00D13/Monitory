using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Monitory.Core.DBContext;
using Monitory.Core.Models;
using Monitory.Tasks.Checks;
using Monitory.Tasks.Filters;

namespace Monitory.Controllers
{
    [Authorize]
    public class WebCheckController : Controller
    {
        #region Properties
        private readonly MonitoryContext _context;

        public WebCheckController(MonitoryContext context)
        {
            _context = context;
        }

        #endregion

        #region Getters

        // GET: WebCheck
        public async Task<IActionResult> Index()
        {
            var monitoryContext = _context.WebChecks.Include(w => w.Account);

            var checkList = await monitoryContext.ToListAsync();

            // User Filter
            var userAccount = GetUserAccountAsync();
            if(userAccount != null)
            {
                checkList = checkList.Where(c => c.Account.Username == userAccount.Result.Username).ToList();
            }

            return View(checkList);
        }

        // GET: WebCheck/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webCheck = await _context.WebChecks
                .Include(w => w.Account)
                .FirstOrDefaultAsync(m => m.WebCheckID == id);
            if (webCheck == null)
            {
                return NotFound();
            }

            return View(webCheck);
        }

        // GET: WebCheck/Create
        public IActionResult Create()
        {
            ViewData["AccountID"] = new SelectList(_context.Accounts, "AccountID", "Password");
            return View();
        }

        // GET: WebCheck/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webCheck = await _context.WebChecks.FindAsync(id);
            if (webCheck == null)
            {
                return NotFound();
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "AccountID", "Password", webCheck.AccountID);
            return View(webCheck);
        }

        // GET: WebCheck/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webCheck = await _context.WebChecks
                .Include(w => w.Account)
                .FirstOrDefaultAsync(m => m.WebCheckID == id);
            if (webCheck == null)
            {
                return NotFound();
            }

            return View(webCheck);
        }

        #endregion

        #region Setters

        // POST: WebCheck/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WebCheckID,Name,Domain,Delay,CreateDate,AccountID")] WebCheck webCheck)
        {
            if (ModelState.IsValid)
            {
                var userAccount = GetUserAccountAsync().Result;
                webCheck.WebCheckID = Guid.NewGuid();
                webCheck.CreateDate = DateTime.Now;
                webCheck.AccountID = userAccount.AccountID;

                _context.Add(webCheck);
                await _context.SaveChangesAsync();

                WebCheckHelper.CreateRecurringEvent(webCheck);

                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "AccountID", "Password", webCheck.AccountID);
            return View(webCheck);
        }

        // POST: WebCheck/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WebCheckID,Name,Domain,Delay,CreateDate,AccountID")] WebCheck webCheck)
        {
            if (id != webCheck.WebCheckID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Set DateTime new
                    webCheck.CreateDate = DateTime.Now;

                    _context.Update(webCheck);
                    await _context.SaveChangesAsync();

                    WebCheckHelper.UpdateRecurringEvent(webCheck);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebCheckExists(webCheck.WebCheckID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "AccountID", "Password", webCheck.AccountID);
            return View(webCheck);
        }

        // POST: WebCheck/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var webCheck = await _context.WebChecks.FindAsync(id);
            _context.WebChecks.Remove(webCheck);
            await _context.SaveChangesAsync();

            WebCheckHelper.DeleteRecurringEvent(webCheck);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Helpers

        private bool WebCheckExists(Guid id)
        {
            return _context.WebChecks.Any(e => e.WebCheckID == id);
        }

        private async Task<Account> GetUserAccountAsync()
        {
            var identityUser = HttpContext.User.Identity;

            Account usr = await _context.Accounts.FirstOrDefaultAsync(a => a.Username == identityUser.Name);

            if(usr != null)
            {
                return usr;
            }
            return null;
        }

        #endregion
    }
}
