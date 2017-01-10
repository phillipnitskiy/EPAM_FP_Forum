using ORM.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Configurations
{
    public class CommentConfig : EntityTypeConfiguration<Comment>
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
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}