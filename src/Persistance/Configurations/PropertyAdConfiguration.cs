using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class PropertyAdConfiguration : IEntityTypeConfiguration<PropertyAd>
    {
        public void Configure(EntityTypeBuilder<PropertyAd> builder)
        {
            builder.ToTable("PropertyAds");

            builder.HasKey(x=>x.Id);

            builder.Property(x=>x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(x=>x.Title)
                .HasColumnName("Title") 
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x=>x.Description)
                .HasColumnName("Description") 
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x=>x.RoomCount)
                .HasColumnName("RoomCount") 
                .IsRequired();

            builder.Property(x=>x.Area)
                .HasColumnName("Area") 
                .IsRequired()
                .HasPrecision(18,2);

            builder.Property(builder=>builder.Price)
                .HasColumnName("Price")
                .IsRequired()
                .HasPrecision(18,2);

            builder.Property(x=>x.IsExtract)
                .HasColumnName("IsExtract") 
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x=>x.IsMortgage)
                .HasColumnName("IsMortgage") 
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x=>x.OfferType)
                .HasColumnName("OfferType") 
                .IsRequired()
                .HasConversion<int>();

            builder.HasMany(x=>x.PropertyMedias)
                .WithOne(x=>x.PropertyAd)
                .HasForeignKey(x=>x.PropertyAdId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x=>x.PropertyCategory)
                .HasColumnName("PropertyCategoryId")
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x=>x.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(x=>x.UpdatedAt)    
                .HasColumnName("UpdatedAt")
                .IsRequired();

            builder.HasMany(x=>x.PropertyMedias)
                .WithOne(x=>x.PropertyAd)
                .HasForeignKey(x=>x.PropertyAdId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.PropertyCategory);
            builder.HasIndex(x => x.Price);
            builder.HasIndex(x=>x.OfferType);
            builder.HasIndex(x=>x.CreatedAt);

        }
    }
}
