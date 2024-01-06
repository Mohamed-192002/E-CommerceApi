using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Date.Config
{
    public class CategoryConfigrution : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(30);
            builder.HasData(
                new Category { Id = 1, Name = "Category 1", Description = "C1" },
                new Category { Id = 2, Name = "Category 2", Description = "C2" },
                new Category { Id = 3, Name = "Category 3", Description = "C3" },
                new Category { Id = 4, Name = "Category 4", Description = "C4" }
                );


        }
    }
}
