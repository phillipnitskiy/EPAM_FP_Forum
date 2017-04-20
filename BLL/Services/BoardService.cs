using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;
using BLL.Mappers;
using BLL.Interface.Entities;

namespace BLL.Services
{
    public class BoardService : IBoardService
    {
        #region Fields

        private readonly IUnitOfWork uow;
        private readonly IBoardRepository boardRepository;

        #endregion

        #region Constructor

        public BoardService(IUnitOfWork uow, IBoardRepository boardRepository)
        {
            this.uow = uow;
            this.boardRepository = boardRepository;
        }

        #endregion

        #region Public Methods

        public BoardEntity GetBoard(int id)
        {
            return boardRepository.GetById(id)
                ?.ToBllBoard();
        }

        public IEnumerable<BoardEntity> GetSubBoards(int? boardId)
        {
            return boardRepository.GetAll()
                .Where(b => b.ParentId == boardId)
                .Select(b => b.ToBllBoard());
        }


        public int GetBoardPostsCount(int boardId)
        {
            return boardRepository.GetBoardPostsCount(boardId);
        }

        public int GetBoardTopicsCount(int boardId)
        {
            return boardRepository.GetBoardTopicsCount(boardId);
        }


        public IEnumerable<BoardEntity> GetAllBoards()
        {
            return boardRepository.GetAll()
                .Select(board => board.ToBllBoard());
        }

        public IEnumerable<BoardEntity> GetBoardParents(int boardId)
        {
            var board = boardRepository.GetById(boardId).ToBllBoard();
            var boardParents = new List<BoardEntity>();

            while (board.ParentId != null)
            {
                board = boardRepository.GetById(board.ParentId.GetValueOrDefault()).ToBllBoard();
                boardParents.Add(board);
            } 

            return boardParents;
        }


        public void CreateBoard(BoardEntity board)
        {
            boardRepository.Create(board.ToDalBoard());
            uow.Commit();
        }

        public void DeleteBoard(BoardEntity board)
        {
            throw new NotImplementedException();
        }

        public void UpdateBoard(BoardEntity board)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
