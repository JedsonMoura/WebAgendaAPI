using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Data.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).HasMaxLength(550).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(250).IsRequired();

            builder.HasData(
                new User(1, "Admin", "Admin")
            );
        }
    }
}
