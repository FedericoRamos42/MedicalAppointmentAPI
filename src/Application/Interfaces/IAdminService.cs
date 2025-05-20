using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Models.Request;
using Application.Result;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<Result<AdminDto>> GetById(int id);
        Task<Result<IEnumerable<AdminDto>>> GetAll();
        Task<Result<AdminDto>> Create(AdminCreateRequest request);
        Task<Result<AdminDto>> Update(int id,AdminUpdateRequest request);
        Task<Result<AdminDto>> Delete(int id);
    }
}
