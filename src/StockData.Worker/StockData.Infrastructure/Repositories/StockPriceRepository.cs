using Microsoft.EntityFrameworkCore;
using StockData.Infrastructure.DbContexts;
using StockData.Infrastructure.Entities;

namespace StockData.Infrastructure.Repositories
{
    public class StockPriceRepository : Repository<StockPrice, Guid>, IStockPriceRepository
    {
        public StockPriceRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

    }
}