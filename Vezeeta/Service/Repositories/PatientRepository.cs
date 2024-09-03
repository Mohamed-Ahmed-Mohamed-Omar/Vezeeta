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
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public PatientRepository(AppDbContext context, IMapper mapper, IFileRepository fileRepository, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _fileRepository = fileRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<string> Create(AddPatient patient)
        {
            try
            {
                var data = _mapper.Map<Patient>(patient);

                // Handle Report Upload
                if (patient.Report_Url != null && patient.Report_Url.Length > 0)
                {
                    var context = _contextAccessor.HttpContext.Request;

                    var baseUrl = context.Scheme + "://" + context.Host;

                    var report = await _fileRepository.UploadFile("Files/Reports", patient.Report_Url);

                    data.Report = baseUrl+report;

                    if (data.Report == "InvalidFile" || data.Report.Contains("FailedToUploadImage"))
                    {
                        return data.Report; // Return error message
                    }
                }

                // Handle Image Upload
                if (patient.Image_Url != null && patient.Image_Url.Length > 0)
                {
                    var context = _contextAccessor.HttpContext.Request;

                    var baseUrl = context.Scheme + "://" + context.Host;

                    var image = await _fileRepository.UploadFile("Files/Images", patient.Image_Url);

                    data.Image = baseUrl + image;

                    if (data.Image == "InvalidFile" || data.Image.Contains("FailedToUploadImage"))
                    {
                        return data.Image; // Return error message
                    }
                }

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

                await _fileRepository.RemoveFile("Images", delData.Image);

                await _fileRepository.RemoveFile("Reports", delData.Report);

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
                GenId = s.GenId,
                Image = s.Image,
                Report = s.Report
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
                GenId = s.GenId,
                AreaId = s.AreaId,
                Image = s.Image,
                Report = s.Report
            }).FirstOrDefaultAsync();

            return data;
        }

        public async Task<string> Update(UpdatePatients patient)
        {
            try
            {
                var data = await _context.patients.FindAsync(patient.Id);

                if (data == null)
                {
                    return "Patient not found";
                }

                // Map the patient object to the existing data
                _mapper.Map(patient, data);

                var context = _contextAccessor.HttpContext.Request;
                var baseUrl = context.Scheme + "://" + context.Host;

                // Handle Report Upload
                if (patient.Report_Url != null && patient.Report_Url.Length > 0)
                {
                    // Delete the old report if it exists
                    if (!string.IsNullOrEmpty(data.Report))
                    {
                        var oldReportPath = data.Report.Replace(baseUrl, ""); // Extract the file path from the URL
                        await _fileRepository.RemoveFile("Files/Reports", oldReportPath);
                    }

                    var report = await _fileRepository.UploadFile("Files/Reports", patient.Report_Url);
                    data.Report = baseUrl + report;

                    if (data.Report == "InvalidFile" || data.Report.Contains("FailedToUploadImage"))
                    {
                        return data.Report; // Return error message
                    }
                }

                // Handle Image Upload
                if (patient.Image_Url != null && patient.Image_Url.Length > 0)
                {
                    // Delete the old image if it exists
                    if (!string.IsNullOrEmpty(data.Image))
                    {
                        var oldImagePath = data.Image.Replace(baseUrl, ""); // Extract the file path from the URL
                        await _fileRepository.RemoveFile("Files/Images", oldImagePath);
                    }

                    var image = await _fileRepository.UploadFile("Files/Images", patient.Image_Url);
                    data.Image = baseUrl + image;

                    if (data.Image == "InvalidFile" || data.Image.Contains("FailedToUploadImage"))
                    {
                        return data.Image; // Return error message
                    }
                }

                _context.patients.Update(data);
                await _context.SaveChangesAsync();

                return "Update Done";
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return "Failed Update";
            }
        }
    }
}
