using AutoMapper;
using Sample.Dao.Interface;
using Sample.Dto;
using Sample.Util;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Dao.Class
{
    public class PersonDao : IPersonDao
    {
        private readonly AppDBContext _appDbContext;
        private readonly IMapper _mapper;

        public PersonDao(AppDBContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public bool CreateMultiplePerson(IList<Person> persons)
        {
            return AppUtil.TryOut(AppError.MULTIPLE_PERSON_CREATE_FAILED, () =>
            {
                AppError.MULTIPLE_PERSON_CREATE_FAILED.message = "Persons creation failed";
                _appDbContext.person.AddRange(persons);
                _appDbContext.SaveChanges();
                return true;
            });
        }

        public int CreatePerson(Person person)
        {
            return AppUtil.TryOut(AppError.PERSON_CREATE_FAILED, () =>
            {
                AppError.PERSON_CREATE_FAILED.message = "Person creation failed";
                _appDbContext.person.Add(person);
                _appDbContext.SaveChanges();
                return person.id;
            });
        }

        public bool DeletePerson(int id)
        {
            return AppUtil.TryOut(AppError.PERSON_DELETE_FAILED, () =>
            {
                AppError.PERSON_DELETE_FAILED.message = "Person id " + id + " does not exist";
                Person personInDB = _appDbContext.person.FirstOrDefault(person => person.id == id);
                Address addressInDB = _appDbContext.address.FirstOrDefault(address => address.id == personInDB.addressid);
                if (personInDB != null && addressInDB != null)
                {
                    _appDbContext.person.Remove(personInDB);
                    _appDbContext.address.Remove(addressInDB);
                    _appDbContext.SaveChanges();
                }
                return true;
            });
        }

        public IList<Person> ReadAllPerson()
        {
            return AppUtil.TryOut(AppError.PERSON_READ_ALL_FAILED, () =>
            {
                AppError.PERSON_READ_ALL_FAILED.message = "Persons does not exists";
                // IList<Person> persons = _appDbContext.person.Include(p => p.addressid).ToList();
                IList<Person> persons = _appDbContext.person.ToList();
                IList<Address> addresses = _appDbContext.address.ToList();
                IList<Person> personsWithAddress = new List<Person>();
                foreach (var (person, address) in persons.SelectMany(person => addresses.Where(address => person.addressid == address.id).Select(address => (person, address))))
                {
                    person.address = address;
                    personsWithAddress.Add(person);
                }
                return personsWithAddress;
                // return persons;
            });
        }

        public Person ReadPerson(int id)
        {
            return AppUtil.TryOut(AppError.PERSON_READ_FAILED, () =>
            {
                AppError.PERSON_READ_FAILED.message = "Person id " + id + " does not exist";
                Person person = _appDbContext.person.FirstOrDefault(person => person.id == id);
                Address address = _appDbContext.address.FirstOrDefault(address => address.id == person.addressid);
                person.address = address;
                return person;
            });
        }

        public bool UpdatePerson(int id, Person person)
        {
            return AppUtil.TryOut(AppError.PERSON_UPDATE_FAILED, () =>
            {
                AppError.PERSON_UPDATE_FAILED.message = "Person id " + id + " does not exist";
                Person personInDB = _appDbContext.person.FirstOrDefault(person => person.id == id);
                Address addressInDB = _appDbContext.address.FirstOrDefault(address => address.id == personInDB.addressid);
                if (person != null)
                {
                    personInDB.name = person.name;
                    addressInDB.street = person.address.street;
                    addressInDB.city = person.address.city;
                    addressInDB.postal_code = person.address.postal_code;
                    _appDbContext.SaveChanges();
                }
                return true;
            });
        }
    }
}
