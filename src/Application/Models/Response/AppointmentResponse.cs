using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Models.Response
{
    public class AppointmentResponse
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = default!;
        public int? PatientId { get; set; }
        public string PatientName { get; set; } = default!;
        public string Date { get; set; } = default!;
        public TimeSpan Time { get; set; }
        public string AppointmentInfo { get; set; } = default!;
        public AppointmentStatus Status { get; set; }
    }
}
