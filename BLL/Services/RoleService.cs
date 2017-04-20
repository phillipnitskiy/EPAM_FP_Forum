using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using BLL.Mappers;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        #region Fields

        private readonly IUnitOfWork uow;
        private readonly IRoleRepository roleRepository;

        #endregion

        #region Constructor

        public RoleService(IUnitOfWork uow, IRoleRepository repository)
        {
            this.uow = uow;
            this.roleRepository = repository;
        }

        #endregion

        #region Public Methods

        public RoleEntity GetRoleEntity(int id)
        {
            return roleRepository.GetById(id)
                ?.ToBllRole();
        }

        public RoleEntity GetRoleEntityByName(string roleName)
        {
            return roleRepository.GetByPredicate(r => r.Name == roleName)
                ?.ToBllRole();
        }

        public IEnumerable<RoleEntity> GetAllRoleEntities()
        {
            return roleRepository.GetAll()
                ?.Select(role => role.ToBllRole());
        }

        public IEnumerable<RoleEntity> GetUserRoleEntities(int userId)
        {
            return roleRepository.GetUserRoles(userId)
                ?.Select(role => role.ToBllRole());
        }

        public IEnumerable<UserEntity> GetUserEntitiesInRole(string roleName)
        {
            var role = roleRepository.GetByPredicate(r => r.Name == roleName);
            return roleRepository.GetUsersInRole(role)
                .Select(user => user.ToBllUser());
        }

        public void AddUsersToRoles(ICollection<UserEntity> users, ICollection<RoleEntity> roles)
        {
            foreach (var user in users)
                foreach (var role in roles)
                    roleRepository.AddUserToRole(user.ToDalUser(), role.ToDalRole());
        }

        public void RemoveUsersFromRoles(ICollection<UserEntity> users, ICollection<RoleEntity> roles)
        {
            foreach (var user in users)
                foreach (var role in roles)
                    roleRepository.RemoveUserFromRole(user.ToDalUser(), role.ToDalRole());
        }

        public void CreateRole(RoleEntity role)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(RoleEntity role)
        {
            throw new NotImplementedException();
        }

        public void UpdateRole(RoleEntity role)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
