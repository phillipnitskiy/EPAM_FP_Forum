using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class BoardEntity
    {

        public BoardEntity()
        {
            SubBoards = new List<BoardEntity>();
            Topics = new List<TopicEntity>();
        }

        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<BoardEntity> SubBoards { get; set; }
        public IEnumerable<TopicEntity> Topics { get; set; }
    }
}
