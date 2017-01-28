using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System.Linq;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        public static DalUser ToDalUser(this UserEntity user)
        {
            return new DalUser()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                RegistrationDate = user.RegistrationDate
            };
        }

        public static UserEntity ToBllUser(this DalUser user)
        {
            return new UserEntity()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                RegistrationDate = user.RegistrationDate
            };
        }


        public static DalProfile ToDalProfile(this ProfileEntity profile)
        {
            return new DalProfile
            {
                Id = profile.Id,
                ImageData = profile.ImageData,
                ImageMimeType = profile.ImageMimeType
            };
        }

        public static ProfileEntity ToBllProfile(this DalProfile profile)
        {
            return new ProfileEntity
            {
                Id = profile.Id,
                ImageData = profile.ImageData,
                ImageMimeType = profile.ImageMimeType
            };
        }


        public static DalRole ToDalRole(this RoleEntity role)
        {
            return new DalRole
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }

        public static RoleEntity ToBllRole(this DalRole role)
        {
            return new RoleEntity
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }


        public static DalBoard ToDalBoard(this BoardEntity board)
        {
            return new DalBoard
            {
                Id = board.Id,
                ParentId = board.ParentId,
                Name = board.Name,
                Description = board.Description
            };
        }

        public static BoardEntity ToBllBoard(this DalBoard board)
        {
            var subBoards = board.SubBoards.ToList().Select(b => b.ToBllBoard());
            var topics = board.Topics.ToList().Select(t => t.ToBllTopic());

            return new BoardEntity
            {
                Id = board.Id,
                ParentId = board.ParentId,
                Name = board.Name,
                Description = board.Description,
                SubBoards = subBoards,
                Topics = topics
            };
        }


        public static DalTopic ToDalTopic(this TopicEntity topic)
        {
            return new DalTopic
            {
                Id = topic.Id,
                UserId = topic.UserId,
                BoardId = topic.BoardId,
                Subject = topic.Subject,
                CreationDate = topic.CreationDate
            };
        }

        public static TopicEntity ToBllTopic(this DalTopic topic)
        {
            var posts = topic.Posts.ToList().Select(t => t.ToBLLPost());

            return new TopicEntity
            {
                Id = topic.Id,
                UserId = topic.UserId,
                BoardId = topic.BoardId,
                Subject = topic.Subject,
                CreationDate = topic.CreationDate,
                Posts = posts
            };
        }


        public static DalPost ToDalPost(this PostEntity post)
        {
            return new DalPost
            {
                Id = post.Id,
                TopicId = post.TopicId,
                UserId = post.UserId,
                Text = post.Text,
                CreationDate = post.CreationDate,
                Reported = post.Reported
            };
        }

        public static PostEntity ToBLLPost(this DalPost post)
        {
            return new PostEntity
            {
                Id = post.Id,
                TopicId = post.TopicId,
                UserId = post.UserId,
                Text = post.Text,
                CreationDate = post.CreationDate,
                Reported = post.Reported
            };
        }
    }
}
