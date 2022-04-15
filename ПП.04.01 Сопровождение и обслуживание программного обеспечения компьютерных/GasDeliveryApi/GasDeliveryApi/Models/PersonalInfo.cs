using System;
using System.Collections.Generic;

#nullable disable

namespace GasDeliveryApi.Models
{
    public partial class PersonalInfo
    {
        public PersonalInfo()
        {
            Clients = new HashSet<Client>();
            Drivers = new HashSet<Driver>();
        }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
