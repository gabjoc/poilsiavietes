using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Poilsiavietes.Models;

public partial class Buseno
{
    public int IdBusena { get; set; }
    
    public string Name { get; set; } = null!;

    public virtual ICollection<Rezervacija> Rezervacijos { get; } = new List<Rezervacija>();
}
