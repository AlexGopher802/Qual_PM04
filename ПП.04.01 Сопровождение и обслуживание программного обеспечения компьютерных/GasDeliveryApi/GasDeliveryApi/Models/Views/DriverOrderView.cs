using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasDeliveryApi.Models.Views
{
    public class DriverOrderView
    {
        public int OrderId { get; set; }
        public string DriverPhone { get; set; }
        public string ExactTime { get; set; }
        public string OrderStatus { get; set; }

    }
}
