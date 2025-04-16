using Domain.Abstractions;

namespace Domain.Entities
{
    public class MedicalHistory : BaseEntity
    {
        public Patient Patient { get; set; } = default!;
        public int PatientId { get; set; }
        public Doctor Doctor { get; set; } = default!;
        public int DoctorId { get; set; }  
        public Appointment? Appoinment { get; set; }
        public int? AppoinmentId { get;set; }
        public string ReasonForVisit { get; set; } = default!;
        public string Diagnosis { get; set; } = default!;
        public string? Treatment { get; set; } = default!;
        public string? Notes { get; set; }
    }
}
