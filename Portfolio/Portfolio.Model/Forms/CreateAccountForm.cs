using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models.Forms
{
    public class CreateAccountForm
    {
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name="Re-type Password"), DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Your passwords do not match. Type again!")]
        public string PasswordConfirm { get; set; }

        public CreateAccountForm() { }
        public CreateAccountForm(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }
    }
}
