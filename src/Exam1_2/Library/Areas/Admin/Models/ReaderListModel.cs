using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Services;
using Library.Models;

namespace Library.Areas.Admin.Models
{
    public class ReaderListModel : BaseModel
    {
        private IReaderService? _readerService;

        public ReaderListModel(IReaderService readerService)
        {
            _readerService = readerService;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _readerService = _scope.Resolve<IReaderService>();
        }

        internal object? GetPagedBooks(DataTablesAjaxRequestModel model)
        {

            var data = _readerService.GetReaders(
                model.PageIndex,
                model.PageSize,
                model.SearchText,
                model.GetSortText(new string[] { "Name", "Address", "Proffetion" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Address,
                                record.Proffetion,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
