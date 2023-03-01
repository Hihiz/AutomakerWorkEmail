using System;
using System.Collections.Generic;

namespace AutomakerWorkEmail.Models
{
    public partial class Role
    {
        public Role()
        {
            Clients = new HashSet<Client>();
            Workers = new HashSet<Worker>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
    }
}
