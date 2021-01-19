using Bogus;
using Bogus.Extensions;
using Microsoft.EntityFrameworkCore;
using patient_api.Data.Models;
using System;

namespace patient_api.Data
{
    public class PatientContext : DbContext
    {
        public PatientContext() { }
        public PatientContext(DbContextOptions<PatientContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Address> PatientAddresss { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
            .Property(b => b.Created)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Address>()
            .Property(b => b.Created)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Address>()
            .HasOne(p => p.Patient)
            .WithMany(b => b.Addresses)
            .HasForeignKey(p => p.PatientId)
            .HasConstraintName("ForeignKey_Patient_Addresses");

        }
    }
}
