using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_BELIY_API.MODEL
{
    public class Supplier
    {
        [Key]
        public Guid IDSupp { get; set; }

        [Required]
        [MaxLength(100)]
        public string NameSupp { get; set; }
         
        [Required]
        [MaxLength(100)]
        public string ProductType{ get; set; }

        public virtual ICollection<ImportBill> ImportBills { get; set; }

    }
}

