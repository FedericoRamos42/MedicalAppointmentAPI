using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Models.Request;
using Application.Result;
using Domain.Abstractions;

namespace Application.Interfaces
{
    public interface IAvailabilityService
    {
        Task<Result<AvailabilityDto>> Create(AvailabilityCreateRequest request);
        Task<Result<AvailabilityDto>> Update(int id,AvailabilityUpdateRequest request);
        Task<Result<AvailabilityDto>> Delete(int id);
        Task<Result<PaginatedList<AvailabilityDto>>> GetPaginated(int pageIndex, int pageSize);
    }
}
