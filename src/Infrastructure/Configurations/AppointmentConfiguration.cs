using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointment");

            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Patient)
                   .WithMany(p => p.Appoinments)
                   .HasForeignKey(a => a.PatientId)
                   .IsRequired(false);

            builder.HasOne(a => a.Doctor)
                   .WithMany(d => d.Appoinments)
                   .HasForeignKey(a => a.DoctorId)
                   .IsRequired();

            builder.Property(a => a.Status)
                   .HasConversion<string>();

            builder.Property(a => a.Date)
                   .IsRequired();

            builder.Property(a => a.Time)
                   .IsRequired();


        }
    }
}
