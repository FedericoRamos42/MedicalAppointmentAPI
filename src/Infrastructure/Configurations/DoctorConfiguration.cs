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

            builder.HasOne(d => d.Schedule)
                   .WithOne(s => s.Doctor)
                   .HasForeignKey<Schedule>(s => s.DoctorId)
                   .IsRequired()  
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
