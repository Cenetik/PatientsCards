using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        

        public void Rebuild()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        
        //public DbSet<Person> Persons { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().UseTpcMappingStrategy();

            modelBuilder.Entity<Doctor>().UseTpcMappingStrategy().ToTable("Doctors");
            modelBuilder.Entity<Patient>().UseTpcMappingStrategy().ToTable("Patients");

            modelBuilder.Entity<Person>().Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Person>().Property(p => p.LastName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Person>().Property(p => p.Patronymic)
                .HasMaxLength(50);

            modelBuilder.Entity<Person>().Property(p => p.Gender)
               .HasDefaultValue(0);


        }
        
    }
}