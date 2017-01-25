using ORM.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Configurations
{
    public class TopicConfig : EntityTypeConfiguration<Topic>
    {
        public TopicConfig()
        {
            HasKey(t => t.Id);

            Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(200);

            Property(t => t.CreationDate)
                .IsRequired();

            HasRequired(t => t.User)
                .WithMany(u => u.Topics)
                .HasForeignKey(t => t.UserId);

            HasMany(t => t.Posts)
                .WithRequired(t => t.Topic)
                .HasForeignKey(t => t.TopicId);
        }
    }
}
