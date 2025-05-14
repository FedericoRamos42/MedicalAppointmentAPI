using System.Reflection.Emit;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("User");

            builder.HasOne(d => d.Specialty)
                   .WithMany(s => s.Doctors)
                   .HasForeignKey(d => d.SpecialtyId)
                   .IsRequired();

           builder.HasMany(d => d.Availabilities)
                  .WithOne(a => a.Doctor)
                  .HasForeignKey(a => a.DoctorId)
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
