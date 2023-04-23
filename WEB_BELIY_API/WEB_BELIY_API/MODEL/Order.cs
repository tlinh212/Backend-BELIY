using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_BELIY_API.MODEL
{
    public class Order
    {
        [Key]
        public Guid IdOrder { get; set; }

        public DateTime OrderCreateTime { get; set; }

    }
}
