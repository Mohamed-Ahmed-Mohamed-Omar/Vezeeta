using Microsoft.EntityFrameworkCore;
using Vezeeta.Data;
using Vezeeta.Data.Entities;
using Vezeeta.Service.Interface;

namespace Vezeeta.Service.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly AppDbContext _context;

        public AreaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Area>> GetAll()
        {
            var data = await _context.areas.ToListAsync();

            return data;
        }
    }
}
