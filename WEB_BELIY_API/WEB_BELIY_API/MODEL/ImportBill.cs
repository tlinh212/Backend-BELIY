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

        public string NameSupplier { get; set; }

        public double TotalMoney { get; set; }

        public DateTime DateImport { get; set; }

        public virtual ICollection<ImportDetail> ImportDetails { get; set; }

    }
}
