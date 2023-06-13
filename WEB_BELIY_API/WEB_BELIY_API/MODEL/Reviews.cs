using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_BELIY_API.MODEL
{
    public class Review
    {
        [Key]
        public Guid IDUser { get; set; }

        [ForeignKey("IDPro")]
        public Guid IDPro { get; set; }
        public double ratting { get; set; }
      
    }
}
