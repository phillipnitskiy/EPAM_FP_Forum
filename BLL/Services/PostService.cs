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
    public class PostService : IPostService
    {
        #region Fields

        private readonly IUnitOfWork uow;
        private readonly IPostRepository postRepository;

        #endregion

        #region Constructor

        public PostService(IUnitOfWork uow, IPostRepository postRepository)
        {
            this.uow = uow;
            this.postRepository = postRepository;
        }

        #endregion

        #region Public Methods

        public void CreatePost(PostEntity post)
        {
            postRepository.Create(post.ToDalPost());
            uow.Commit();
        }

        public void DeletePost(PostEntity post)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(PostEntity post)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
