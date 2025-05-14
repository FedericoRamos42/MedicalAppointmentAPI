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

namespace Application.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _repository;
        public SpecialtyService(ISpecialtyRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<SpecialtyDto>> Create(SpecialtyCreateRequest request)
        {
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
            var dto = specialty.ToDto();
            return Result<SpecialtyDto>.Success(dto);
        }

        public async Task<Result<SpecialtyDto>> Update(int id, SpecialtyUpdateRequest request)
        {
            var specialty = await _repository.GetByIdAsync(id);

            specialty.Name = request.Name;
            specialty.Description = request.Description;

            await _repository.UpdateAsync(specialty);

            var dto = specialty.ToDto();
            return Result<SpecialtyDto>.Success(dto);
        }
    }
}
