﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250416191447_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("Domain.Entities.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("DoctorId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointment", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.MedicalHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AppoinmentId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Diagnosis")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<int>("DoctorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Notes")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReasonForVisit")
                        .IsRequired()
                        .HasMaxLength(700)
                        .HasColumnType("TEXT");

                    b.Property<string>("Treatment")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppoinmentId")
                        .IsUnique();

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalHistory", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Specialty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Specialty", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Role");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.Admin", b =>
                {
                    b.HasBaseType("Domain.Entities.User");

                    b.ToTable("User", (string)null);

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("Domain.Entities.Doctor", b =>
                {
                    b.HasBaseType("Domain.Entities.User");

                    b.Property<int>("SpecialtyId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("User", (string)null);

                    b.HasDiscriminator().HasValue("Doctor");
                });

            modelBuilder.Entity("Domain.Entities.Patient", b =>
                {
                    b.HasBaseType("Domain.Entities.User");

                    b.Property<string>("HealtInsurance")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("User", (string)null);

                    b.HasDiscriminator().HasValue("Patient");
                });

            modelBuilder.Entity("Domain.Entities.Appointment", b =>
                {
                    b.HasOne("Domain.Entities.Doctor", "Doctor")
                        .WithMany("Appoinments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Patient", "Patient")
                        .WithMany("Appoinments")
                        .HasForeignKey("PatientId");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Domain.Entities.MedicalHistory", b =>
                {
                    b.HasOne("Domain.Entities.Appointment", "Appoinment")
                        .WithOne("MedicalHistory")
                        .HasForeignKey("Domain.Entities.MedicalHistory", "AppoinmentId");

                    b.HasOne("Domain.Entities.Doctor", "Doctor")
                        .WithMany("MedicalHistories")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Patient", "Patient")
                        .WithMany("MedicalHistories")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appoinment");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Domain.Entities.Doctor", b =>
                {
                    b.HasOne("Domain.Entities.Specialty", "Specialty")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("Domain.Entities.Appointment", b =>
                {
                    b.Navigation("MedicalHistory");
                });

            modelBuilder.Entity("Domain.Entities.Specialty", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("Domain.Entities.Doctor", b =>
                {
                    b.Navigation("Appoinments");

                    b.Navigation("MedicalHistories");
                });

            modelBuilder.Entity("Domain.Entities.Patient", b =>
                {
                    b.Navigation("Appoinments");

                    b.Navigation("MedicalHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
