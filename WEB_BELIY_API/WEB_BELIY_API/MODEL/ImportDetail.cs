using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_BELIY_API.MODEL
{
    public class ImportDetail
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "ImportBill")]
        public Guid IDIDImp { get; set; }

        [ForeignKey("IDImp")]
        public virtual ImportBill ImportBill { get; set; }

        [Display(Name = "ProductStock")]
        public Guid IDProStock { get; set; }

        [ForeignKey("IDProStock")]
        public virtual ProductStock ProductStock { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
