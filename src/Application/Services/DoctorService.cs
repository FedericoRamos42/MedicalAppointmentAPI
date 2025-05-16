using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Request;
using Application.Models.Response;
using Application.Result;
using Application.Validations.Doctor;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;
        private readonly IValidator<DoctorCreateRequest> _validatorCreate;
        private readonly IPasswordHasherService _passwordHasherService;
        public DoctorService(IDoctorRepository repository, IValidator<DoctorCreateRequest> validator, IPasswordHasherService passwordHasherService)
        {
            _repository = repository;
            _validatorCreate = validator;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<Result<DoctorDto>> Create(DoctorCreateRequest request)
        {
            var validationResult = _validatorCreate.Validate(request);
            if (!validationResult.IsValid)
            {
                List<string> errorsModels = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<DoctorDto>.FailureModels(errorsModels);
            }
            var hashedPassword = _passwordHasherService.HashPassword(request.Password);

            Doctor doctor = new Doctor()
            {
                Name = request.Name,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Address = request.Address,
                Password = hashedPassword,
                SpecialtyId = request.SpecialtyId,
                IsAvailable = request.IsAvailable
            };
            await _repository.AddAsync(doctor);
            var dto = doctor.ToDto();
            return Result<DoctorDto>.Success(dto);
        }

        public async Task<Result<DoctorDto>> Delete(int id)
        {
            var doctor = await _repository.GetByIdAsync(id);
            doctor.IsAvailable = false;
            await _repository.UpdateAsync(doctor);
            var dto = doctor.ToDto();
            return Result<DoctorDto>.Success(dto);
        }

        public async Task<Result<IEnumerable<DoctorDto>>> GetAll()
        { 
            IEnumerable<Doctor> list = await _repository.GetAllAsync();
            var dto = list.ToListDto();
            return Result<IEnumerable<DoctorDto>>.Success(dto);
        }

        public async Task<Result<DoctorDto>> GetById(int id)
        {
            Doctor doctor = await _repository.GetByIdAsync(id);
            var dto = doctor.ToDto();
            return Result<DoctorDto>.Success(dto);
        }

        public async Task<Result<DoctorResponse>> GetWithAvailabilities(int id)
        {
            var doctor = await  _repository.GetWithAvailabities(id);
            var dto = new DoctorResponse()
            {
                Id = id,
                Name = doctor.Name,
                LastName = doctor.LastName,
                PhoneNumber = doctor.PhoneNumber,
                Email = doctor.Email,
                Address = doctor.Address,
                SpecialtyId = doctor.SpecialtyId,
                IsAvailable = doctor.IsAvailable,
                Availabilities = doctor.Availabilities.ToListDto()
            };

            return Result<DoctorResponse>.Success(dto);
        }

        public async Task<Result<DoctorDto>> Update(int id, DoctorUpdateRequest request)
        {
            Doctor doctor = await _repository.GetByIdAsync(id);
            doctor.Name = request.Name;
            doctor.LastName = request.LastName;
            doctor.PhoneNumber = request.PhoneNumber;
            doctor.Email = request.Email;
            doctor.Address = request.Address;
            doctor.SpecialtyId = request.SpecialtyId;
            doctor.IsAvailable = request.IsAvailable;

            await _repository.UpdateAsync(doctor);
            var dto = doctor.ToDto();
            return Result<DoctorDto>.Success(dto);
        }

       
    }
}
