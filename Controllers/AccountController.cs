using Books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Books.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signManager;

        public AccountController(UserManager<ApplicationUser> UserManager, SignInManager<ApplicationUser> SignManager)
        {
            userManager = UserManager;
            signManager = SignManager;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> register(Registerview model)
        {


            if (ModelState.IsValid)//means when we pass variable from model/form data
            {
                var user = new ApplicationUser { 
                    UserName = model.Email,
                    Email = model.Email ,
                    FirstName=model.FirstName,
                    LastName=model.LastName
                };
                var result = await userManager.CreateAsync(user, model.Password);//registration /insert data
                if (result.Succeeded)//if create user will be successful then sign in that account 
                {
                    await signManager.SignInAsync(user, isPersistent: false);//is persistent for data save as session
                    return RedirectToAction("index", "Home"); //means that it will direct to home controller and go to index action/index page
                }
                //else 
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);


        }
        [HttpGet]
        [Authorize]
        public IActionResult view()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Loginview model)
        {


            if (ModelState.IsValid)//means when we pass variable from model/form data
            {

                var result = await signManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);//registration /insert data
                if (result.Succeeded)//if create user will be successful then sign in that account 
                {
                    //is persistent for data save as session
                    return RedirectToAction("index", "Home"); //means that it will direct to home controller and go to index action/index page
                }
                //else 


                ModelState.AddModelError("", "Invalid Email and Password");

            }
            return View(model);


        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
