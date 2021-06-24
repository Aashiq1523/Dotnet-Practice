using Sample.Dto;
using System.Collections.Generic;

namespace Sample.Dao.Interface
{
    public interface IPersonDao
    {
        public int CreatePerson(Person person);
        public Person ReadPerson(int id);
        public IList<Person> ReadAllPerson();
        public bool UpdatePerson(int id, Person person);
        public bool DeletePerson(int id);
        public bool CreateMultiplePerson(IList<Person> persons);
    }
}
