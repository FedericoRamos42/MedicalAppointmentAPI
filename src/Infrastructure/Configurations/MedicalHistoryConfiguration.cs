using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class MedicalHistoryConfiguration : IEntityTypeConfiguration<MedicalHistory>
    {
        public void Configure(EntityTypeBuilder<MedicalHistory> builder)
        {
            builder.ToTable("MedicalHistory");

            builder
                .HasOne(m => m.Patient)
                .WithMany(p => p.MedicalHistories)
                .HasForeignKey(m => m.PatientId)
                .IsRequired();

            builder
                .HasOne(m => m.Doctor)
                .WithMany(d => d.MedicalHistories)
                .HasForeignKey(m => m.DoctorId)
                .IsRequired();

            builder
                .HasOne(m => m.Appoinment)
                .WithOne(a => a.MedicalHistory)
                .HasForeignKey<MedicalHistory>(m => m.AppoinmentId)
                .IsRequired(false);

            builder.HasKey(m=> m.Id);

            builder.Property(m => m.Notes)
                    .HasMaxLength(1000)
                   .IsRequired(false);

            builder.Property(m => m.Diagnosis)
                    .HasMaxLength(500)
                   .IsRequired();

            builder.Property(m => m.Treatment)
                    .HasMaxLength(1000)
                   .IsRequired(false);

            builder.Property(m => m.ReasonForVisit)
                    .HasMaxLength(700)
                   .IsRequired();


        }
    }
}
