using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XOProject
{
    public class TradeViewModel
    {
        public string Symbol { get; set; }

        public int Quantity { get; set; }

        public OperationEnum Action { get; set; }
    }
}