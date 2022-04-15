using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasDeliveryApi.Models.Views
{
    public class OrderProductView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Summ { get; set; }
    }
}
