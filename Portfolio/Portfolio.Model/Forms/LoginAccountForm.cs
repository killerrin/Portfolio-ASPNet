using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models.Forms
{
    public class LoginAccountForm
    {
        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginAccountForm() { }
        public LoginAccountForm(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
