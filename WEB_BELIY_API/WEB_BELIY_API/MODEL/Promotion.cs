using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_BELIY_API.MODEL
{
    public class Promotion
    {
        [Key]
        public Guid IDPromotion { get; set; }

        public string NamePromotion { get; set; }

        public double Discount { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }


    }
}
