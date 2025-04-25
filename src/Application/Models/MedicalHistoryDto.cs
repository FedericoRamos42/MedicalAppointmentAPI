using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class MedicalHistoryDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = default!;
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = default!;
        public Appointment? Appoinment { get; set; }
        public string ReasonForVisit { get; set; } = default!;
        public string Diagnosis { get; set; } = default!;
        public string? Treatment { get; set; } = default!;
        public string? Notes { get; set; }
    }
}
