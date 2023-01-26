using Lumia.Models;
using Lumia.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lumia.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            AppUser user = new AppUser()
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                Fullname = registerVM.Fullname
            };
            var result = await _userManager.CreateAsync(user,registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View(registerVM);
                }
            }
            //var role = _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
            _userManager.AddToRoleAsync(user, "admin");
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);
            AppUser existUser = await _userManager.FindByNameAsync(loginVM.UserName);
            if (existUser is null)
            {
                ModelState.AddModelError("", "Not found");
                return View(loginVM);
            }
            if ( !(await _userManager.CheckPasswordAsync(existUser,loginVM.Password)))
            {
                ModelState.AddModelError("", "username or password is incorrect");
                return View(loginVM);
            }
            await _signInManager.PasswordSignInAsync(existUser, loginVM.Password, false, true);
            return RedirectToAction("index", "home");
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("index","home");
        }
    }
}
