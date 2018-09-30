using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XOProject
{
    public class Portfolio : BaseModel<Guid>
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public ICollection<Trade> Trades { get; set; }
    }
}
