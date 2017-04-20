using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;
using ORM.Entities;
using DAL.Mappers;
using DAL.Visitor;

namespace DAL.Concrete
{
    public class TopicRepository : ITopicRepository
    {
        #region Fields

        private readonly DbContext context;

        #endregion

        #region Constructor

        public TopicRepository(DbContext context)
        {
            this.context = context;
        }

        #endregion

        #region Public methods

        public DalTopic GetById(int id)
        {
            var topic = context.Set<Topic>()
                .FirstOrDefault(t => t.Id == id)
                ?.ToDalTopic();

            if (topic == null)
                return null;

            topic.Posts = context.Set<Post>()
                .Where(p => p.TopicId == topic.Id)
                .ToList()
                .Select(p => p.ToDalPost());

            return topic;
        }

        public DalTopic GetByPredicate(Expression<Func<DalTopic, bool>> predicate)
        {
            var newPredicate = predicate.Convert<DalTopic, Topic>();

            return context.Set<Topic>()
                .FirstOrDefault(newPredicate)
                ?.ToDalTopic();
        }

        public IEnumerable<DalTopic> GetAll()
        {
            return context.Set<Topic>()
                .ToList()
                .Select(topic => topic.ToDalTopic());
        }

        public void Create(DalTopic topic)
        {
            context.Set<Topic>()
                .Add(topic.ToOrmTopic());
        }

        public void Delete(DalTopic topic)
        {
            throw new NotImplementedException();
        }

        public void Update(DalTopic topic)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
