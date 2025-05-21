using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utils
{
    public static class IQueryableExtensions
    {
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(
        this IQueryable<T> query,
        int pageIndex,
        int pageSize) where T : class
        {
            var count = await query.CountAsync();
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            return new PaginatedList<T>(items, pageIndex, totalPages);
        }

    }
}
