using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProfileService : IProfileService
    {
        #region Fields

        private readonly IUnitOfWork uow;
        private readonly IProfileRepository profileRepository;

        #endregion

        #region Constructor

        public ProfileService(IUnitOfWork uow, IProfileRepository profileRepository)
        {
            this.uow = uow;
            this.profileRepository = profileRepository;
        }

        #endregion

        #region Public Methods

        public ProfileEntity GetProfileEntity(int id)
        {
            return profileRepository.GetById(id)
                ?.ToBllProfile();
        }

        public void CreateProfile(ProfileEntity profile)
        {
            profileRepository.Create(profile.ToDalProfile());
            uow.Commit();
        }

        public void DeleteProfile(ProfileEntity profile)
        {
            throw new NotImplementedException();
        }

        public void UpdateProfile(ProfileEntity profile)
        {
            profileRepository.Update(profile.ToDalProfile());
            uow.Commit();
        }

        #endregion
    }
}
