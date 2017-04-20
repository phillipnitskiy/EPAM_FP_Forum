using ORM.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Configurations
{
    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            HasKey(u => u.Id);

            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(20);

            Property(r => r.Description)
                .IsOptional()
                .HasMaxLength(200);
        }

    }
}
