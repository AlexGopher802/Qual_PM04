using System;
using System.Collections.Generic;

#nullable disable

namespace GasDeliveryApi.Models
{
    public partial class Ordered
    {
        public Ordered()
        {
            OrderCompos = new HashSet<OrderCompo>();
        }

        public int Id { get; set; }
        public DateTime DateDelivery { get; set; }
        public TimeSpan? DesiredTimeFrom { get; set; }
        public TimeSpan? DesiredTimeTo { get; set; }
        public TimeSpan? ExactTime { get; set; }
        public double Sum { get; set; }
        public int AddressId { get; set; }
        public int? DriverId { get; set; }
        public int ClientId { get; set; }
        public int StatusId { get; set; }

        public virtual AddressDelivery Address { get; set; }
        public virtual Client Client { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual ICollection<OrderCompo> OrderCompos { get; set; }
    }
}
