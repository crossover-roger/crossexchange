using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace XOProject
{
    public class TradeAnalysis
    {
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Sum { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Average { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Maximum { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Minimum { get; set; }   
    }
}
