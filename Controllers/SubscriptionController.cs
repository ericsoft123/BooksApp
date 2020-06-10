using Books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly BookDbContext _db;
        private readonly SignInManager<ApplicationUser> signManager;

        public SubscriptionController(BookDbContext db, SignInManager<ApplicationUser> SignManager)
        {
            _db = db;
            signManager = SignManager;



        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> subscribe([Bind("email,planId,name,text,price")] Subscription subscriptions)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    subscriptions.email = User.Identity.Name;
                    var searchData = _db.Subscriptions.FirstOrDefault(u => u.planId == subscriptions.planId && u.email == subscriptions.email);
                    if (searchData == null)
                    {
                        //return Ok(new { data = "return null" });
                        subscriptions.Created_at = DateTime.Today;
                        subscriptions.End_at = DateTime.Today.AddMonths(1);
                        _db.Subscriptions.Add(subscriptions);
                        await _db.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {

                        return RedirectToAction("error", "subscription");
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return View();

        }
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> unsubscribe([Bind("email,planId,name,text,price")] Subscription subscriptions)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    subscriptions.email = User.Identity.Name;
                    var searchData = _db.Subscriptions.FirstOrDefault(u => u.planId == subscriptions.planId && u.email == subscriptions.email);
                    if (searchData == null)
                    {
                        //return Ok(new { data = "return null" });
                        // return Ok(new { data = searchData });
                     
                        return RedirectToAction("index", "Home");

                    }
                    else
                    {


                        _db.Subscriptions.Remove(searchData);

                        await _db.SaveChangesAsync();
                        ViewData["pagedata"] = "eric";
                        return RedirectToAction("index", "Home");
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return Ok(new { mydata = "done" });

        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _db.Subscriptions.ToListAsync());
            //return View();
            // return RedirectToAction("index", "Home");

        }
        public IActionResult Error()
        {
            return View();
        }
    }
}