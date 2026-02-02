using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance.Configurations
{
    public class PropertyMediaConfiguration
    {
        public class PropertyAdConfiguration : IEntityTypeConfiguration<PropertyMedia>
        {
            public void Configure(EntityTypeBuilder<PropertyMedia> builder)
            {
                builder.ToTable("PropertyMedias");

                builder.HasKey(pm => pm.Id);

                builder.Property(pm => pm.MediaName)
                    .HasColumnName("MediaName")
                    .IsRequired()
                    .HasMaxLength(200);

                builder.Property(pm => pm.MediaUrl)
                    .HasColumnName("MediaUrl")
                    .IsRequired()
                    .HasMaxLength(500);

                builder.Property(pm => pm.Order)
                    .HasColumnName("Order")
                    .IsRequired();

                builder.Property(pm => pm.PropertyAdId)
                    .HasColumnName("PropertyAdId")
                    .IsRequired();

                builder.Property(pm => pm.CreatedAt)
                    .HasColumnName("CreatedAt")
                    .IsRequired();
       

                builder.Property(pm => pm.UpdatedAt)
                    .HasColumnName("UpdatedAt")
                    .IsRequired();


                builder.HasOne(pm => pm.PropertyAd)
                    .WithMany(pa => pa.PropertyMedias)
                    .HasForeignKey(pm => pm.PropertyAdId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasIndex(pm => pm.PropertyAdId);
                builder.HasIndex(z=>new { z.PropertyAdId, z.Order });
            }
        }
    }
}
