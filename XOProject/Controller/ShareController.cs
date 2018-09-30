using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace XOProject.Controller
{
    [Route("api/shares")]
    public class ShareController : ControllerBase
    {
        private readonly IShareRepository _shareRepository;

        public ShareController(IShareRepository shareRepository)
        {
            _shareRepository = shareRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var shares = await _shareRepository.Query().ToListAsync();

            return Ok(shares);
        }

        [HttpGet("{symbol}")]
        public async Task<IActionResult> Get([FromRoute]string symbol)
        {
            var share = await _shareRepository.Query()
                .Where(x => x.Symbol.Equals(symbol)).FirstOrDefaultAsync();

            return Ok(share);
        }


        [HttpGet("{symbol}/latest")]
        public async Task<IActionResult> GetLatestPrice([FromRoute]string symbol)
        {
            var share = await _shareRepository.Query()
                .Where(x => x.Symbol.Equals(symbol)).FirstOrDefaultAsync();

            return Ok(share?.CurrentPrice);
        }

        [HttpGet("{symbol}/rates")]
        public async Task<IActionResult> GetRates([FromRoute]string symbol)
        {
            var share = await _shareRepository.Query()
                .Where(x => x.Symbol.Equals(symbol)).FirstOrDefaultAsync();

            var rates = share?.Rates?.OrderByDescending(p => p.TimeStamp).ToList();

            return Ok(rates);
        }

        [HttpGet("{symbol}/trades")]
        public async Task<IActionResult> GetTrades([FromRoute]string symbol)
        {
            var share = await _shareRepository.Query()
                .Where(x => x.Symbol.Equals(symbol)).FirstOrDefaultAsync();

            return Ok(share?.Trades);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Share share)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _shareRepository.InsertAsync(share);

            return Created($"shares/{share.Id}", share);
        }

        [HttpPost("{symbol}/rates")]
        public async Task<IActionResult> PostRate([FromRoute]string symbol, [FromBody]ShareRates rate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (rate.TimeStamp == DateTime.MinValue)
            {
                rate.TimeStamp = DateTime.Now;
            }

            var share = await _shareRepository.Query().Where(x => x.Symbol.Equals(symbol)).FirstOrDefaultAsync();
            var newRate = new ShareRates { ShareId = share.Id, TimeStamp = rate.TimeStamp, Value = rate.Value };

            share.Rates.Add(newRate);
            await _shareRepository.UpdateAsync(share);

            return Created($"shares/latest", newRate);
        }

    }
}
