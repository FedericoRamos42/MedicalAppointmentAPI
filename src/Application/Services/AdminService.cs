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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IValidator<AdminCreateRequest> _createAdmin;
        private readonly IValidator<AdminUpdateRequest> _updateAdmin;

        public AdminService(IAdminRepository repository, IPasswordHasherService passwordHasherService,
                            IValidator<AdminCreateRequest> createAdmin, IValidator<AdminUpdateRequest> updateAdmin)
        {
            _repository = repository;
            _passwordHasherService = passwordHasherService;
            _createAdmin = createAdmin;
            _updateAdmin = updateAdmin;
        }
        public async Task<Result<AdminDto>> Create(AdminCreateRequest request)
        {
            var hashedPassword = _passwordHasherService.HashPassword(request.Password);
            var result = _createAdmin.Validate(request);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e=>e.ErrorMessage).ToList();
                return Result<AdminDto>.FailureModels(errors);
            }

            Admin admin = new Admin()
            {
                Name = request.Name,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Address = request.Address,
                Password = hashedPassword,
            };
            await _repository.AddAsync(admin);
            var dto = admin.ToDto();
            return Result<AdminDto>.Success(dto);
           
        }

        public async Task<Result<AdminDto>> Delete(int id)
        {
            var admin = await _repository.GetByIdAsync(id);
            if(admin is null)
            {
                return Result<AdminDto>.Failure($"Admin with id {id} does not exist");
            }

            admin.IsAvailable = false;
            await _repository.UpdateAsync(admin);
            var dto = admin.ToDto();
            return Result<AdminDto>.Success(dto);
        }

        public async Task<Result<IEnumerable<AdminDto>>> GetAll()
        {
           IEnumerable<Admin> list = await _repository.GetAllAsync();
           var dto = list.ToListDto();
           return Result<IEnumerable<AdminDto>>.Success(dto);
           
        }

        public async Task<Result<AdminDto>> GetById(int id)
        {
            Admin admin = await _repository.GetByIdAsync(id);
            if (admin is null)
            {
                return Result<AdminDto>.Failure($"Admin with id {id} does not exist");
            }
            var dto = admin.ToDto();
            return Result<AdminDto>.Success(dto);
        }

        public async Task<Result<AdminDto>> Update(int id , AdminUpdateRequest request)
        {
            Admin admin = await _repository.GetByIdAsync(id);

            if (admin is null)
            {
                return Result<AdminDto>.Failure($"Admin with id {id} does not exist");
            }

            var result = _updateAdmin.Validate(request);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<AdminDto>.FailureModels(errors);
            }

            admin.Name = request.Name;
            admin.LastName = request.LastName;
            admin.PhoneNumber = request.PhoneNumber;
            admin.Email = request.Email;
            admin.Address = request.Address;

            await _repository.UpdateAsync(admin);
            var dto = admin.ToDto();
            return Result<AdminDto>.Success(dto);

        }
    }
}
