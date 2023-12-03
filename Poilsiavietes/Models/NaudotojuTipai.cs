using System;
using System.Collections.Generic;

namespace Poilsiavietes.Models;

public partial class NaudotojuTipai
{
    public int IdNaudojoTipas { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Naudotojai> Naudotojais { get; } = new List<Naudotojai>();
}
