﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class SpecialtyRepository : BaseRepository<Specialty>, ISpecialtyRepository
    {
        private readonly ApplicationDbContext _context;
        public SpecialtyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }  
}
