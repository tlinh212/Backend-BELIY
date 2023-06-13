using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_BELIY_API.MODEL
{
    public class ProductDetail
    {
        [Key]
        public Guid IDProDetail { get; set; }

        [Display(Name = "Product")]
        public Guid IDPro { get; set; }

        [ForeignKey("IDPro")]
        public virtual Product Product { get; set; }
        
        [Display(Name = "Size")]
        public Guid IDSize { get; set; }

        [ForeignKey("IDSize")]
        public virtual Size Size { get; set; }

        public int Quantity { get; set; }

        // public virtual ICollection<ProductStock> ProductStocks { get; set; }
    }
}
