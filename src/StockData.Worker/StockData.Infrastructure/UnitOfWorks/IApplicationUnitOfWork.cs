using StockData.Infrastructure.Repositories;

namespace StockData.Infrastructure.UnitOfWorks
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IStockPriceRepository StockPrice { get; }
        ICompanyRepository Company { get; }
    }
}