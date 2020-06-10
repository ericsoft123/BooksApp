using Books.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Books.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // List<Book> _books;
        // IRepository<Book> _repo;
        private readonly BookDbContext _db;
        public HomeController(ILogger<HomeController> logger, BookDbContext db)
        {
            _logger = logger;
            // _repo = new MockBookRepository();
            _db = db;

        }

        public IActionResult Index()
        {
            ViewData["pagedata"] = "";
           
            return View(_db.Plan.ToList());
            //return View(_books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
