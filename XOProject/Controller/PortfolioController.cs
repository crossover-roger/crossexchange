using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace XOProject.Controller
{
    [Route("api/portfolios")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var portfolios = await _portfolioRepository.Query().ToListAsync();

            return Ok(portfolios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var portfolio = await _portfolioRepository.Query()
                .Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            
            return Ok(portfolio);
        }

        [HttpGet("{id}/trades")]
        public async Task<IActionResult> GetTrades([FromRoute]Guid id)
        {
            var portfolio = await _portfolioRepository.Query()
                .Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            return Ok(portfolio?.Trades);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Portfolio portfolio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _portfolioRepository.InsertAsync(portfolio);

            return Created($"Portfolio/{portfolio.Id}", portfolio);
        }

    }
}
