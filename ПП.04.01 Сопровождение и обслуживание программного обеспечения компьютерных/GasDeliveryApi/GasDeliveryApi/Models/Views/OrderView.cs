using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasDeliveryApi.Models.Views
{
    public class OrderView
    {
        public int Id { get; set; }
        public AddressDelivery Address { get; set; }
        public string ClientLastName { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientPhone { get; set; }
        public string CourierPhone { get; set; }
        public string DateDelivery { get; set; }
        public string DesiredTimeFrom { get; set; }
        public string DesiredTimeTo { get; set; }
        public string ExactTime { get; set; }
        public double Summ { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderProductView> Products { get; set; }
    }
}
