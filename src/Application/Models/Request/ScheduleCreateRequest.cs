using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models.Request
{
    public class ScheduleCreateRequest
    {
        public int DoctorId { get; set; }  
        public List<AvailabilityCreateRequest> Availabilities { get; set; } = new List<AvailabilityCreateRequest>();
    }
}
