using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class TopicEntity
    {

        public TopicEntity()
        {
            Posts = new List<PostEntity>();
        }

        public int Id { get; set; }
        public int BoardId { get; set; }
        public int UserId { get; set; }

        public string Subject { get; set; }

        public DateTime CreationDate { get; set; }

        public IEnumerable<PostEntity> Posts { get; set; }

    }
}
