using bART_Task.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace bART_Task.Domain
{
    public class bARTDBContext : DbContext
    {
        public bARTDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasIndex(contact => contact.Email)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .HasIndex(account => account.Name)
                .IsUnique();
        }
    }
}
