using Portfolio.DAL.Repositories;
using Portfolio.Models.Forms;
using Portfolio.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            return View(new LoginAccountForm());
        }

        [HttpPost]
        public ActionResult Login(LoginAccountForm form)
        {
            LoginService loginService = new LoginService(ModelState);
            var user = loginService.Login(form);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Username, true);
                return RedirectToRoute("Default");
            }

            return View(form);
        }

        public ActionResult CreateAccount()
        {
            return View(new CreateAccountForm());
        }

        [HttpPost]
        public ActionResult CreateAccount(CreateAccountForm form)
        {
            LoginService loginService = new LoginService(ModelState);
            var user = loginService.CreateAccount(form);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Username, true);
                return RedirectToRoute("Default");
            }

            return View(form);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Default");
        }
    }
}