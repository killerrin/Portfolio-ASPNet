using Portfolio.DAL.Repositories;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portfolio.WebUI.Infrastructure
{
	public class RoleProvider : System.Web.Security.RoleProvider
	{
		public override string[] GetRolesForUser(string username)
		{
			List<string> userRoles = new List<string>();

			UserRepository userRepo = new UserRepository(new DAL.Data.DataContext());
			var user = userRepo.GetAll().Where(x => x.Username.Equals(username)).FirstOrDefault();
			if (user != null)
			{
				foreach (var role in user.Roles)
				{
					userRoles.Add(role.Name);
				}
			}

			return userRoles.ToArray();
		}

		public override string[] GetAllRoles()
		{
			RoleRepository roleRepo = new RoleRepository(new DAL.Data.DataContext());
			return roleRepo.GetAll().Select(x => x.Name).ToArray();
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			var userRoles = GetRolesForUser(username);
			foreach (var role in userRoles)
				if (role == roleName)
					return true;
			return false;
		}

		public override bool RoleExists(string roleName)
		{
			var roles = GetAllRoles();
			foreach (var role in roles)
			{
				if (role.Equals(roleName))
					return true;
			}
			return false;
		}

		public override string[] GetUsersInRole(string roleName)
		{
			List<string> users = new List<string>();

			RoleRepository roleRepo = new RoleRepository(new DAL.Data.DataContext());
			var role = roleRepo.GetAll().Where(x => x.Name == roleName).FirstOrDefault();
			if (role != null)
			{
				users.AddRange(role.Users.Select(x => x.Username));
			}

			return users.ToArray();
		}

		#region Not Implemented
		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}


		#endregion
	}
}