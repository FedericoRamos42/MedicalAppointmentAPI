using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Request;
using Application.Result;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<PatientDto>> Create(PatientCreateRequest request)
        {
            Patient patient = new Patient()
            {
                Name = request.Name,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Address = request.Address,
                Password = request.Password,
                //HealtInsurance = request.HealtInsurance,
                IsAvailable = request.IsAvailable
            };
            await _repository.AddAsync(patient);
            var dto = patient.ToDto();
            return Result<PatientDto>.Success(dto);
        }
        public async Task<Result<PatientDto>> Delete(int id)
        {
            Patient patient = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(patient);
            var dto = patient.ToDto();
            return Result<PatientDto>.Success(dto);
        }
        public async Task<Result<IEnumerable<PatientDto>>> GetAll()
        {
            IEnumerable<Patient> list = await _repository.GetAllAsync();
            var dto = list.ToListDto();
            return Result<IEnumerable<PatientDto>>.Success(dto);
        }
        public async Task<Result<PatientDto>> GetById(int id)
        {
            Patient patient = await _repository.GetByIdAsync(id);
            var dto = patient.ToDto();
            return Result<PatientDto>.Success(dto);
        }
        public async Task<Result<PatientDto>> Update(int id, PatientUpdateRequest request)
        {
            Patient patient = await _repository.GetByIdAsync(id);
            patient.Name = request.Name;
            patient.LastName = request.LastName;
            patient.PhoneNumber = request.PhoneNumber;
            patient.Email = request.Email;
            patient.Address = request.Address;
            //patient.HealtInsurance = request.HealtInsurance,
            patient.IsAvailable = request.IsAvailable;    

            await _repository.UpdateAsync(patient); 
            var dto = patient.ToDto();  
            return Result<PatientDto>.Success(dto);
        }
    }
}
