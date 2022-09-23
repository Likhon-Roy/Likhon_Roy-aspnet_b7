using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public interface IBookRepository : IRepository<Book, Guid>
    {

    }
}