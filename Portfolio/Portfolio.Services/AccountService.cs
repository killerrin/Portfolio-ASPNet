using Portfolio.DAL.Extensions;
using Portfolio.DAL.Repositories;
using Portfolio.Models;
using Portfolio.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Portfolio.Services
{
    public class AccountService
    {
        public ModelStateDictionary Errors { get; protected set; }

        public AccountService(ModelStateDictionary modelState)
        {
            SetModelState(modelState);
        }

        public void SetModelState(ModelStateDictionary modelState)
        {
            if (modelState == null)
                Errors = new ModelStateDictionary();
            Errors = modelState;
        }

        public User Login(LoginAccountForm loginInfo)
        {
            UserRepository userRepo = new UserRepository(new DAL.Data.DataContext());

            // Grab the User based off of their Username
            var user = userRepo.GetAll().Where(x => x.Username.Equals(loginInfo.Username)).FirstOrDefault();
            if (user != null)
            {
                if (CheckPassword(loginInfo.Password, user.PasswordHash))
                {
                    return user;                
                }
            }

            Errors.AddModelError("", "Your username or password is incorrect");
            return null;
        }

        public User CreateAccount(CreateAccountForm newAccount)
        {
            UserRepository userRepo = new UserRepository(new DAL.Data.DataContext());

            // Preform Validation
            if (!ValidateUsername(newAccount.Username)) { Errors.AddModelError(nameof(newAccount.Username), "Username is Invalid"); }
            if (!ValidateEmail(newAccount.Email)) { Errors.AddModelError(nameof(newAccount.Email), "Email is Invalid"); }
            if (!ValidatePassword(newAccount.Password)) { Errors.AddModelError(nameof(newAccount.Password), "Password is Invalid"); }

            // Ensure there are no duplicate users
            var allUsers = userRepo.GetAll();
            if (allUsers.Where(x => x.Username.Equals(newAccount.Username)).FirstOrDefault() != null)
            {
                Errors.AddModelError(nameof(newAccount.Username), "A user with this username already exists");
                FakeHash();
            }

            // If there were errors, jump out now
            if (!Errors.IsValid) return null;

            // Create, Save and return the User
            User user = new User();
            user.Username = newAccount.Username;
            user.PasswordHash = HashPassword(newAccount.Password);
            user.Email = newAccount.Email;
            userRepo.Insert(user);
            userRepo.Commit();
            return user;
        }

        private const int WorkFactor = 13;
        private static void FakeHash() { BCrypt.Net.BCrypt.HashPassword("", WorkFactor); }
        private static string HashPassword(string password) { return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor); }
        private bool CheckPassword(string password, string hash) { return BCrypt.Net.BCrypt.Verify(password, hash); }
        private bool CheckPasswordExpiry(DateTime expiry)
        {
            if (expiry == null) return false;
            return expiry <= DateTime.UtcNow;
        }

        public static bool ValidateUsername(string _username)
        {
            if (!IntExtensions.IsBetween(_username.Length, 1, 40))
                return false;

            if (!Regex.IsMatch(_username, @"^[a-zA-Z0-9\s]"))
                return false;

            return true;
        }

        public static bool ValidateEmail(string _email)
        {
            if (Regex.IsMatch(_email, @"^[\w!#$%&'*+/=?`{|}~^-]+(?:\.[!#$%&'*+/=?`{|}~^-]+)*@(?:[A-Z0-9-]+\.)+[A-Z]{2,6}$"))
                return false;
            return true;
        }

        public static bool ValidatePassword(string _password)
        {
            // Check password contains at least one digit, one lower case 
            // letter, one uppercase letter, and is between 8 and 10 characters long
            if (!Regex.IsMatch(_password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,10}$"))
                return false;
            return true;
        }
    }
}
