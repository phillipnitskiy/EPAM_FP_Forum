using ORM.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Configurations
{
    public class BoardConfig : EntityTypeConfiguration<Board>
    {
        public BoardConfig()
        {
            HasKey(b => b.Id);

            Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(200);

            Property(b => b.Description)
                .IsOptional()
                .HasMaxLength(200);

            HasMany(b => b.SubBoards)
                .WithOptional(b => b.ParentBoard)
                .HasForeignKey(b => b.ParentBoardId);

            HasMany(b => b.Topics)
                .WithRequired(t => t.Board)
                .HasForeignKey(b => b.BoardId);
        }
    }
}
