using Portfolio.DAL.Repositories;
using Portfolio.Models.Forms;
using Portfolio.Services;
using Portfolio.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Portfolio.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public UserRepository UserRepo;
        public RoleRepository RoleRepo;

        public AccountController(UserRepository userRepo, RoleRepository roleRepo)
        {
            UserRepo = userRepo;
            RoleRepo = roleRepo;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            Debug.WriteLine($"{nameof(AccountController)} - {nameof(Login)}");
            return View(new LoginAccountForm());
        }

        [HttpPost]
        public ActionResult Login(LoginAccountForm form)
        {
            Debug.WriteLine($"HttpPost: {nameof(AccountController)} - {nameof(Login)}");

            AccountService loginService = new AccountService(ModelState);
            var user = loginService.Login(form);
            if (user != null)
            {
                Debug.WriteLine("Login Successful!");
                FormsAuthentication.SetAuthCookie(user.Username, true);
                return RedirectToRoute("Home");
            }

            return View(form);
        }

        public ActionResult CreateAccount()
        {
            Debug.WriteLine($"{nameof(AccountController)} - {nameof(CreateAccount)}");
            if (!AppSettings.Values.AllowAccountCreation)
                return RedirectToRoute("Home");

            return View(new CreateAccountForm());
        }

        [HttpPost]
        public ActionResult CreateAccount(CreateAccountForm form)
        {
            Debug.WriteLine($"HttpPost: {nameof(AccountController)} - {nameof(CreateAccount)}");
            if (!AppSettings.Values.AllowAccountCreation)
                return RedirectToRoute("Home");

            AccountService loginService = new AccountService(ModelState);
            var user = loginService.CreateAccount(form);
            if (user != null)
            {
                Debug.WriteLine("Account Creation Successful!");
                FormsAuthentication.SetAuthCookie(user.Username, true);
                return RedirectToRoute("Home");
            }

            return View(form);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Debug.WriteLine($"{nameof(AccountController)} - {nameof(Logout)}");
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home");
        }
    }
}