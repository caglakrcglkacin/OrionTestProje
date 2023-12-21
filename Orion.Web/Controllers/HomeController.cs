using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Orion.Bussines.Ýnterface;
using Orion.Domain;
using Orion.Dto;
using Orion.Web.Models;
using Orion.Web.ViewModels;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;

namespace Orion.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        UserManager<User> _userManager;
        IUserStore<User> _userStore;
        IUser _User;
        SignInManager<User> _signInManager;
        private readonly IDataProtectionProvider _dataProtectionProvider;

        public HomeController(ILogger<HomeController> logger,
            IMapper mapper,
            UserManager<User> userManager,
            IUserStore<User> userStore,
            IUser User,
            SignInManager<User> signInManager,
            IDataProtectionProvider dataProtectionProvider)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _userStore = userStore;
            _User = User;
            _signInManager = signInManager;
            _dataProtectionProvider = dataProtectionProvider;
        }

        public async Task<IActionResult> Index()
        {
            var isSignedIn = User.Identity.IsAuthenticated;

            if (isSignedIn)
            {
                // Kullanýcý zaten oturum açmýþsa phone listesi sayfasýna yönlendir
                return RedirectToAction("Liste", "Phone");
            }

            // Ýþlem devam ediyor, normal olarak login sayfasýný göster
            var model = new LoginViewModel
            {
                RembemberMe = true
            };

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.KullaniciAdý);

            if (user == null)
                return View(login);

            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RembemberMe, false);

            if (result.Succeeded)
            {
                if (login.RembemberMe)
                {
                    var existingCookie = Request.Cookies["UserSettings"]; // Çerez adýný "UserSettings" olarak deðiþtir

                    if (existingCookie != null && existingCookie == "true")
                    {
                        // Çerez ayný ise otomatik giriþ yap
                        return RedirectToAction("Liste", "Phone");
                    }

                    // Çerez yoksa veya deðeri "true" deðilse, çerezi oluþtur ve otomatik giriþ yap
                    Response.Cookies.Append("UserSettings", "true", new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,  // Eðer uygulamanýz HTTPS üzerinden çalýþýyorsa bu özelliði etkinleþtirin
                        Expires = DateTime.Now.AddDays(7)  // Çerezin süresini ihtiyacýnýza göre ayarlayýn
                    });

                    return RedirectToAction("Liste", "Phone");
                }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Giriþ Yapamadýnýz. E-posta veya Þifre Yanlýþtýr.";
            return View(login);
        }
        public IActionResult Save()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(UserDto user)
        {
            if (user.Password == user.AginPassword)
            {
                var binteUser = new User();   
                binteUser.FullName = user.FullName;

                await _userManager.SetEmailAsync(binteUser, user.Email);
                var result = await _userStore.CreateAsync(binteUser, new CancellationToken());
                if (result.Succeeded)
                {
                    await _userManager.SetUserNameAsync(binteUser, user.Email);
                    var ps = await _userManager.AddPasswordAsync(binteUser, user.Password);

                    TempData["SuccessMessage"] = "Kaydýnýz Baþarý Þekilde Oluþturuldu";
                  
                    return Redirect("/Home/Index");
                }
                return View(user);
            }
            ViewBag.ErrorMessage = "Kaydýnýz Yapýlamadý";
            return View(user);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
