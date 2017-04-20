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
    public class TopicService : ITopicService
    {
        #region Fields

        private readonly IUnitOfWork uow;
        private readonly IBoardRepository boardRepository;
        private readonly ITopicRepository topicRepository;

        #endregion

        #region Constructor

        public TopicService(IUnitOfWork uow, IBoardRepository boardRepository, ITopicRepository topicRepository)
        {
            this.uow = uow;
            this.boardRepository = boardRepository;
            this.topicRepository = topicRepository;
        }

        #endregion

        #region Public Methods

        public TopicEntity GetTopic(int id)
        {
            return topicRepository.GetById(id)
                ?.ToBllTopic();
        }

        public TopicEntity GetTopicBySubject(string subject)
        {
            return topicRepository.GetByPredicate(t => t.Subject == subject)
                ?.ToBllTopic();
        }

        public IEnumerable<TopicEntity> FindTopics(string term)
        {
            return topicRepository.GetAll()
                .Where(t => t.Subject.ToUpper().Contains(term.ToUpper()))
                .Select(t => t.ToBllTopic());
        }

        public IEnumerable<TopicEntity> GetAllTopics()
        {
            return topicRepository.GetAll()
                .Select(topic => topic.ToBllTopic());
        }


        public IEnumerable<BoardEntity> GetTopicParents(int topicId)
        {
            var topic = topicRepository.GetById(topicId).ToBllTopic();
            var topicParents = new List<BoardEntity>();

            var board = boardRepository.GetById(topic.BoardId).ToBllBoard();
            topicParents.Add(board);

            while (board.ParentId != null)
            {
                board = boardRepository.GetById(board.ParentId.GetValueOrDefault()).ToBllBoard();
                topicParents.Add(board);
            }

            return topicParents;
        }


        public void CreateTopic(TopicEntity topic)
        {
            topicRepository.Create(topic.ToDalTopic());
            uow.Commit();
        }

        public void DeleteTopic(TopicEntity topic)
        {
            throw new NotImplementedException();
        }

        public void UpdateTopic(TopicEntity topic)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
