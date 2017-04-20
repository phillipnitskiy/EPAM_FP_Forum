using System;
using System.Linq;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using System.Collections.Generic;

namespace MvcPL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserService => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
        public IRoleService RoleService => (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));

        public override bool IsUserInRole(string login, string roleName)
        {
            var user = UserService.GetUserEntityByLogin(login);

            if (user == null) return false;

            var userRoles = RoleService.GetUserRoleEntities(user.Id);

            return userRoles != null && userRoles.Any(role => role.Name == roleName);
        }

        public override string[] GetRolesForUser(string login)
        {
            var user = UserService.GetUserEntityByLogin(login);

            if (user == null) return null;

            return RoleService.GetUserRoleEntities(user.Id)
                ?.Select(role => role.Name)
                .ToArray();
        }

        public override string[] GetAllRoles()
        {
            return RoleService.GetAllRoleEntities()
                .Select(role => role.Name)
                .ToArray();
        }

        public override void AddUsersToRoles(string[] logins, string[] roleNames)
        {
            var users = new List<UserEntity>();
            foreach (var login in logins)
               users.Add(UserService.GetUserEntityByLogin(login));

            var roles = new List<RoleEntity>();
            foreach (var roleName in roleNames)
                roles.Add(RoleService.GetRoleEntityByName(roleName));

            RoleService.AddUsersToRoles(users, roles);
        }

        public override void RemoveUsersFromRoles(string[] logins, string[] roleNames)
        {
            var users = new List<UserEntity>();
            foreach (var login in logins)
                users.Add(UserService.GetUserEntityByLogin(login));

            var roles = new List<RoleEntity>();
            foreach (var roleName in roleNames)
                roles.Add(RoleService.GetRoleEntityByName(roleName));

            RoleService.RemoveUsersFromRoles(users, roles);
        }

        public override bool RoleExists(string roleName)
        {
            return RoleService.GetAllRoleEntities().FirstOrDefault(r => r.Name == roleName) != null;
        }

        #region Stabs

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return RoleService.GetUserEntitiesInRole(roleName)
                ?.Select(user => user.Login)
                .ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }

        #endregion
    }
}