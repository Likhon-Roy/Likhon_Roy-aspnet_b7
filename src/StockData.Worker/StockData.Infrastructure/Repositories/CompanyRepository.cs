using Microsoft.EntityFrameworkCore;
using StockData.Infrastructure.DbContexts;
using StockData.Infrastructure.Entities;

namespace StockData.Infrastructure.Repositories
{
    public class CompanyRepository : Repository<Company, Guid>, ICompanyRepository
    {
        public CompanyRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

    }
}