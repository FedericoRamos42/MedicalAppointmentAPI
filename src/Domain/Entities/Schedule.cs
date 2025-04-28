using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entities
{
    public class Schedule : BaseEntity
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = default!;
        public List<Availability> Availabilities { get; set; } = new List<Availability>();
    }

}
