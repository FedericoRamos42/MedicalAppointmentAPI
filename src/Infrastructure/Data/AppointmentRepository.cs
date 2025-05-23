﻿using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        { 
          _context = context;   
        }

        public async Task<List<Appointment>> GetAppointmentsbyDateAndDoctor(DateTime date, int DoctorId)
        {
            var appointments = await _context.Appointments
                                                .Where(a => a.Date.Date == date.Date && a.DoctorId == DoctorId)
                                                .ToListAsync();
            return appointments;
        }
       
       
    }
}
