using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthenticationRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByEmail(string email);

    }
}
