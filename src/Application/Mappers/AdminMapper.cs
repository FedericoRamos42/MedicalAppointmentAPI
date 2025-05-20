using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Domain.Entities;

namespace Application.Mappers
{
    public static class AdminMapper
    {
        public static AdminDto ToDto(this Admin admin) => new AdminDto
        {
            Id = admin.Id,
            Name = admin.Name,
            LastName = admin.LastName,
            Email = admin.Email,
            Address = admin.Address,
            PhoneNumber = admin.PhoneNumber,
            isAvailable = admin.IsAvailable,
        };

        public static List<AdminDto> ToListDto(this IEnumerable<Admin> admins) => admins.Select(x => ToDto(x)).ToList();
        
    }
}
