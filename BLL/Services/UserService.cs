using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using System.Web.Helpers;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        #endregion

        #region Constructor

        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            this.uow = uow;
            this.userRepository = repository;
        }
        
        #endregion
        
        #region Public Methods

        public UserEntity GetUserEntity(int id)
        {
            return userRepository.GetById(id)
                ?.ToBllUser();
        } 

        public UserEntity GetUserEntityByLogin(string login)
        {
            return userRepository.GetByPredicate(u => u.Login == login)
                ?.ToBllUser();
        }

        public UserEntity GetUserEntityByEmail(string email)
        {
            return userRepository.GetByPredicate(u => u.Email == email)
                ?.ToBllUser();
        }

        public IEnumerable<UserEntity> GetAllUserEntities()
        {
            return userRepository.GetAll()
                .Select(user => user.ToBllUser());
        }

        public bool ValidateUser(string login, string password)
        {
            var user = GetUserEntityByLogin(login);

            return user != null && Crypto.VerifyHashedPassword(user.Password, password);
        }

        public void CreateUser(UserEntity user)
        {
            user.Password = Crypto.HashPassword(user.Password);
            user.RegistrationDate = DateTime.Now;

            userRepository.Create(user.ToDalUser());
            uow.Commit();
        }

        public void DeleteUser(UserEntity user)
        {
            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }

        public void UpdateUser(UserEntity user)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
