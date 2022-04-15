using System;
using System.Collections.Generic;

#nullable disable

namespace GasDeliveryApi.Models
{
    public partial class AddressDelivery
    {
        public AddressDelivery()
        {
            Ordereds = new HashSet<Ordered>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public int? ApartmentNum { get; set; }
        public int? FrontDoorNum { get; set; }
        public int? FloorNum { get; set; }
        public string Intercom { get; set; }

        public virtual ICollection<Ordered> Ordereds { get; set; }
    }
}
