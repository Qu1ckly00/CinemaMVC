using System;
using System.Collections.Generic;

namespace CinemaDomain.Model;

public partial class User : Entity
{

    public string Name { get; set; } = null!;

    public string Emaill { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string OrderHistory { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
