using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DbContexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : Repository<Book, Guid>, IBookRepository
    {
        public BookRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public (IList<Book> data, int total, int totalDisplay) GetBooks(int pageIndex,
            int pageSize, string searchText, string orderby)
        {
            if(orderby == "" || orderby == null){
                orderby = null;
            }

            (IList<Book> data, int total, int totalDisplay) results = 
                GetDynamic(x => x.Name.Contains(searchText), orderby,
                "", pageIndex, pageSize, true);

            return results;
        }
    }
}
