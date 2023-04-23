using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace WEB_BELIY_API.MODEL
{
    public class ImportBill
    {
        [Key]
        public Guid IDImp { get; set; }

        // foreignKey
 
        [Display(Name = "Supplier")]
        public virtual Guid IDSupp { get; set; }
        [ForeignKey("IDSupp")]
        public virtual Supplier Supplier { get; set; }

        [Display(Name = "WareHouse")]
        public virtual Guid IDWare { get; set; }
        [ForeignKey("IDWare")]
        public virtual WareHouse WareHouse { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double TotalMoney { get; set; }

        public DateTime DateImport { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
