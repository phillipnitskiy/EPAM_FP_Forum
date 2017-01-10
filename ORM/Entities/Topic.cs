using System;
using System.Collections.Generic;

namespace ORM.Entities
{
    public class Topic
    {
        public Topic()
        {
            Comments = new List<Comment>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int BoardId { get; set; }
        public virtual Board Board { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
