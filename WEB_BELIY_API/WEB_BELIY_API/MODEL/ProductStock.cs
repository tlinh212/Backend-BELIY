using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_BELIY_API.MODEL
{
    public class ProductStock
    {
        [Key]
        public Guid IDProStock { get; set; }

        [Display(Name = "ProductDetail")]
        public Guid IDProDetail { get; set; }

        [ForeignKey("IDProDetail")]
        public virtual ProductDetail ProductDetail { get; set; }

        [Display(Name = "Stock")]
        public Guid IDStock { get; set; }

        [ForeignKey("IDStock")]
        public virtual Stock Stock { get; set; }
        public int Quantity { get; set; }

        //public virtual ICollection<ImportDetail> ImportDetails { get; set; }

        //public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }   
}
