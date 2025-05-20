using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
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
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _repository;
        private readonly IValidator<SpecialtyCreateRequest> _specialtyCreateValidator;
        private readonly IValidator<SpecialtyUpdateRequest> _specialtyUpdateValidator;
        public SpecialtyService(ISpecialtyRepository repository, IValidator<SpecialtyUpdateRequest> specialtyUpdateValidator, IValidator<SpecialtyCreateRequest> specialtyCreateValidator)
        {
            _repository = repository;
            _specialtyUpdateValidator = specialtyUpdateValidator;
            _specialtyCreateValidator = specialtyCreateValidator;
        }
        public async Task<Result<SpecialtyDto>> Create(SpecialtyCreateRequest request)
        {
            var result = _specialtyCreateValidator.Validate(request);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<SpecialtyDto>.FailureModels(errors);
            }
            Specialty specialty = new Specialty()
            {
                Name = request.Name,
                Description = request.Description
            };
            await _repository.AddAsync(specialty);
            var dto = specialty.ToDto();
            return Result<SpecialtyDto>.Success(dto);
        }
        public async Task<Result<SpecialtyDto>> Delete(int id)
        {
            Specialty specialty = await _repository.GetByIdAsync(id);
            if (specialty is null)
            {
                return Result<SpecialtyDto>.Failure($"Specialty with id {id} does not exist");
            }
            await _repository.DeleteAsync(specialty);
            var dto = specialty.ToDto();
            return Result<SpecialtyDto>.Success(dto);
        }
        public async Task<Result<IEnumerable<SpecialtyDto>>> GetAll()
        {
            IEnumerable<Specialty> list = await _repository.GetAllAsync();
            var dto = list.ToListDto();
            return Result<IEnumerable<SpecialtyDto>>.Success(dto);
        }
        public async Task<Result<SpecialtyDto>> GetById(int id)
        {
            Specialty specialty = await _repository.GetByIdAsync(id);
            if (specialty is null)
            {
                return Result<SpecialtyDto>.Failure($"Specialty with id {id} does not exist");
            }
            var dto = specialty.ToDto();
            return Result<SpecialtyDto>.Success(dto);
        }

        public async Task<Result<SpecialtyDto>> Update(int id, SpecialtyUpdateRequest request)
        {
            var result = _specialtyUpdateValidator.Validate(request);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<SpecialtyDto>.FailureModels(errors);
            }

            var specialty = await _repository.GetByIdAsync(id);
            if (specialty is null)
            {
                return Result<SpecialtyDto>.Failure($"Specialty with id {id} does not exist");
            }

            specialty.Name = request.Name;
            specialty.Description = request.Description;

            await _repository.UpdateAsync(specialty);

            var dto = specialty.ToDto();
            return Result<SpecialtyDto>.Success(dto);
        }
    }
}
