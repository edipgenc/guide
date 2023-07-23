using AutoMapper;
using FluentValidation;
using Guide.Book.Application.Dto.CommonDto;
using Guide.Book.Application.Dto.ContactsDto.PersonsDto;
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
    public class PersonService : ControllerBase,IPersonService
    {
        private readonly ILogger<PersonService> _logger;
        private readonly IPersonRepository _personRepository;
        private readonly IValidator<CreatePersonDto> _validator;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IValidator<CreatePersonDto> validator, IMapper mapper, ILogger<PersonService> logger)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<IActionResult> CreateAsync(CreatePersonDto model)
        {
            try
            {
                var result = await _validator.ValidateAsync(model);
                if (result.IsValid)
                {
                    Person _person = _mapper.Map<Person>(model);
                    var data = await _personRepository.Create(_person);
                    return Ok(data);

                }
                return ValidationError("Person data is invalid", result);
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
                var data = await _personRepository.Delete(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorModel() { Message = "Guide Book has been error please try again after a while", Detail = ex.Message });
            }
        }

        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var data = await _personRepository.GetAll();
                return Ok(data);
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
                var data = await _personRepository.GetById(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorModel() { Message = "Guide Book has been error please try again after a while", Detail = ex.Message });
            }
        }

        public async Task<IActionResult> UpdateAsync(UpdatePersonDto model)
        {
            try
            {
                var result = await _validator.ValidateAsync(model);
                if (result.IsValid)
                {
                    Guid objGuid = Guid.Empty;
                    objGuid = Guid.Parse(model.Id);
                    Person _person = _mapper.Map<Person>(model);
                    _person.Id = objGuid;

                    var data = await _personRepository.Update(_person);
                    return Ok(data);
                }
                return ValidationError("Person data is invalid", result);
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
