using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XOProject
{
    public class BaseModel<TKeyType>
    {
        [Key]
        public TKeyType Id { get; set; }
    }
}
