using System;
using System.Collections.Generic;

namespace CinemaDomain.Model;

public partial class Ticket : Entity
{

    public int SessionId { get; set; }

    public string Seat { get; set; } = null!;

    public int UserId { get; set; }

    public string MovieTitle { get; set; } = null!;

    public virtual Session Session { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
