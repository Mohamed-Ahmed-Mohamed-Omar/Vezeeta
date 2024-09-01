using AutoMapper;
using Vezeeta.Data.Entities;
using Vezeeta.Models.Contact_us;
using Vezeeta.Models.Patients;

namespace Vezeeta.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Contact_us Setting

            CreateMap<Contact_us, GetAllContactUsVm>().ReverseMap();

            CreateMap<AddContactUsVM, Contact_us>().ReverseMap();

            #endregion

            #region Patients Setting

            CreateMap<AddPatient,Patient>().ReverseMap();

            CreateMap<GetAllPatients,Patient>().ReverseMap();

            CreateMap<UpdatePatients,Patient>().ReverseMap();

            #endregion
        }
    }
}
