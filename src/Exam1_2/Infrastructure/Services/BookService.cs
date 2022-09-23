using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.UnitOfWorks;

using BookBO = Infrastructure.BusinessObjects.Book;
using BookEO = Infrastructure.Entities.Book;

namespace Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;

        public BookService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public void CreateBook(BookBO book)
        {
            BookEO bookEntity = new BookEO();
            bookEntity.Name = book.Name;
            bookEntity.AutherName = book.AutherName;
            bookEntity.Publisher = book.Publisher;

            _applicationUnitOfWork.Books.Add(bookEntity);
            _applicationUnitOfWork.Save();
        }

        public (int total, int totalDisplay, IList<BookBO> records) GetBooks(int pageIndex,
            int pageSize, string searchText, string orderby)
        {
            (IList<BookEO> data, int total, int totalDisplay) results = _applicationUnitOfWork
                .Books.GetBooks(pageIndex, pageSize, searchText, orderby);

            IList<BookBO> books = new List<BookBO>();
            foreach (BookEO book in results.data)
            {
                books.Add(new BookBO
                {
                    Id = book.Id,
                    Name = book.Name,
                    AutherName = book.AutherName,
                    Publisher = book.Publisher
                });
            }

            return (results.total, results.totalDisplay, books);
        }
    }
}
