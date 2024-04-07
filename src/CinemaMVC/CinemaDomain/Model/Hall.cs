using System;
using System.Collections.Generic;

namespace CinemaDomain.Model;

public partial class Hall : Entity
{

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
