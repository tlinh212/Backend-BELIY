using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_BELIY_API.MODEL
{
    public class Cart
    {
        [Key]
        public Guid IdCart { get; set; }

        [Required]
        [Display(Name = "IDPro")]
        public virtual Guid IDPro { get; set; }

        [ForeignKey("IDPro")]
        public virtual Product Product { get; set; }

        [Required]
        [Display(Name = "IDCus")]
        public virtual Guid IDCus { get; set; }

        [ForeignKey("IDPro")]
        public virtual Customer Customer { get; set; }

        public int Quantity { get; set; }


    }
}
