using AutoMapper;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Sample.Dao.Interface;
using Sample.Dto;
using Sample.Model;
using Sample.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sample.Service.Class
{
    public class PersonService
    {
        private static string UploadedFile;
        private readonly IPersonDao _personDao;
        private readonly IMapper _mapper;

        public PersonService(IPersonDao personDao, IMapper mapper)
        {
            _personDao = personDao;
            _mapper = mapper;
        }

        public int CreatePerson(PersonVM personVM)
        {
            Person person = _mapper.Map<PersonVM, Person>(personVM);
            return _personDao.CreatePerson(person);
        }

        public bool DeletePerson(int id)
        {
            return _personDao.DeletePerson(id);
        }

        public IList<PersonVM> ReadAllPerson()
        {
            IList<Person> persons = _personDao.ReadAllPerson();
            return _mapper.Map<IList<Person>, IList<PersonVM>>(persons);
        }

        public PersonVM ReadPerson(int id)
        {
            Person person = _personDao.ReadPerson(id);
            return _mapper.Map<Person, PersonVM>(person);
        }

        public bool UpdatePerson(int id, PersonVM personVM)
        {
            Person person = _mapper.Map<PersonVM, Person>(personVM);
            return _personDao.UpdatePerson(id, person);
        }

        public IList<PersonVM> ReadFile(IFormFile file)
        {
            IList<PersonVM> personVMs = new List<PersonVM>();
            if (UploadFile(file))
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (var stream = File.Open(UploadedFile, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        while (reader.Read())
                        {
                            PersonVM personVM = new PersonVM();
                            personVM.name = reader.GetValue(0).ToString();
                            personVM.address = new AddressVM();
                            personVM.address.street = reader.GetValue(1).ToString();
                            personVM.address.city = reader.GetValue(2).ToString();
                            personVM.address.postal_code = Convert.ToInt32(reader.GetValue(3));
                            personVMs.Add(personVM);
                        }
                    }
                }
            }
            return personVMs;
        }

        public bool CreateMultiplePerson(IList<PersonVM> personVMs)
        {
            IList<Person> persons = _mapper.Map<IList<PersonVM>, IList<Person>>(personVMs);
            return _personDao.CreateMultiplePerson(persons);
        }

        private bool UploadFile(IFormFile file)
        {
            return AppUtil.TryOut(AppError.FILE_UPLOAD_NOT_SUCCESS, () =>
            {
                AppError.FILE_UPLOAD_NOT_SUCCESS.message = "File Upload Not Success";

                string extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string newFileName = DateTime.Now.Ticks + extension;
                string pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "res\\files");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                UploadedFile = Path.Combine(Directory.GetCurrentDirectory(), "res\\files",
                   newFileName);

                using (FileStream stream = new FileStream(UploadedFile, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }
                return true;
            });
        }
    }
}
