using Microsoft.EntityFrameworkCore;
using StockData.Infrastructure.DbContexts;
using StockData.Infrastructure.Repositories;

namespace StockData.Infrastructure.UnitOfWorks
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public IStockPriceRepository StockPrice { get; private set; }
        public ICompanyRepository Company { get; set; }

        public ApplicationUnitOfWork(IApplicationDbContext dbContext,
            IStockPriceRepository stockPriceRepository,
            ICompanyRepository companyRepository) 
        : base((DbContext)dbContext)
        {
            StockPrice = stockPriceRepository;
            Company = companyRepository;
        }
    }
}