using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Data.EntityConfiguration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(550).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(25).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();


            builder.HasData(
                new Contact(1, "Jedson", "jedmoura27@gmail.com", "81996324062", true)
            );
        }
    }
}
