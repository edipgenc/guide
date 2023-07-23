using Guide.Book.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
 

namespace Guide.Book.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {                        
            modelBuilder.UseSerialColumns();
            //modelBuilder.Entity<Person>()
            //    .HasMany(b => b.Contacts)
            //    .WithOne(a => a.Person)
            //    .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }
       
    }
}
