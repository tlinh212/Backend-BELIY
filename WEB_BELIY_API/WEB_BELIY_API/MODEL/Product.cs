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
        public virtual int IDCat { get; set; }
        [ForeignKey("IDCat")]
        public virtual Category Category { get; set; }

        [Required]
        [Display(Name = "ImportBill")]
        public virtual Guid IDImp { get; set; }

        [ForeignKey("IDImp")]
        public virtual ImportBill ImportBill { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    
        public string Description  { get; set; }

        public double Discount { get; set; }

        public double SaleRate { get; set; }       

    }
}
