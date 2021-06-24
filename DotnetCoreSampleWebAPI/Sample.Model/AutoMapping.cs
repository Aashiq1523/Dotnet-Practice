using AutoMapper;
using Sample.Dto;

namespace Sample.Model
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<PersonVM, Person>();
            CreateMap<Person, PersonVM>();
            CreateMap<AddressVM, Address>();
            CreateMap<Address, AddressVM>();
        }
    }
}
