using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthenticationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
              
        public async Task<User?> Authenticate(string email, string password)
        {
            User? userToAuthenticate = await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return userToAuthenticate;
        }
    }
}
