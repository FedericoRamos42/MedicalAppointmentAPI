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
    public interface IMedicalHistoryService
    {
        Task<Result<MedicalHistoryDto>> GetById(int id);
        Task<Result<IEnumerable<MedicalHistoryDto>>> GetAll();
        Task<Result<MedicalHistoryDto>> Create(MedicalHistoryCreateRequest request);
        Task<Result<MedicalHistoryDto>> Delete(int id);
    }
}
