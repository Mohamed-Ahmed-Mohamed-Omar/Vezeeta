using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Data;
using Vezeeta.Data.Entities;
using Vezeeta.Models.Patients;
using Vezeeta.Service.Interface;

namespace Vezeeta.Service.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PatientRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> Create(AddPatient patient)
        {
            try
            {
                var data = _mapper.Map<Patient>(patient);

                await _context.patients.AddAsync(data);

                await _context.SaveChangesAsync();

                return "Patient Added Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteById(int id)
        {
            var delData = await _context.patients.FindAsync(id);

            if(delData != null)
            {
                _context.patients.Remove(delData);

                await _context.SaveChangesAsync();

                return "Deleye Done";
            }

            return "Faild Delete";
        }

        public async Task<IEnumerable<GetAllPatients>> GetAllList()
        {
            var data = await _context.patients.Select(s=> new GetAllPatients
            {
                Id = s.Id,
                Name = s.Name,
                Area = s.Area.AreaName,
                Date = s.Date,
                Email = s.Email,
                phone = s.phone,
                Gender = s.Gender.GenderName
            }).ToListAsync();

            return data;
        }

        public async Task<GetAllPatients> GetById(int id)
        {
            var data = await _context.patients.Where(w=>w.Id == id).Select(s => new GetAllPatients
            {
                Name = s.Name,
                Area = s.Area.AreaName,
                Date = s.Date,
                Email = s.Email,
                phone = s.phone,
                Gender = s.Gender.GenderName
            }).FirstOrDefaultAsync();

            return data;
        }

        public async Task<string> Update(UpdatePatients patient)
        {
            try
            {
                var data = await _context.patients.FindAsync(patient.Id);

                _mapper.Map(patient, data);

                _context.patients.Update(data);

                await _context.SaveChangesAsync();

                return "Update Done";
            }
            catch (Exception ex)
            {
                return "Faild Update";
            }
        }
    }
}
