using System;
using System.Collections.Generic;

namespace ORM.Entities
{
    public class Topic
    {
        public Topic()
        {
            Posts = new List<Post>();
        }

        public int Id { get; set; }

        public string Subject { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int BoardId { get; set; }
        public virtual Board Board { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
