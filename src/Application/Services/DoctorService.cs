using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Request;
using Application.Models.Response;
using Application.Result;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;
        public DoctorService(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<DoctorDto>> Create(DoctorCreateRequest request)
        {
            Doctor doctor = new Doctor()
            {
                Name = request.Name,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Address = request.Address,
                Password = request.Password,
                SpecialtyId = request.SpecialtyId,
                IsAvailable = request.IsAvailable
            };
            await _repository.AddAsync(doctor);
            var dto = doctor.ToDto();
            return Result<DoctorDto>.Success(dto);
        }

        public async Task<Result<DoctorDto>> Delete(int id)
        {
            Doctor doctor = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(doctor);
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
