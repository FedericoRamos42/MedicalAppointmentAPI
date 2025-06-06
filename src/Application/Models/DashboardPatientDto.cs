using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class DashboardPatientDto
    {
        public int AppointmentsConfirmed { get; set; }
        public int AppointmentsCanceled { get; set; }
        public AppointmentDto? NextAppointment { get; set; } = default!;
        public int MedicalHistoryTotal { get; set; }
        public MedicalHistoryDto? LastMedicalHistory { get; set; } = default!;
    }
}
