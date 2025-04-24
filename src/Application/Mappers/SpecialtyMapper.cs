using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Domain.Entities;

namespace Application.Mappers
{
    public static class SpecialtyMapper
    {
        public static SpecialtyDto ToDto(this Specialty specialty) => new SpecialtyDto
        {
            Id = specialty.Id,
            Name = specialty.Name,
            Description = specialty.Description
        };
        public static List<SpecialtyDto> ToListDto(this IEnumerable<Specialty> specialties) => specialties.Select(x => ToDto(x)).ToList();
    }
}
