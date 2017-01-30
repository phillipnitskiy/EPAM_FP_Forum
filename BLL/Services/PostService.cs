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

        public PostEntity GetPostEntity(int id)
        {
            return postRepository.GetById(id)?.ToBLLPost();
        }

        public IEnumerable<PostEntity> GetTopicPosts(int topicId)
        {
            return postRepository.GetAll()
                .Where(p => p.TopicId == topicId)
                .Select(p => p.ToBLLPost());
        }


        public void ReportPost(int id)
        {
            var post = postRepository.GetById(id);
            post.Reported = true;
            postRepository.Update(post);
            uow.Commit();
        }

        public void UnreportPost(int id)
        {
            var post = postRepository.GetById(id);
            post.Reported = false;
            postRepository.Update(post);
            uow.Commit();
        }

        public IEnumerable<PostEntity> GetReportedPosts()
        {
            return postRepository.GetAll()
                .Where(p => p.Reported == true)
                .Select(p => p.ToBLLPost());
        }

        public void DeletePost(int id)
        {
            var post = postRepository.GetById(id);
            postRepository.Delete(post);
            uow.Commit();
        }

        public void CreatePost(PostEntity post)
        {
            postRepository.Create(post.ToDalPost());
            uow.Commit();
        }

        public void DeletePost(PostEntity post)
        {
            postRepository.Delete(post.ToDalPost());
            uow.Commit();
        }

        public void UpdatePost(PostEntity post)
        {            
            postRepository.Update(post.ToDalPost());
            uow.Commit();
        }

        #endregion
    }
}
