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
using FluentValidation;

namespace Application.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IValidator<AvailabilityCreateRequest> _createValidator;
        private readonly IValidator<AvailabilityUpdateRequest> _updateValidator;


        public AvailabilityService(IAvailabilityRepository availabilityRepository,
            IValidator<AvailabilityCreateRequest> createValidator,
            IValidator<AvailabilityUpdateRequest> updateValidator)
        {
            _availabilityRepository = availabilityRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<Result<AvailabilityDto>> Create(AvailabilityCreateRequest request)
        {
            var result = _createValidator.Validate(request);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<AvailabilityDto>.FailureModels(errors);
            }
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
            if (availability == null)
            {
                return Result<AvailabilityDto>.Failure($"Availability with Id {id} that not exist");
            }
            await _availabilityRepository.DeleteAsync(availability);
            var dto = availability.ToDto();
            return Result<AvailabilityDto>.Success(dto);
        }

        public async Task<Result<AvailabilityDto>> Update(int id, AvailabilityUpdateRequest request)
        {
            Availability availability = await _availabilityRepository.GetByIdAsync(id);
            if (availability == null)
            {
                return Result < AvailabilityDto>.Failure($"Availability with Id {id} that not exist");
            }
            var result = _updateValidator.Validate(request);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<AvailabilityDto>.FailureModels(errors);
            }

            availability.StartTime = request.StartTime;
            availability.EndTime = request.EndTime;

            await _availabilityRepository.UpdateAsync(availability);
            var dto = availability.ToDto();
            return Result<AvailabilityDto>.Success(dto);
        }
    }
}
