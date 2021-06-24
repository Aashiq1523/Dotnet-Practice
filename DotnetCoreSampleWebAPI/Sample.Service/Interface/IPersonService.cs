using Microsoft.AspNetCore.Http;
using Sample.Model;
using System.Collections.Generic;

namespace Sample.Service.Interface
{
    public interface IPersonService
    {
        public int CreatePerson(PersonVM personVM);
        public PersonVM ReadPerson(int id);
        public IList<PersonVM> ReadAllPerson();
        public bool UpdatePerson(int id, PersonVM personVM);
        public bool DeletePerson(int id);
        public IList<PersonVM> ReadFile(IFormFile file);
        public bool CreateMultiplePerson(IList<PersonVM> personVMs);
    }
}
