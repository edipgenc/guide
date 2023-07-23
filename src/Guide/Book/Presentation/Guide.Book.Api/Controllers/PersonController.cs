using Guide.Book.Application.Dto.ContactsDto.PersonsDto;
using Guide.Book.Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Guide.Book.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        /// <summary>
        /// This methot returns Person list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPersonList")]
        public async Task<IActionResult> GetPersonList()
        {
            return await _personService.GetAllAsync();
        }
        /// <summary>
        /// This methot returns Person record
        /// </summary>
        /// <param name="Id"> Person Id </param> 
        /// <returns></returns>
        [HttpGet]
        [Route("GetPerson")]
        public async Task<IActionResult> GetPerson(string Id)
        {
            return await _personService.GetByIdAsync(Id);
        }


        /// <summary>
        /// This methot returns truo or false, if the process is success returns true
        /// </summary>
        /// <param name="Id"> Person Id </param> 
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            return await _personService.DeleteAsync(Id);
        }

        /// <summary>
        /// This methot returns truo or false, if the process is success returns true
        /// </summary>
        /// <param name="Id"> Person Id </param> 
        /// <param name="model"> Person Model Data</param> 
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdatePersonDto model)
        {
            return await _personService.UpdateAsync(model);
        }

        /// <summary>
        /// This methot returns the new added person Id
        /// </summary>
        /// <param name="model"> Person Model Data</param> 
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(CreatePersonDto model)
        {
            return await _personService.CreateAsync(model);

        }
    }
}
