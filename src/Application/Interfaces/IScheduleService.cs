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
    public interface IScheduleService
    {
        Task<Result<ScheduleDto>> Create(ScheduleCreateRequest request);
        Task<Result<IEnumerable<ScheduleDto>>> GetByDoctorAndDate(int DoctorId,DateTime date);
        Task<Result<ScheduleDto>> Delete(int Id);

    }
}
