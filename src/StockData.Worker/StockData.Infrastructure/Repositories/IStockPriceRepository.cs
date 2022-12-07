using StockData.Infrastructure.Entities;

namespace StockData.Infrastructure.Repositories
{
    public interface IStockPriceRepository : IRepository<StockPrice, Guid>
    {

    }
}