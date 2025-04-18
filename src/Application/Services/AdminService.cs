using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Request;
using Application.Result;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;
        public AdminService(IAdminRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<AdminDto>> Create(AdminCreateRequest request)
        {
            Admin admin = new Admin()
            {
                Name = request.Name,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Address = request.Address,
                Password = request.Password,
            };

            await _repository.AddAsync(admin);
            var dto = admin.ToDto();
            return Result<AdminDto>.Success(dto);
           
        }

        public async Task<Result<AdminDto>> Delete(int id)
        {
            Admin admin = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(admin);
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
            var dto = admin.ToDto();
            return Result<AdminDto>.Success(dto);
        }

        public async Task<Result<AdminDto>> Update(int id , AdminUpdateRequest request)
        {
            Admin admin = await _repository.GetByIdAsync(id);
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
