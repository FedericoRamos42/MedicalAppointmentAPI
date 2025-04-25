using Domain.Enums;

namespace Domain.Entities
{
    public class Doctor : User
    {
        public Specialty? Specialty { get; set; }
        public int SpecialtyId { get; set; }
        public List<Appointment> Appoinments { get; set; } = new List<Appointment>();
        public List<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();
        public List<Schedule> Schedules { get; set; } = new List<Schedule>();

        public Doctor()
        {
            Role = UserRole.Doctor;
        }
    }
}
