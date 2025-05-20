using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Request;
using Application.Result;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IValidator<PatientUpdateRequest> _updateValidator;
        private readonly IValidator<PatientCreateRequest> _createValidator;


        public PatientService(IPatientRepository repository, 
                IPasswordHasherService passwordHasherService,
                IValidator<PatientUpdateRequest> updateValidator, 
                IValidator<PatientCreateRequest> createValidator)
        {
            _repository = repository;
            _passwordHasherService = passwordHasherService;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
        }

        public async Task<Result<PatientDto>> Create(PatientCreateRequest request)
        {
            var validationResult = _createValidator.Validate(request);
            if (!validationResult.IsValid) 
            {
                var errors = validationResult.Errors.Select(e=>e.ErrorMessage).ToList();
                return Result<PatientDto>.FailureModels(errors);
            }
            
            var hashedPassword = _passwordHasherService.HashPassword(request.Password);

            Patient patient = new Patient()
            {
                Name = request.Name,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Address = request.Address,
                Password = hashedPassword,
                HealtInsurance = request.HealtInsurance,
                IsAvailable = request.IsAvailable
            };
            await _repository.AddAsync(patient);
            var dto = patient.ToDto();
            return Result<PatientDto>.Success(dto);
        }
        public async Task<Result<PatientDto>> Delete(int id)
        {
            var patient = await _repository.GetByIdAsync(id);

            if(patient is null)
            {
                return Result<PatientDto>.Failure($"Patient with id {id} does not exist.");
            }

            patient.IsAvailable = false;
            await _repository.UpdateAsync(patient);
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
            if (patient is null)
            {
                return Result<PatientDto>.Failure($"Patient with id {id} does not exist.");
            }
            var dto = patient.ToDto();
            return Result<PatientDto>.Success(dto);
        }
        public async Task<Result<PatientDto>> Update(int id, PatientUpdateRequest request)
        {
            Patient patient = await _repository.GetByIdAsync(id);
            if (patient is null)
            {
                return Result<PatientDto>.Failure($"Patient with id {id} does not exist.");
            }

            var validationResult = _updateValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<PatientDto>.FailureModels(errors);
            }

            patient.Name = request.Name;
            patient.LastName = request.LastName;
            patient.PhoneNumber = request.PhoneNumber;
            patient.Email = request.Email;
            patient.Address = request.Address;
            patient.HealtInsurance = request.HealtInsurance;  
            
            await _repository.UpdateAsync(patient); 
            var dto = patient.ToDto();  
            return Result<PatientDto>.Success(dto);
        }
    }
}
