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
        PostEntity GetPostEntity(int id);
        IEnumerable<PostEntity> GetTopicPosts(int topicId);

        void ReportPost(int id);
        void UnreportPost(int id);
        IEnumerable<PostEntity> GetReportedPosts();

        void DeletePost(int id);

        void CreatePost(PostEntity post);
        void DeletePost(PostEntity post);
        void UpdatePost(PostEntity post);

    }
}
