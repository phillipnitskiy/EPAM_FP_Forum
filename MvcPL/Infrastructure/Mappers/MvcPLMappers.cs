using BLL.Interface.Entities;
using MvcPL.Models.Input;
using System.Linq;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMappers
    {

        public static UserEntity ToBllUser(this UserRegistrationInputModel user)
        {
            return new UserEntity()
            {
                Login = user.Login,
                Email = user.Email,
                Password = user.Password
            };
        }

        public static BoardViewModel ToPlBoard(this BoardEntity board)
        {
            var subBoards = board.SubBoards.Select(b => b.ToPlBoard()).ToList();
            var topics = board.Topics.Select(t => t.ToPlTopic()).ToList();

            return new BoardViewModel
            {
                Id = board.Id,
                Name = board.Name,
                Description = board.Description,
                SubBoards = subBoards,
                Topics = topics
            };
        }

        public static BoardEntity ToBllBoard(this BoardInputModel board)
        {
            return new BoardEntity
            {
                ParentId = board.ParentId,
                Name = board.Name,
                Description = board.Description
            };
        }


        public static TopicViewModel ToPlTopic(this TopicEntity topic)
        {
            var posts = topic.Posts.ToList().Select(t => t.ToPlPost()).ToList();

            return new TopicViewModel
            {
                Id = topic.Id,
                BoardId = topic.BoardId,
                Subject = topic.Subject,
                CreationDate = topic.CreationDate,
                Posts = posts
            };
        }

        public static TopicEntity ToBllTopic(this TopicInputModel topic)
        {
            return new TopicEntity
            {
                BoardId = topic.BoardId,
                Subject = topic.Subject
            };
        }


        public static PostViewModel ToPlPost(this PostEntity post)
        {
            var user = new UserViewModel { Id = post.UserId };

            return new PostViewModel
            {
                Id = post.Id,
                TopicId = post.TopicId,
                User = user,
                Text = post.Text,
                CreationDate = post.CreationDate,
                Reported = post.Reported
            };
        }

        public static PostEntity ToBllPost(this PostInputModel post)
        {
            return new PostEntity
            {
                Text = post.Text
            };
        }


        public static UserViewModel ToPlUser(this UserEntity user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                RegistrationDate = user.RegistrationDate
            };
        }


        public static ProfileEntity ToBllProfile(this UserProfileInputModel profile)
        {
            return new ProfileEntity
            {
                Id = profile.Id,
                ImageData = profile.ImageData,
                ImageMimeType = profile.ImageMimeType
            };
        }

    }
}
