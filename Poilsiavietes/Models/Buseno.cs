using System;
using System.Collections.Generic;

namespace Poilsiavietes.Models;

public partial class Buseno
{
    public int IdBusena { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Rezervacijos> Rezervacijos { get; } = new List<Rezervacijos>();
}
