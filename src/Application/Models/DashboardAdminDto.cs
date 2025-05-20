using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class DashboardAdminDto
    {
        public int AppointmentTotal { get; set; }
        public int AppoinmentToday { get; set; }
        public int ConfirmedAppointment {  get; set; }
        public int CanceledAppointment { get; set; }
        public int ActivePatient { get; set; }
        public int ActiveDoctor { get; set; }
    }
}
