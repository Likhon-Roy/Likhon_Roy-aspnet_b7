

using Infrastructure.Repositories;
using Infrastructure.Services;

namespace Infrastructure.UnitOfWorks
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IBookRepository Books { get; }
        IReaderRepository Readers {get; }
    }
}