using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.UnitOfWorks;
using ReaderBO = Infrastructure.BusinessObjects.Reader;
using ReaderEO = Infrastructure.Entities.Reader;

namespace Infrastructure.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;

        public ReaderService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public void CreateReader(ReaderBO reader)
        {
            ReaderEO readerEntity = new ReaderEO();
            readerEntity.Name = reader.Name;
            readerEntity.Address = reader.Address;
            readerEntity.Proffetion = reader.Proffetion;

            _applicationUnitOfWork.Readers.Add(readerEntity);
            _applicationUnitOfWork.Save();
        }

        public (int total, int totalDisplay, IList<ReaderBO> records) GetReaders(int pageIndex,
            int pageSize, string searchText, string orderby)
        {
            (IList<ReaderEO> data, int total, int totalDisplay) results = _applicationUnitOfWork
                .Readers.GetReaders(pageIndex, pageSize, searchText, orderby);

            IList<ReaderBO> books = new List<ReaderBO>();
            foreach (ReaderEO reader in results.data)
            {
                books.Add(new ReaderBO
                {
                    Id = reader.Id,
                    Name = reader.Name,
                    Address = reader.Address,
                    Proffetion = reader.Proffetion
                });
            }

            return (results.total, results.totalDisplay, books);
        }
    }
}
