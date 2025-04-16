using Domain.Enums;

namespace Domain.Entities
{
    public class Doctor : User
    {
        public Specialty Specialty { get; set; } = new Specialty();
        public int SpecialtyId { get; set; }
        public List<Appointment> Appoinments { get; set; } = new List<Appointment>();
        public List<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();

        public Doctor()
        {
            Role = UserRole.Doctor;
        }
    }
}
