using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class EmailDto
    {
        public string Para {  get; set; } = string.Empty;
        public string Asunto { get; set; } = string.Empty;
        public string Contenido {  get; set; } = string.Empty;
    }
}
