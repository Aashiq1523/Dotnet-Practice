using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sample.Model;
using Sample.Service.Interface;
using Sample.Util;
using System.Collections.Generic;

namespace Sample.Web.Controllers
{
    [Route("api/v1/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPersonService _personService;

        public PersonController(IConfiguration configuration, IPersonService personService)
        {
            _configuration = configuration;
            _personService = personService;
        }

        [HttpPost("upload-file")]
        public IActionResult UploadFile(IFormFile file)
        {
            return AppUtil.PrepareIActionResult(200, () =>
            {
                AppUtil.TryOut(AppError.BAD_REQUEST, () =>
                {
                    if (file == null)
                    {
                        AppError.FILE_NOT_PROVIDED.message = "No File is uploaded. Please Upload a file";
                        throw new AppException(AppError.FILE_NOT_PROVIDED);
                    }
                    if (!CheckIfExcelFile(file))
                    {
                        AppError.FILE_FORMAT_NOT_SUPPORTED.message = "File format not supported. Supported Format .xls or .xlsx";
                        throw new AppException(AppError.FILE_FORMAT_NOT_SUPPORTED);
                    }
                });
                return _personService.ReadFile(file);
            });
        }

        [HttpPost("add-persons")]
        public IActionResult CreatePersons(IList<PersonVM> personVMs)
        {
            return AppUtil.PrepareIActionResult(201, () =>
            {
                AppUtil.TryOut(AppError.BAD_REQUEST, () =>
                {
                    if (personVMs == null)
                    {
                        AppError.PERSONS_NOT_FOUND.message = "Persons not found. Please provide value for persons";
                        throw new AppException(AppError.PERSONS_NOT_FOUND);
                    }
                });
                return _personService.CreateMultiplePerson(personVMs);
            });
        }

        [HttpPost("add-person")]
        public IActionResult CreatePerson(PersonVM personVM)
        {
            return AppUtil.PrepareIActionResult(201, () =>
            {
                AppUtil.TryOut(AppError.BAD_REQUEST, () => {
                    personVM.NullOrEmptyValidation();
                });
                return _personService.CreatePerson(personVM);
            });
        }

        [HttpGet("get-person-{id}")]
        public IActionResult GetPerson(int id)
        {
            return AppUtil.PrepareIActionResult(200, () =>
            {
                AppUtil.TryOut(AppError.BAD_REQUEST, () =>
                {
                    if (id == 0)
                    {
                        AppError.ID_CANNOT_BE_EMPTY.message = "Field id cannot be empty or zero. Please provide a valid id value";
                        throw new AppException(AppError.ID_CANNOT_BE_EMPTY);
                    }
                });
                return _personService.ReadPerson(id);
            });
        }

        [Authorize(Roles = "True")]
        [HttpGet("get-all-person")]
        public IActionResult GetAllPerson()
        {
            // works only in local
            //EmailService emailService = new EmailService();
            //IList<string> mailList = new List<string>();
            //mailList.Add("soffiashraf486@gmail.com");
            //mailList.Add("aashiqui1086@gmail.com");
            //emailService.CreateMessage(mailList, "Mail Check", "Alert Mail", _configuration["FromMail"]);
            //emailService.SendMail(_configuration["Host"], Convert.ToInt32(_configuration["Port"]), _configuration["FromMail"], _configuration["Password"]);
            return AppUtil.PrepareIActionResult(200, () => _personService.ReadAllPerson());
        }

        [HttpPut("update-person-{id}")]
        public IActionResult UpdatePerson(int id, PersonVM personVM)
        {
            return AppUtil.PrepareIActionResult(200, () =>
            {
                AppUtil.TryOut(AppError.BAD_REQUEST, () => {
                    if (id == 0)
                    {
                        AppError.ID_CANNOT_BE_EMPTY.message = "Field id cannot be empty or zero. Please provide a valid id value";
                        throw new AppException(AppError.ID_CANNOT_BE_EMPTY);
                    }
                    personVM.NullOrEmptyValidation();
                });
                return _personService.UpdatePerson(id, personVM);
            });
        }

        [HttpDelete("delete-person-{id}")]
        public IActionResult DeletePerson(int id)
        {
            return AppUtil.PrepareIActionResult(200, () =>
            {
                AppUtil.TryOut(AppError.BAD_REQUEST, () =>
                {
                    if (id == 0)
                    {
                        AppError.ID_CANNOT_BE_EMPTY.message = "Field id cannot be empty or zero. Please provide a valid id value";
                        throw new AppException(AppError.ID_CANNOT_BE_EMPTY);
                    }
                });
                return _personService.DeletePerson(id);
            });
        }

        private bool CheckIfExcelFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".xlsx" || extension == ".xls"); // Change the extension based on your need
        }
    }
}
