using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WEB_BELIY_API.DATA;

namespace WEB_BELIY_API.MODEL
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDCat { get; set;  }

        [Required]
        [MaxLength(100)]
        public string Name { get; set;  }
        public int IDParent { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
