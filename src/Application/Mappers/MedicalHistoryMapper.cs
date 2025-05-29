using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Domain.Entities;

namespace Application.Mappers
{
    public static class MedicalHistoryMapper
    {
        public static MedicalHistoryDto ToDto(this MedicalHistory medicalHistory) => new MedicalHistoryDto
        {
            Id = medicalHistory.Id,
            PatientId = medicalHistory.PatientId,
            DoctorId = medicalHistory.DoctorId,
            AppoinmentId = medicalHistory.AppoinmentId,
            ReasonForVisit = medicalHistory.ReasonForVisit,
            Diagnosis = medicalHistory.Diagnosis,
            Treatment = medicalHistory.Treatment,
            Notes = medicalHistory.Notes,
        };
        public static List<MedicalHistoryDto> ToListDto(this IEnumerable<MedicalHistory> medicalHistories) => medicalHistories.Select(x => ToDto(x)).ToList();
    }
}
