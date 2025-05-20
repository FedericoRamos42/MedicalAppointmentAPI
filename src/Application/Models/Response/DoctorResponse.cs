using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models.Response
{
    public class DoctorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int SpecialtyId { get; set; }
        public bool IsAvailable { get; set; } = true;
        public List<AvailabilityDto> Availabilities { get; set; } = [];
    }
}
