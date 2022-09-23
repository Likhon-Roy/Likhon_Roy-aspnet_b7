using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Services;
using Library.Models;

namespace Library.Areas.Member.Models
{
    public class BookListModel: BaseModel
	{
        private IBookService? _bookService;

        public BookListModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _bookService = _scope.Resolve<IBookService>();
        }

        internal object? GetPagedBooks(DataTablesAjaxRequestModel model)
		{
            
            var data = _bookService.GetBooks(
                model.PageIndex,
                model.PageSize,
                model.SearchText,
                model.GetSortText(new string[] { "Name", "AutherName", "Publisher" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.AutherName,
                                record.Publisher,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
	}
}
