using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_BELIY_API.MODEL
{
    public class Image
    {
        [Key]
         public Guid IDImage { get; set; }

        [Required]
        [Display(Name = "IDPro")]
        public virtual Guid IDPro { get; set; }

        [ForeignKey("IDPro")]
        public virtual Product Product { get; set; }

        public string LinkImage { get; set; }
    }
}
