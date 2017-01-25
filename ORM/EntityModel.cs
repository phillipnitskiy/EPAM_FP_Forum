using ORM.Entities;
using ORM.Initializers;
using System.Data.Entity;

namespace ORM
{
    public class EntityModel : DbContext
    {
        static EntityModel()
        {
            Database.SetInitializer(new ForumDbInitializer());
        }

        public EntityModel()
            : base()
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }

        public virtual DbSet<Board> Boards { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Post> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new Configurations.RoleConfig());
            modelBuilder.Configurations.Add(new Configurations.UserConfig());
            modelBuilder.Configurations.Add(new Configurations.ProfileConfig());
            modelBuilder.Configurations.Add(new Configurations.BoardConfig());
            modelBuilder.Configurations.Add(new Configurations.TopicConfig());
            modelBuilder.Configurations.Add(new Configurations.CommentConfig());
        }
    }
}
