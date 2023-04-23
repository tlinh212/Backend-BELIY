using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace WEB_BELIY_API.MODEL
{
    public class WareHouse
    {
        [Key]
        public Guid IDWare { get; set;  }

        [Required]
        [MaxLength(100)]
        public string NameWare { get; set; }

        [Required]
        public string Address { get; set; }
        public virtual ICollection<ImportBill> ImportBills { get; set; }
    }
}
