using Vezeeta.Data.Entities;

namespace Vezeeta.Service.Interface
{
    public interface IAreaRepository
    {
        public Task<IEnumerable<Area>> GetAll();
    }
}
