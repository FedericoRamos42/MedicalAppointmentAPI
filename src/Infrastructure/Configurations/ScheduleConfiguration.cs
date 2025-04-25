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

            builder.HasMany(s => s.Availabilities)
                   .WithOne(s => s.Schedule)
                   .HasForeignKey(s => s.ScheduleId)
                   .IsRequired();

            

        }
    }
}
