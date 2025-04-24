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
        public Task<Result<SpecialtyDto>> Delete(int id)
        {

        }
        public Task<Result<IEnumerable<SpecialtyDto>>> GetAll()
        {
            
        }
        public Task<Result<SpecialtyDto>> GetById(int id)
        {
            
        }
    }
}
