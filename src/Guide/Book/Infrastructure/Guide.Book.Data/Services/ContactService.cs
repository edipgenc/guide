using AutoMapper;
using FluentValidation;
using Guide.Book.Application.Dto.CommonDto;
using Guide.Book.Application.Dto.ContactsDto;
using Guide.Book.Application.Dto.ReportDto;
using Guide.Book.Data.Repositories.Interfaces;
using Guide.Book.Data.Services.Interfaces;
using Guide.Book.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Data.Services
{
    public class ContactService  : ControllerBase,IContactService
    {
        private readonly ILogger<ContactService> _logger;
        private readonly IPersonRepository _personRepository;
        private readonly IContactRepository _contactRepository;

        private readonly IValidator<CreateContactDto> _validator;
        private readonly IMapper _mapper;
        public ContactService(IPersonRepository personRepository, IContactRepository contactRepository, IValidator<CreateContactDto> validator, IMapper mapper, ILogger<ContactService> logger)
        {
            _personRepository = personRepository;
            _contactRepository = contactRepository;

            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }


        public async Task<IActionResult> CreateAsync(CreateContactDto model)
        {
            try
            {
                var result = await _validator.ValidateAsync(model);

                if (result.IsValid)
                {
                    Person? person = await _personRepository.GetById(model.PersonId);
                    if (person != null)
                    {
                        Contact _contact = _mapper.Map<Contact>(model);
                        _contact.PersonId = person.Id;
                        var data = await _contactRepository.Create(_contact);
                        return Ok(data);
                    }
                    else
                    {
                        return BadRequest(new ErrorModel() { Message = "Please check your person Id!", Detail = "Person Id is not valid" });
                    }
                }

                return ValidationError("Contact data is invalid", result);


            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorModel() { Message = "Guide Book has been error please try again after a while", Detail = ex.Message });
            }

        }

        public async Task<IActionResult> DeleteAsync(string Id)
        {
            try
            {
                var data = await _contactRepository.Delete(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorModel() { Message = "Guide Book has been error please try again after a while", Detail = ex.Message });
            }
        }

        public async Task<IActionResult> GetAllAsync(string Id)
        {
            try
            {
                var data = await _contactRepository.GetAll();
                Guid PersonId = Guid.Empty;
                PersonId = Guid.Parse(Id);
                var filtredData = data.Where(p => p.PersonId == PersonId).ToList();
                return Ok(filtredData);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorModel() { Message = "Guide Book has been error please try again after a while", Detail = ex.Message });
            }
        }

        public async Task<IActionResult> GetByIdAsync(string Id)
        {
            try
            {
                var data = await _contactRepository.GetById(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorModel() { Message = "Guide Book has been error please try again after a while", Detail = ex.Message });
            }
        }

        public async Task<IActionResult> UpdateAsync(UpdateContactDto model)
        {
            try
            {
                var result = await _validator.ValidateAsync(model);

                if (result.IsValid)
                {
                    Person? person = await _personRepository.GetById(model.PersonId);

                    if (person != null)
                    {
                        Contact _contact = _mapper.Map<Contact>(model);
                        _contact.PersonId = person.Id;
                        var data = await _contactRepository.Update(_contact);
                        return Ok(data);

                    }
                    else
                    {
                        return BadRequest(new ErrorModel() { Message = "Please check your person Id!", Detail = "Person Id is not valid" });
                    }

                }

                return ValidationError("Contact data is invalid", result);



            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorModel() { Message = "Guide Book has been error please try again after a while", Detail = ex.Message });
            }
        }

        private IActionResult ValidationError(string messageTitle, FluentValidation.Results.ValidationResult result)
        {
            var errorDetailList = new List<string>();
            foreach (var item in result.Errors)
            {
                errorDetailList.Add(item.ErrorMessage);
            };
            string errorList = "";
            foreach (var item in errorDetailList)
            {
                errorList += item + ", ";
            }

            return BadRequest(new ErrorModel()
            {
                Message = messageTitle,
                Detail = errorList
            });
        }

    }
}
