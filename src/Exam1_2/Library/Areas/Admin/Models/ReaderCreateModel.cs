using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace Library.Areas.Admin.Models
{
    public class ReaderCreateModel
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Proffetion { get; set; }

        private IReaderService _readerService;
        private ILifetimeScope _scope;


        public ReaderCreateModel()
        {

        }

        public ReaderCreateModel(IReaderService readerService)
        {
            _readerService = readerService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _readerService = _scope.Resolve<IReaderService>();
        }

        internal async Task CreateReader()
        {
            Reader reader = new Reader();
            reader.Name = Name;
            reader.Address = Address;
            reader.Proffetion = Proffetion;

            _readerService.CreateReader(reader);
        }
    }
}
