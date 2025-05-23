﻿using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public static class PatientMapper
    {
        public static PatientDto ToDto(this Patient patient) => new PatientDto
        {
            Id = patient.Id,
            Name = patient.Name,
            LastName = patient.LastName,
            Email = patient.Email,
            Address = patient.Address,
            PhoneNumber = patient.PhoneNumber,
            HealtInsurance = patient.HealtInsurance.ToString(),
            IsAvailable = patient.IsAvailable,
        };

       
        public static List<PatientDto> ToListDto(this IEnumerable<Patient> patients) => patients.Select(x => ToDto(x)).ToList();
    }
}
