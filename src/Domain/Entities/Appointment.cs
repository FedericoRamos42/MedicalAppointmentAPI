using Domain.Abstractions;
using Domain.Enums;


namespace Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public Doctor Doctor { get; set; } = default!;
        public int DoctorId { get; set; }
        public Patient? Patient { get; set; }
        public int? PatientId { get; set; }
        public MedicalHistory? MedicalHistory { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public AppointmentStatus Status { get; set; }

    }
}
