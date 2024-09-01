using Vezeeta.Models.Patients;

namespace Vezeeta.Service.Interface
{
    public interface IPatientRepository
    {
        public Task<string> Update(UpdatePatients patient);
        public Task<string> DeleteById(int id);
        public Task<string> Create(AddPatient patient);
        public Task<IEnumerable<GetAllPatients>> GetAllList();
        public Task<GetAllPatients> GetById(int id);
    }
}
