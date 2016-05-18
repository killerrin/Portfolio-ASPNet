using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;
using Portfolio.Models;
using Portfolio.DAL.Repositories;

namespace Portfolio.WebUI
{
    public static class Auth
    {
        private const string UserKey = "Portfolio.WebUI.Auth.UserKey";
        public static User User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[UserKey] as User;
                if (user == null)
                {
                    UserRepository userRepo = new UserRepository(new DAL.Data.DataContext());
                    user = userRepo.GetAll().FirstOrDefault(x => x.Username.Equals(HttpContext.Current.User.Identity.Name));
        
                    if (user == null)
                        return null;

                    HttpContext.Current.Items[UserKey] = user;
                }
        
                return user;
            }
        }
    }
}
