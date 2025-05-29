using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Request;
using Application.Result;
using Domain.Interfaces;

namespace Application.Services
{
    public class MedicalHistoryService : IMedicalHistoryService
    {
        private readonly IMedicalHistoryRepository _medicalHistoryRepository;
        public MedicalHistoryService(IMedicalHistoryRepository medicalHistoryRepository)
        {
            _medicalHistoryRepository = medicalHistoryRepository;
        }
        public async Task<Result<MedicalHistoryDto>> Create(MedicalHistoryCreateRequest request)
        {
            Domain.Entities.MedicalHistory medicalHistory = new Domain.Entities.MedicalHistory()
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                AppoinmentId = request.AppointmentId,
                ReasonForVisit = request.ReasonForVisit,
                Diagnosis = request.Diagnosis,
                Treatment = request.Treatment,
                Notes = request.Notes
            };
            await _medicalHistoryRepository.AddAsync(medicalHistory);
            var dto = medicalHistory.ToDto();
            return Result<MedicalHistoryDto>.Success(dto);
        }
        public async Task<Result<MedicalHistoryDto>> Delete(int id)
        {
            Domain.Entities.MedicalHistory medicalHistory = await _medicalHistoryRepository.GetByIdAsync(id);
            await _medicalHistoryRepository.DeleteAsync(medicalHistory);
            var dto = medicalHistory.ToDto();
            return Result<MedicalHistoryDto>.Success(dto);
        }
        public async Task<Result<IEnumerable<MedicalHistoryDto>>> GetAll()
        {
            IEnumerable<Domain.Entities.MedicalHistory> list = await _medicalHistoryRepository.GetAllAsync();
            var dto = list.ToListDto();
            return Result<IEnumerable<MedicalHistoryDto>>.Success(dto);
        }
        public async Task<Result<MedicalHistoryDto>> GetById(int id)
        {
            Domain.Entities.MedicalHistory medicalHistory = await _medicalHistoryRepository.GetByIdAsync(id);
            var dto = medicalHistory.ToDto();
            return Result<MedicalHistoryDto>.Success(dto);
        }
    }
}
