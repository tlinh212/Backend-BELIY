using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_BELIY_API.MODEL
{
    public class Product
    {
        [Key]
        public Guid IDPro { get; set; }

        [Required]
        [MaxLength(100)]
        public string NamePro { get; set; }

        [Display(Name = "Category")]
        public int IDCat { get; set; }

        [ForeignKey("IDCat")]
        public virtual Category Category { get; set; }

        public double Price { get; set; }
    
        public string Description  { get; set; }

        public double Discount { get; set; }

        public double SaleRate { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
       
    }
}
