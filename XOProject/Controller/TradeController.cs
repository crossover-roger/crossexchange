using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace XOProject.Controller
{
    [Route("api/trades")]
    public class TradeController : ControllerBase
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IShareRepository _shareRepository;
        private readonly ITradeRepository _tradeRepository;

        public TradeController(IPortfolioRepository portfolioRepository, IShareRepository shareRepository, ITradeRepository tradeRepository)
        {
            _portfolioRepository = portfolioRepository;
            _shareRepository = shareRepository;
            _tradeRepository = tradeRepository;
        }

        [HttpGet("{portfolioId}")]
        public async Task<IActionResult> Get([FromRoute]Guid portfolioId)
        {
            var portfolio = await _portfolioRepository.Query()
                .Where(x => x.Id.Equals(portfolioId)).FirstOrDefaultAsync();

            var trades = portfolio?.Trades.Select(p => new { p.Id, p.Share.Symbol, p.Quantity, p.SinglePrice, p.TotalPrice, Action = p.Action.ToString() });

            return Ok(trades);
        }

        [HttpPost("{portfolioId}")]
        public async Task<IActionResult> Post([FromRoute]Guid portfolioId, [FromBody]TradeViewModel trade)
        {
            var portfolio = await _portfolioRepository.Query()
                .Where(x => x.Id.Equals(portfolioId)).FirstOrDefaultAsync();

            var share = await _shareRepository.Query()
                .Where(x => x.Symbol.Equals(trade.Symbol)).FirstOrDefaultAsync();

            var newTrade = new Trade()
                {
                    Portfolio = portfolio,
                    Share = share,
                    Action = trade.Action,
                    Quantity = trade.Quantity,
                    SinglePrice = share.CurrentPrice
                };

            await _tradeRepository.InsertAsync(newTrade);

            return Created($"trades/{newTrade.Id}", newTrade);
        }


        /// <summary>
        /// For a given symbol of share, get the statistics for that particular share calculating the maximum, minimum, 
        /// average and Sum of all the trades for that share individually grouped into Buy trade and Sell trade.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>

        [HttpGet("analysis/{symbol}")]
        public async Task<IActionResult> GetAnalysis([FromRoute]string symbol)
        {
            var buyAnalysis = new TradeAnalysis();
            var sellAnalysis = new TradeAnalysis();

            var share = await _shareRepository.Query().Where(p => p.Symbol.Equals(symbol)).FirstOrDefaultAsync();

            var buyTrades = share?.Trades?.Where(p => p.Action.Equals(OperationEnum.Buy)).ToList();
            var sellTrades = share?.Trades?.Where(p => p.Action.Equals(OperationEnum.Sell)).ToList();

            if (buyTrades != null)
            {
                buyAnalysis.Sum = buyTrades.Sum(p => p.TotalPrice);
                buyAnalysis.Average = buyTrades.Average(p => p.TotalPrice);
                buyAnalysis.Minimum = buyTrades.Min(p => p.TotalPrice);
                buyAnalysis.Maximum = buyTrades.Max(p => p.TotalPrice);
            }

            if (sellTrades != null)
            {
                sellAnalysis.Sum = sellTrades.Sum(p => p.TotalPrice);
                sellAnalysis.Average = sellTrades.Average(p => p.TotalPrice);
                sellAnalysis.Minimum = sellTrades.Min(p => p.TotalPrice);
                sellAnalysis.Maximum = sellTrades.Max(p => p.TotalPrice);
            }

            return Ok(new { buyAnalysis, sellAnalysis });
        }


    }
}
