using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IProfileService
    {
        ProfileEntity GetProfileEntity(int id);

        void CreateProfile(ProfileEntity profile);
        void DeleteProfile(ProfileEntity profile);
        void UpdateProfile(ProfileEntity profile);
    }
}
