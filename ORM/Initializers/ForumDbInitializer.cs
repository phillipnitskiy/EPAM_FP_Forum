using ORM.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace ORM.Initializers
{
    public class ForumDbInitializer : DropCreateDatabaseAlways<EntityModel>
    {

        protected override void Seed(EntityModel context)
        {
            var roles = new List<Role>
            {
                new Role { Name = "Administrator", Description = "Administrator" },
                new Role { Name = "Moderator", Description = "Moderator" },
                new Role { Name = "User", Description = "User" },
                new Role { Name = "Guest", Description = "Guest" }
            };

            context.Roles.AddRange(roles);

            base.Seed(context);
        }

    }
}
