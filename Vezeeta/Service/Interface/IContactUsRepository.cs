using Vezeeta.Models.Contact_us;

namespace Vezeeta.Service.Interface
{
    public interface IContactUsRepository
    {
        Task<string> AddContactUs(AddContactUsVM addContactUsVM);
        Task<IEnumerable<GetAllContactUsVm>> GetAllContactUs();
    }
}
