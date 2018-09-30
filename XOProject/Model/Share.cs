using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XOProject
{
    public class Share : BaseModel<Guid>
    {
        [Required]
        [MinLength(3), MaxLength(3)]
        public string Symbol { get; set; }

        public decimal CurrentPrice
        {
            get
            {
                var lastRate = this.Rates?.OrderByDescending(p => p.TimeStamp).FirstOrDefault();
                if (lastRate == null)
                {
                    return 0;
                }
                else
                {
                    return lastRate.Value;
                }
            }
        } 
        
        public ICollection<ShareRates> Rates { get; set; }

        public ICollection<Trade> Trades { get; set; }
    }
}