using ExoCrud.DevenirDev2.Models.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExoCrud.DevenirDev2.Data.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Brand)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Horses)
                .IsRequired();

            builder.Property(c => c.Color)
                .IsRequired();

            builder.Property(c => c.UserId)
                .IsRequired();
        }
    }
}
