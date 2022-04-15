using System;
using System.Collections.Generic;

#nullable disable

namespace GasDeliveryApi.Models
{
    public partial class User
    {
        public User()
        {
            Clients = new HashSet<Client>();
            Drivers = new HashSet<Driver>();
        }

        public int Id { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }

        public virtual UserRole Role { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
