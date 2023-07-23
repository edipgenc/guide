using Guide.Book.Application.Dto.ContactsDto.PersonsDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Data.Services.Interfaces
{
    public interface IPersonService
    {
        Task<IActionResult> CreateAsync(CreatePersonDto model);
        Task<IActionResult> UpdateAsync(UpdatePersonDto model);
        Task<IActionResult> DeleteAsync(string Id);
        Task<IActionResult> GetAllAsync();
        Task<IActionResult> GetByIdAsync(string Id);
    }

    
}
