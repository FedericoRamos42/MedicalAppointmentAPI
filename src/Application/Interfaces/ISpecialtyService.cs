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
    public interface ISpecialtyService
    {
        Task<Result<SpecialtyDto>> GetById(int id);
        Task<Result<IEnumerable<SpecialtyDto>>> GetAll();
        Task<Result<SpecialtyDto>> Create(SpecialtyCreateRequest request);
        Task<Result<SpecialtyDto>> Delete(int id);
        Task<Result<SpecialtyDto>> Update(int id, SpecialtyUpdateRequest request);
    }
}
