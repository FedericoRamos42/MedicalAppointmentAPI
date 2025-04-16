using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entities
{
    public abstract class User : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address {  get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password {  get; set; } = default!;
        public UserRole Role { get; set; }
        public bool IsAvailable { get; set; } = true; 
    }
}
