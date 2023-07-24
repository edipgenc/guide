using Guide.Book.Application.Dto.ContactsDto;
using Guide.Book.Application.Dto.ReportDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Data.Services.Interfaces
{
    public interface IContactService
    {
        
        Task<IActionResult> CreateAsync(CreateContactDto model);
        Task<IActionResult> UpdateAsync(UpdateContactDto model);
        Task<IActionResult> DeleteAsync(string Id);
        Task<IActionResult> GetAllAsync(string Id);
        Task<IActionResult> GetByIdAsync(string Id);
    }
}
