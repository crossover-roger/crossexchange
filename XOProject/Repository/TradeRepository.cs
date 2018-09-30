using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace XOProject
{
    public class TradeRepository : GenericRepository<Trade, Guid>, ITradeRepository
    {
        public TradeRepository(ExchangeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override IQueryable<Trade> Query()
        {
            return base.Query()
                .Include(e => e.Portfolio)
                .Include(e => e.Share)
                .AsQueryable();
        }
    }
}