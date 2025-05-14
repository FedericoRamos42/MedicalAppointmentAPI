using Domain.Enums;

namespace Domain.Entities
{
    public class Doctor : User
    {
        public Specialty Specialty { get; set; } = default!;
        public int SpecialtyId { get; set; }
        public List<Appointment> Appoinments { get; set; } = new List<Appointment>();
        public List<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();
        public List<Availability> Availabilities { get; set; }

        public Doctor()
        {
            Role = UserRole.Doctor;
        }
    }
}
