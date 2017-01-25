using ORM.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Configurations
{
    public class CommentConfig : EntityTypeConfiguration<Post>
    {
        public CommentConfig()
        {
            HasKey(c => c.Id);

            Property(c => c.CreationDate)
                .IsRequired();

            Property(c => c.Text)
                .IsRequired()
                .HasMaxLength(15000);

            HasRequired(c => c.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}