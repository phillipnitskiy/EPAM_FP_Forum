using ORM.Entities;
using ORM.Initializers;
using System.Data.Entity;

namespace ORM
{
    public class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=EntityModel")
        {
            Database.SetInitializer(new ForumDbInitializer());
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Board> Boards { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new Configurations.RoleConfig());
            modelBuilder.Configurations.Add(new Configurations.UserConfig());
            modelBuilder.Configurations.Add(new Configurations.BoardConfig());
            modelBuilder.Configurations.Add(new Configurations.TopicConfig());
            modelBuilder.Configurations.Add(new Configurations.CommentConfig());
        }
    }
}
