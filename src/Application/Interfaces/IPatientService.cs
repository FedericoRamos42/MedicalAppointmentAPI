using Application.Models.Request;
using Application.Models;
using Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions;

namespace Application.Interfaces
{
    public interface IPatientService
    {
        Task<Result<PatientDto>> GetById(int id);
        Task<Result<IEnumerable<PatientDto>>> GetAll();
        Task<Result<PatientDto>> Create(PatientCreateRequest request);
        Task<Result<PatientDto>> Update(int id, PatientUpdateRequest request);
        Task<Result<PatientDto>> Delete(int id);
        Task<Result<PaginatedList<PatientDto>>> GetPaginated(int pageIndex, int pageSize);
        Result<List<string>> GetMedicalInsaurances();

    }
}
