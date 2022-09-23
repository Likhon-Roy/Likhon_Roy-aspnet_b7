using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace Library.Areas.Admin.Models
{
    public class BookCreateModel
    {
        public string? Name { get; set; }
        public string? AutherName { get; set; }
        public string? Publisher { get; set; }

        private IBookService _bookService;
        private ILifetimeScope _scope;


        public BookCreateModel()
        {

        }

        public BookCreateModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _bookService = _scope.Resolve<IBookService>();
        }

        internal async Task CreateBook()
        {
            Book book = new Book();
            book.Name = Name;
            book.AutherName = AutherName;
            book.Publisher = Publisher;

            _bookService.CreateBook(book);
        }
    }
}
