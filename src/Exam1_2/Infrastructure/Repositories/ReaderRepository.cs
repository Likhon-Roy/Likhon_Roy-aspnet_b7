using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DbContexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReaderRepository : Repository<Reader, Guid>, IReaderRepository
    {
        public ReaderRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }
    }
}
