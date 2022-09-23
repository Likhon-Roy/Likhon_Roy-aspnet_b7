using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public interface IReaderRepository : IRepository<Reader, Guid>
    {
        (IList<Reader> data, int total, int totalDisplay) GetReaders(int pageIndex,
            int pageSize, string searchText, string orderby);
    }
}