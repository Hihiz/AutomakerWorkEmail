using System;
using System.Collections.Generic;

namespace AutomakerWorkEmail.Models
{
    public partial class Service
    {
        public Service()
        {
            ClientOrders = new HashSet<ClientOrder>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal? Cost { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<ClientOrder> ClientOrders { get; set; }
    }
}
