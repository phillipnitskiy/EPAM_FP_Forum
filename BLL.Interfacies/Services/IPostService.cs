using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IPostService
    {

        void CreatePost(PostEntity post);
        void DeletePost(PostEntity post);
        void UpdatePost(PostEntity post);

    }
}
