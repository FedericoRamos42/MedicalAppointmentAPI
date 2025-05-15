using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class DashboardDoctorDto
    {
        public int AppointmentTotal { get; set; }
        public int AppoinmentToday { get; set; }
        public int AppointmentCanceled { get; set; }
        public int AppointmentConfirmed { get; set; }
        public int MedicalHistoryTotal{ get; set; }
    }
}
