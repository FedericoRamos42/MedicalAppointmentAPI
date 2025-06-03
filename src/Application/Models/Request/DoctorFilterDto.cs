using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class DoctorFilterDto
    {
        public string? SpecialtyName { get; set; }
        public string? Name { get; set; }
        public bool? IsAvailable { get; set; }
    }
}
