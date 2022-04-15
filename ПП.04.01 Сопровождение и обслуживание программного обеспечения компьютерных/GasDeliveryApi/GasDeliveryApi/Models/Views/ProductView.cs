using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasDeliveryApi.Models.Views
{
    public class ProductView
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
