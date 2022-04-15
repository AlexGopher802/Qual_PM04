using System;
using System.Collections.Generic;

#nullable disable

namespace GasDeliveryApi.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderCompos = new HashSet<OrderCompo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public virtual ICollection<OrderCompo> OrderCompos { get; set; }
    }
}
