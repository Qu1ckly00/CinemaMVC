using System;
using System.Collections.Generic;

namespace CinemaDomain.Model;

public partial class Film : Entity
{

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Rating { get; set; }

    public string Directors { get; set; } = null!;

    public string TrailerUrl { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
