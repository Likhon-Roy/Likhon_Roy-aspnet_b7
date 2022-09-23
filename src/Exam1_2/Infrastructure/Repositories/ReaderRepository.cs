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

        public (IList<Reader> data, int total, int totalDisplay) GetReaders(int pageIndex,
            int pageSize, string searchText, string orderby)
        {
            if(orderby == "" || orderby == null){
                orderby = null;
            }
            
            (IList<Reader> data, int total, int totalDisplay) results = 
                GetDynamic(x => x.Name.Contains(searchText), orderby,
                "", pageIndex, pageSize, true);

            return results;
        }
    }
}
