using EHI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EHI.Data.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder
               .Property(m => m.LastName)
               .IsRequired()
               .HasMaxLength(50);

            builder
               .Property(m => m.Email)
               .IsRequired()
               .HasMaxLength(50);

            builder
              .Property(m => m.Phone)
              .IsRequired()
              .HasMaxLength(20);

            builder
              .Property(m => m.IsActive)
              .IsRequired();

            builder
              .Property(m => m.CreatedOn)
              .IsRequired();

            builder
                .ToTable("Contact");
        }
    
    }
}
