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
    }
}
