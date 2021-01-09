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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }
    }
}
