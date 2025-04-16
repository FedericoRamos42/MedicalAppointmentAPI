using Domain.Abstractions;

namespace Domain.Entities
{
    public class Specialty : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public List<Doctor> Doctors { get; set; } = new List<Doctor>(); 
    }
}
