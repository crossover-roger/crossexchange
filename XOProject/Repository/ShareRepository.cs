using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace XOProject
{
    public class ShareRepository : GenericRepository<Share, Guid>, IShareRepository
    {
        public ShareRepository(ExchangeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override IQueryable<Share> Query()
        {
            return base.Query()
                .Include(e => e.Rates)
                .Include(e => e.Trades)
                .AsQueryable();
        }
    }
}