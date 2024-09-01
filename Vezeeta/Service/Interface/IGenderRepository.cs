using Vezeeta.Data.Entities;

namespace Vezeeta.Service.Interface
{
    public interface IGenderRepository
    {
        public Task<IEnumerable<Gender>> GetAll();
    }
}
