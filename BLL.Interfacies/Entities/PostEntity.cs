using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class PostEntity
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Reported { get; set; }
    }
}
