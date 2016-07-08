using Portfolio.WebUI.Areas.Admin.Controllers;
using System.Web.Mvc;

namespace Portfolio.WebUI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            var namespaces = new[] { typeof(CategoryController).Namespace };

            context.MapRoute("AdminCategory", "admin/categories", new { controller = "Category", action = "Index" }, namespaces);
            context.MapRoute("AdminPortfolioEntries", "admin/portfolio-entries", new { controller = "PortfolioEntry", action = "Index" }, namespaces);

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Category", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}