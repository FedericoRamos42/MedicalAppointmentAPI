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
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        public AvailabilityService(IAvailabilityRepository availabilityRepository)
        {
            _availabilityRepository = availabilityRepository;
        }

        public async Task<Result<AvailabilityDto>> Create(AvailabilityCreateRequest request)
        {
            Availability availability = new Availability()
            {
                DoctorId = request.DoctorId,
                DayOfWeek = request.DayOfWeek,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
            };

            await _availabilityRepository.AddAsync(availability);
            var dto = availability.ToDto();
            return Result<AvailabilityDto>.Success(dto);
        }


        public async Task<Result<AvailabilityDto>> Delete(int id)
        {
            var availability = await _availabilityRepository.GetByIdAsync(id);
            await _availabilityRepository.DeleteAsync(availability);
            var dto = availability.ToDto();
            return Result<AvailabilityDto>.Success(dto);
        }

        public async Task<Result<AvailabilityDto>> Update(int id, AvailabilityUpdateRequest request)
        {
            Availability availability = await _availabilityRepository.GetByIdAsync(id);

            availability.StartTime = request.StartTime;
            availability.EndTime = request.EndTime;

            await _availabilityRepository.UpdateAsync(availability);
            var dto = availability.ToDto();
            return Result<AvailabilityDto>.Success(dto);
        }
    }
}
