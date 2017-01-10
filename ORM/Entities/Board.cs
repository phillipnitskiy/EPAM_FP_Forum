using System.Collections.Generic;

namespace ORM.Entities
{
    public partial class Board
    {
        public Board()
        {
            SubBoards = new List<Board>();
            Topics = new List<Topic>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentBoardId { get; set; }
        public virtual Board ParentBoard { get; set; }

        public virtual ICollection<Board> SubBoards { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}
