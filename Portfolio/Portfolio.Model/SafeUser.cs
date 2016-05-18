using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class SafeUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public SafeUser() { }

        public static implicit operator SafeUser(User user)
        {
            SafeUser safeUser = new SafeUser();
            safeUser.UserId = user.UserId;
            safeUser.Username = user.Username;
            return safeUser;
        }
    }
}
