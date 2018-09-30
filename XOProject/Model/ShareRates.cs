using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XOProject
{
    public class ShareRates
    {
        public Guid ShareId { get; set; }

        public DateTime TimeStamp { get; set; }

        public decimal Value { get; set; }
    }
}
