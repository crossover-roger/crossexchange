using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XOProject
{
    public class Trade : BaseModel<Guid>
    {
        public Portfolio Portfolio { get; set; }

        public Share Share { get; set; }

        public int Quantity { get; set; }

        public decimal SinglePrice { get; set; }

        public decimal TotalPrice { get; set; }

        public OperationEnum Action { get; set; }
    }
}