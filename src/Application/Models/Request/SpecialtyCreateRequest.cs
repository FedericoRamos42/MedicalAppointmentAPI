using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class SpecialtyCreateRequest
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
