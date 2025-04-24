using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Configurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedule");

            builder.HasKey(x => x.Id);

            builder.HasOne(s => s.Doctor)
                   .WithMany(d => d.Schedules)
                   .HasForeignKey(s => s.DoctorId)
                   .IsRequired();

            builder.Property(s =>s.DayOfWeek)
                 .HasConversion(new EnumToStringConverter<SheduleDay>())
                 .IsRequired();

        }
    }
}
