using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface ITopicService
    {
        TopicEntity GetTopic(int id);
        TopicEntity GetTopicBySubject(string subject);

        IEnumerable<TopicEntity> FindTopics(string term);

        IEnumerable<TopicEntity> GetAllTopics();

        IEnumerable<BoardEntity> GetTopicParents(int topicId);

        void CreateTopic(TopicEntity topic);
        void DeleteTopic(TopicEntity topic);
        void UpdateTopic(TopicEntity topic);
    }
}
