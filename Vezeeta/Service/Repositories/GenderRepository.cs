using Microsoft.EntityFrameworkCore;
using Vezeeta.Data;
using Vezeeta.Data.Entities;
using Vezeeta.Service.Interface;

namespace Vezeeta.Service.Repositories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly AppDbContext _context;

        public GenderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gender>> GetAll()
        {
            var data = await _context.genders.ToListAsync();

            return data;
        }
    }
}
