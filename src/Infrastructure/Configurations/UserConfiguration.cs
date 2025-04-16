using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasDiscriminator(u => u.Role)
            .HasValue<Admin>(UserRole.Admin)   // Asocia 'Admin' con 'UserRole.Admin'
            .HasValue<Patient>(UserRole.Patient) // Asocia 'Patient' con 'UserRole.Patient'
            .HasValue<Doctor>(UserRole.Doctor);

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
               .IsRequired()
               .HasMaxLength(30);

            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(u => u.Address)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Role)
                   .HasConversion(new EnumToStringConverter<UserRole>())
                   .IsRequired();
        }
    }
}
