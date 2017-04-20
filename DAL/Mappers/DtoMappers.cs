using DAL.Interface.DTO;
using ORM.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Mappers
{
    public static class DtoMappers
    {
        public static DalUser ToDalUser(this User user)
        {
            return new DalUser
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                RegistrationDate = user.RegistrationDate
            };
        }

        public static User ToOrmUser(this DalUser user)
        {
            return new User
            {
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                RegistrationDate = user.RegistrationDate
            };
        }


        public static DalProfile ToDalProfile(this Profile profile)
        {
            return new DalProfile
            {
                Id = profile.Id,
                ImageData = profile.ImageData,
                ImageMimeType = profile.ImageMimeType
            };
        }

        public static Profile ToOrmProfile(this DalProfile profile)
        {
            return new Profile
            {
                Id = profile.Id,
                ImageData = profile.ImageData,
                ImageMimeType = profile.ImageMimeType
            };
        }


        public static DalRole ToDalRole(this Role role)
        {
            return new DalRole
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }

        public static Role ToOrmRole(this DalRole role)
        {
            return new Role
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }


        public static DalBoard ToDalBoard(this Board board)
        {
            return new DalBoard
            {
                Id = board.Id,
                ParentId = board.ParentBoardId,
                Name = board.Name,
                Description = board.Description
            };
        }

        public static Board ToOrmBoard(this DalBoard board)
        {
            return new Board
            {
                Id = board.Id,
                ParentBoardId = board.ParentId,
                Name = board.Name,
                Description = board.Description
            };
        }


        public static DalTopic ToDalTopic(this Topic topic)
        {
            return new DalTopic
            {
                Id = topic.Id,
                BoardId = topic.BoardId,
                Subject = topic.Subject,
                CreationDate = topic.CreationDate
            };
        }

        public static Topic ToOrmTopic(this DalTopic topic)
        {
            return new Topic
            {
                Id = topic.Id,
                UserId = topic.UserId,
                BoardId = topic.BoardId,
                Subject = topic.Subject,
                CreationDate = topic.CreationDate
            };
        }


        public static DalPost ToDalPost(this Post post)
        {
            return new DalPost
            {
                Id = post.Id,
                UserId = post.UserId,
                TopicId = post.TopicId,
                Text = post.Text,
                CreationDate = post.CreationDate,
                Reported = post.Reported
            };
        }

        public static Post ToOrmPost(this DalPost post)
        {
            return new Post
            {
                Id = post.Id,
                UserId = post.UserId,
                TopicId = post.TopicId,
                Text = post.Text,
                CreationDate = post.CreationDate,
                Reported = post.Reported
            };
        }
    }
}
