using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Domain.Entities;

namespace Application.Mappers
{
    public static class DoctorMapper
    {
        public static DoctorDto ToDto(this Doctor doctor) => new DoctorDto
        {
            Id = doctor.Id,
            Name = doctor.Name,
            LastName = doctor.LastName,
            Email = doctor.Email,
            Address = doctor.Address,
            PhoneNumber = doctor.PhoneNumber,
            SpecialtyId = doctor.SpecialtyId,
            IsAvailable = doctor.IsAvailable
        };
        public static List<DoctorDto> ToListDto(this IEnumerable<Doctor> doctors) => doctors.Select(x => ToDto(x)).ToList();
    }
}
