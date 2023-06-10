using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_BELIY_API.MODEL
{
    public class Order
    {
        [Key]
        public Guid IdOrder { get; set; }
        public DateTime OrderData { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public double TotalValue { get; set; }
        public string DeliveryAddress { get; set; }

        [Display(Name = "Customer")]
        public Guid IDCus { get; set; }

        [ForeignKey("IDCus")]
        public virtual Customer Customer { get; set; }
        //public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}