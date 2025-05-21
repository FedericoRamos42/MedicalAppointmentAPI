using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Models.Request;
using Application.Models.Response;
using Application.Result;
using Domain.Abstractions;

namespace Application.Interfaces
{
    public interface IDoctorService
    {
        Task<Result<DoctorDto>> GetById(int id);
        Task<Result<IEnumerable<DoctorDto>>> GetAll();
        Task<Result<DoctorDto>> Create(DoctorCreateRequest request);
        Task<Result<DoctorDto>> Update(int id, DoctorUpdateRequest request);
        Task<Result<DoctorDto>> Delete(int id);
        Task<Result<DoctorResponse>> GetWithAvailabilities(int id);
        Task<Result<PaginatedList<DoctorDto>>> GetPaginated(int pageIndex
            , int pageSize);
    }
}
