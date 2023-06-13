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
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public double TotalValue { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Boolean IsCharge { get; set; }
        public string NameStock { get; set; }
        public string DeliveryAddress { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}