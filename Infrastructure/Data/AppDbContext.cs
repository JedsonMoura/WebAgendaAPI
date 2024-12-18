using Domain.Entities;
using Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ContactConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }

}
