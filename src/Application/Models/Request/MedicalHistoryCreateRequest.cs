using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class MedicalHistoryCreateRequest
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int? AppointmentId { get; set; }
        public string ReasonForVisit { get; set; } = default!;
        public string Diagnosis { get; set; } = default!;
        public string? Treatment { get; set; } = default!;
        public string? Notes { get; set; }
    }
}
