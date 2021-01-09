﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using patient_api.Data;

namespace patient_api.Data.Migrations
{
    [DbContext(typeof(PatientContext))]
    [Migration("20210109153242_MakeCreatedDefaultComputed")]
    partial class MakeCreatedDefaultComputed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("patient_api.Data.Models.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MI")
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("MedicalRecordNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Race")
                        .HasColumnType("int");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });
#pragma warning restore 612, 618
        }
    }
}
