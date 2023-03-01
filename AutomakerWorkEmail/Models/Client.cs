using System;
using System.Collections.Generic;

namespace AutomakerWorkEmail.Models
{
    public partial class Client
    {
        public Client()
        {
            ClientOrders = new HashSet<ClientOrder>();
        }

        public int Id { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Passport { get; set; } = null!;
        public string? PhotoPath { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<ClientOrder> ClientOrders { get; set; }
    }
}
