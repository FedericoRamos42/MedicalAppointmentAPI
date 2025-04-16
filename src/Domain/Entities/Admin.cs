using Domain.Enums;

namespace Domain.Entities
{
    public class Admin : User
    {
        public Admin()
        {
            Role = UserRole.Admin;
        }
    }
}
