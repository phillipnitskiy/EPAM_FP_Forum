using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using System.Data.Entity;
using ORM.Entities;
using System.Linq;
using DAL.Mappers;
using DAL.Visitor;

namespace DAL.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        #region Fields

        private readonly DbContext context;

        #endregion

        #region Constructor

        public RoleRepository(DbContext uow)
        {
            this.context = uow;
        }

        #endregion

        #region Public methods

        public DalRole GetById(int id)
        {
            throw new NotImplementedException();
        }

        public DalRole GetByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            var newPredicate = predicate.Convert<DalRole, Role>();

            return context.Set<Role>()
                .FirstOrDefault(newPredicate)
                ?.ToDalRole();
        }

        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<Role>()
                .ToList()
                .Select(role => role.ToDalRole());
        }

        public IEnumerable<DalRole> GetUserRoles(int userId)
        {
            return context.Set<User>()
                .FirstOrDefault(user => user.Id == userId)
                ?.Roles.Select(role => role.ToDalRole());
        }

        public IEnumerable<DalUser> GetUsersInRole(DalRole role)
        {
            return context.Set<User>()
                .Where(user => user.Roles.Any(r => r == role.ToOrmRole()))
                .Select(user => user.ToDalUser());
        }

        public void AddUserToRole(DalUser user, DalRole role)
        {
            var ormUser = context.Set<User>()
                .FirstOrDefault(u => u.Login == user.Login);

            var ormRole = context.Set<Role>()
                .FirstOrDefault(r => r.Name == role.Name);

            ormRole.Users.Add(ormUser);

            context.Entry(ormRole).State = EntityState.Modified;

            context.SaveChanges();

        }

        public void RemoveUserFromRole(DalUser user, DalRole role)
        {
            var ormUser = context.Set<User>()
                .FirstOrDefault(u => u.Login == user.Login);

            var ormRole = context.Set<Role>()
                .FirstOrDefault(r => r.Name == role.Name);

            ormRole.Users.Remove(ormUser);

            context.Entry(ormRole).State = EntityState.Modified;

            context.SaveChanges();

        }

        public void Create(DalRole role)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalRole role)
        {
            throw new NotImplementedException();
        }

        public void Update(DalRole role)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
