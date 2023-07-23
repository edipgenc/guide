using Guide.Book.Application.Dto.CommonDto;
using Guide.Book.Application.Dto.ContactsDto;
using Guide.Book.Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Guide.Book.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactService;
 
        public ContactController(IContactService contactService,ILogger<ContactController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        /// <summary>
        /// This methot returns Person's Contact list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPersonContactList")]
        public async Task<IActionResult> GetPersonContactList(string Id)
        {         
          var data = await _contactService.GetAllAsync(Id);
          return data;   
        }
        /// <summary>
        /// This methot returns Person record
        /// </summary>
        /// <param name="Id"> Person Id </param> 
        /// <returns></returns>
        [HttpGet]
        [Route("GetPersonContact")]
        public async Task<IActionResult> GetPersonContact(string Id)
        {
            var data = await _contactService.GetByIdAsync(Id);
            return data;
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
            var data = await _contactService.DeleteAsync(Id);
            return data;
        }

        /// <summary>
        /// This methot returns truo or false, if the process is success returns true
        /// </summary>
        /// <param name="model"> Person Model Data</param> 
        /// <returns></returns>
        [HttpPut()]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateContactDto model)
        {
            var data = await _contactService.UpdateAsync(model);
            return data;
        }

        /// <summary>
        /// This methot returns the new added contact Id
        /// </summary>
        /// <param name="model"> Person Model Data</param> 
        /// <returns></returns>
        [HttpPost()]
        [Route("Add")]
        public async Task<IActionResult> Add(CreateContactDto model)
        {
            var data = await _contactService.CreateAsync(model);
            return data;
        }


    }
}
