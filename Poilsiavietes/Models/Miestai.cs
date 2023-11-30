using System;
using System.Collections.Generic;

namespace Poilsiavietes.Models;

public partial class Miestai
{
    public int Kodas { get; set; }

    public string Pavadinimas { get; set; } = null!;

    public virtual ICollection<Poilsiaviete> Poilsiavietes { get; } = new List<Poilsiaviete>();
}
