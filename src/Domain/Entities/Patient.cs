using Domain.Enums;

namespace Domain.Entities
{
    public class Patient : User
    {
        public HealtInsurance HealtInsurance { get; set; }
        public List<Appointment> Appoinments { get; set; } = new List<Appointment>();
        public List<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();
        public Patient()
        {
            Role = UserRole.Patient;
        }
    }
}
