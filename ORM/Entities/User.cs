using System;
using System.Collections.Generic;

namespace ORM.Entities
{
    public class User
    {
        public User()
        {
            Topics = new List<Topic>();
            Comments = new List<Comment>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}



