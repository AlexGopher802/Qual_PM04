using System;
using System.Collections.Generic;

#nullable disable

namespace GasDeliveryApi.Models
{
    public partial class Client
    {
        public Client()
        {
            Ordereds = new HashSet<Ordered>();
        }

        public int Id { get; set; }
        public int PersonalInfoId { get; set; }
        public int UserId { get; set; }

        public virtual PersonalInfo PersonalInfo { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Ordered> Ordereds { get; set; }
    }
}
