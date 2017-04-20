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
    public class ProfileRepository : IProfileRepository
    {

        #region Fields

        private readonly DbContext context;

        #endregion

        #region Constructor

        public ProfileRepository(DbContext context)
        {
            this.context = context;
        }

        #endregion

        #region Public methods

        public DalProfile GetById(int id)
        {
            return context.Set<Profile>()
                .FirstOrDefault(profile => profile.Id == id)
                ?.ToDalProfile();
        }

        public DalProfile GetByPredicate(Expression<Func<DalProfile, bool>> predicate)
        {
            var newPredicate = predicate.Convert<DalProfile, Profile>();

            return context.Set<Profile>()
                .FirstOrDefault(newPredicate)
                ?.ToDalProfile();
        }

        public IEnumerable<DalProfile> GetAll()
        {
            return context.Set<Profile>()
                .ToList()
                .Select(profile => profile.ToDalProfile());
        }

        public void Create(DalProfile profile)
        {
            context.Set<Profile>()
                .Add(profile.ToOrmProfile());
        }

        public void Delete(DalProfile profile)
        {
            throw new NotImplementedException();
        }

        public void Update(DalProfile profile)
        {
            var oldProfile = context.Set<Profile>()
                .FirstOrDefault(p => p.Id == profile.Id);

            oldProfile.ImageData = profile.ImageData;
            oldProfile.ImageMimeType = profile.ImageMimeType;

            context.Entry(oldProfile).State = EntityState.Modified;
        }

        #endregion
    }
}
