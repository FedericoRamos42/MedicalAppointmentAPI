using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Request;
using Application.Result;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Services
{
    public class MedicalHistoryService : IMedicalHistoryService
    {
        private readonly IMedicalHistoryRepository _medicalHistoryRepository;
        private readonly IValidator<MedicalHistoryCreateRequest> _validate;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public MedicalHistoryService(IMedicalHistoryRepository medicalHistoryRepository, 
            IValidator<MedicalHistoryCreateRequest> validate,
            IPatientRepository patientRepository,
            IDoctorRepository doctorRepository,
            IAppointmentRepository appointmentRepository)
        {
            _medicalHistoryRepository = medicalHistoryRepository;
            _validate = validate;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
        }
        public async Task<Result<MedicalHistoryDto>> Create(MedicalHistoryCreateRequest request)
        {
            var validationResult = _validate.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<MedicalHistoryDto>.FailureModels(errors);
            }

            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            if(doctor is null)
            {
                return Result<MedicalHistoryDto>.Failure($"Doctor with Id {request.DoctorId} dows not exist");
            }

            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            if (patient is null)
            {
                return Result<MedicalHistoryDto>.Failure($"Doctor with Id {request.DoctorId} dows not exist");
            }

            MedicalHistory medicalHistory = new MedicalHistory()
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
            MedicalHistory medicalHistory = await _medicalHistoryRepository.GetByIdAsync(id);
            
            if (medicalHistory is null)
            {
                return Result<MedicalHistoryDto>.Failure($"Medical History with Id {id} dows not exist");
            }

            await _medicalHistoryRepository.DeleteAsync(medicalHistory);
            var dto = medicalHistory.ToDto();
            return Result<MedicalHistoryDto>.Success(dto);
        }
        public async Task<Result<IEnumerable<MedicalHistoryDto>>> GetAll()
        {
            IEnumerable<MedicalHistory> list = await _medicalHistoryRepository.GetAllAsync();
            var dto = list.ToListDto();
            return Result<IEnumerable<MedicalHistoryDto>>.Success(dto);
        }
        public async Task<Result<MedicalHistoryDto>> GetById(int id)
        {
            MedicalHistory medicalHistory = await _medicalHistoryRepository.GetByIdAsync(id);

            if (medicalHistory is null)
            {
                return Result<MedicalHistoryDto>.Failure($"Medical History with Id {id} dows not exist");
            }

            var dto = medicalHistory.ToDto();
            return Result<MedicalHistoryDto>.Success(dto);
        }
    }
}
