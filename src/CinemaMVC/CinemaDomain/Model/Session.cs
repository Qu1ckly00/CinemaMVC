using System;
using System.Collections.Generic;

namespace CinemaDomain.Model;

public partial class Session : Entity
{

    public DateTime Data { get; set; }

    public decimal TicketPrice { get; set; }

    public int FilmId { get; set; }

    public int HallId { get; set; }

    public virtual Film Film { get; set; } = null!;

    public virtual Hall Hall { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
