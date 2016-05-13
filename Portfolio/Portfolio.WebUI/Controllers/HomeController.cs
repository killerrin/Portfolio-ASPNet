using Portfolio.Contracts.Repositories;
using Portfolio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepositoryBase<PortfolioEntry> PortfolioEntries;
        public HomeController(IRepositoryBase<PortfolioEntry> portfolioEntries)
        {
            PortfolioEntries = portfolioEntries;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}