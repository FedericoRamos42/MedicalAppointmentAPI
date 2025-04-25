using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Mappers
{
    public static class MedicalHistoryMapper
    {
        public static MedicalHistoryDto ToDto(this Domain.Entities.MedicalHistory medicalHistory) => new MedicalHistoryDto
        {
            Id = medicalHistory.Id,
            PatientId = medicalHistory.PatientId,
            DoctorId = medicalHistory.DoctorId,
            Appoinment = medicalHistory.Appoinment,
            ReasonForVisit = medicalHistory.ReasonForVisit,
            Diagnosis = medicalHistory.Diagnosis,
            Treatment = medicalHistory.Treatment,
            Notes = medicalHistory.Notes,
        };
        public static List<MedicalHistoryDto> ToListDto(this IEnumerable<Domain.Entities.MedicalHistory> medicalHistories) => medicalHistories.Select(x => ToDto(x)).ToList();
    }
}
