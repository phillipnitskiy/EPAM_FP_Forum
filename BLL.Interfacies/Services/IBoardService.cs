using BLL.Interface.Entities;
using System.Collections.Generic;

namespace BLL.Interface.Services
{
    public interface IBoardService
    {
        BoardEntity GetBoard(int id);

        IEnumerable<BoardEntity> GetSubBoards(int? boardId);

        IEnumerable<BoardEntity> GetAllBoards();

        IEnumerable<BoardEntity> GetBoardParents(int boardId);

        int GetBoardPostsCount(int boardId);
        int GetBoardTopicsCount(int boardId);

        void CreateBoard(BoardEntity board);
        void DeleteBoard(BoardEntity board);
        void UpdateBoard(BoardEntity board);

    }
}
