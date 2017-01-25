using System;

namespace ORM.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
