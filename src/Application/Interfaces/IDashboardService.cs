using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Result;

namespace Application.Interfaces
{
    public interface IDashboardService
    {
        Task<Result<DashboardAdminDto>> GetAdminDashboard();
        Task<Result<DashboardDoctorDto>> GetDoctorDashboard(int doctorId);

    }
}
