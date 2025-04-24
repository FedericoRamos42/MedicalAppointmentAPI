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
    public interface IDoctorService
    {
        Task<Result<DoctorDto>> GetById(int id);
        Task<Result<IEnumerable<DoctorDto>>> GetAll();
        Task<Result<DoctorDto>> Create(DoctorCreateRequest request);
        Task<Result<DoctorDto>> Update(int id, DoctorUpdateRequest request);
        Task<Result<DoctorDto>> Delete(int id);
    }
}
