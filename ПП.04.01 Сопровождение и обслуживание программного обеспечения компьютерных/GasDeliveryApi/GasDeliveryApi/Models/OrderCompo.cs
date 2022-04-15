using System;
using System.Collections.Generic;

#nullable disable

namespace GasDeliveryApi.Models
{
    public partial class OrderCompo
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Sum { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public virtual Ordered Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
