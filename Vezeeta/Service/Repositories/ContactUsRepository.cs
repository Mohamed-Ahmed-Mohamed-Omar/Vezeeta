using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Data;
using Vezeeta.Data.Entities;
using Vezeeta.Models.Contact_us;
using Vezeeta.Service.Interface;

namespace Vezeeta.Service.Repositories
{
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContactUsRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> AddContactUs(AddContactUsVM addContactUsVM)
        {
            try
            {
                var contactUs = _mapper.Map<Contact_us>(addContactUsVM);

                await _context.contact_Us.AddAsync(contactUs);

                await _context.SaveChangesAsync();

                return "Contact Us Added Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IEnumerable<GetAllContactUsVm>> GetAllContactUs()
        {
            var contactUs = await _context.contact_Us.ToListAsync();

            return _mapper.Map<IEnumerable<GetAllContactUsVm>>(contactUs);
        }

    }
}
