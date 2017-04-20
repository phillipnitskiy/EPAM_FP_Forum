using ORM.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Configurations
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            HasKey(u => u.Id);

            Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(30);

            Property(u => u.Email)
                .IsRequired();

            Property(u => u.RegistrationDate)
                .IsRequired();

            HasOptional(u => u.Profile)
                .WithRequired(p => p.User);

            HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(m =>
                {
                    m.ToTable("UserRole");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                });
        }
    }
}
