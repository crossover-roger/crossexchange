using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace XOProject
{
    public class PortfolioRepository : GenericRepository<Portfolio, Guid>, IPortfolioRepository
    {
        public PortfolioRepository(ExchangeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override IQueryable<Portfolio> Query()
        {
            return base.Query()
                .Include(t => t.Trades)
                .ThenInclude(s => s.Share)
                .AsQueryable();
        }
    }
}