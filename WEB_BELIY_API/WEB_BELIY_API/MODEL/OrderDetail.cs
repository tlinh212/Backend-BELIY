using System;
using System.ComponentModel.DataAnnotations;

namespace WEB_BELIY_API.MODEL
{
    public class OrderDetail
    {
        public Guid IDOrder { get; set; }
        public Order Order { get; set; }

        public Guid IDProStock { get; set; }
        public ProductStock ProductStock { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
