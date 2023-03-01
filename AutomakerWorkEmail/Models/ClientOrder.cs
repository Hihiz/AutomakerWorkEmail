using System;
using System.Collections.Generic;

namespace AutomakerWorkEmail.Models
{
    public partial class ClientOrder
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;
        public string TrackNumber { get; set; } = null!;
        public string Code { get; set; } = null!;
        public decimal FinalCost { get; set; }
        public DateTime? DateDispatch { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; } = null!;

        public int ServiceId { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
    }
}
