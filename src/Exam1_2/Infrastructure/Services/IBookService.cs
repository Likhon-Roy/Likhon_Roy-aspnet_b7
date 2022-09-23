using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.BusinessObjects;

namespace Infrastructure.Services
{
    public interface IBookService
    {
        void CreateBook(Book book);

        (int total, int totalDisplay, IList<Book> records) GetBooks(int pageIndex, int pageSize, string searchText, string orderby);

    }
}