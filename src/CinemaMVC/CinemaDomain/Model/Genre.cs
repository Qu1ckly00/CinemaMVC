using System;
using System.Collections.Generic;

namespace CinemaDomain.Model;

public partial class Genre : Entity
{

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
