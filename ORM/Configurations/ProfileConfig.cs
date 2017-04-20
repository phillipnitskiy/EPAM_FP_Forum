using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Configurations
{
    public class ProfileConfig : EntityTypeConfiguration<Profile>
    {
        public ProfileConfig()
        {
            //HasKey(b => b.Id);

            Property(p => p.ImageData)
                .IsOptional();

            Property(p => p.ImageMimeType)
                .IsOptional();
        }
    }
}
