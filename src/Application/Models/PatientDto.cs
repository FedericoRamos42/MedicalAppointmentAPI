using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string LastName { get; set; }= default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string HealtInsurance { get; set; } = default!;
        public bool IsAvailable { get; set; } = true;

    }
}
