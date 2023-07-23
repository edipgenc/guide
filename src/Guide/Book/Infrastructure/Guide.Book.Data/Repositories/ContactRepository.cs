using Guide.Book.Data.Repositories.Interfaces;
using Guide.Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Data.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
    }
}
