using System;
using System.Collections.Generic;

namespace Poilsiavietes.Models;

public partial class Kategorijo
{
    public string Pavadinimas { get; set; } = null!;

    public int IdKategorija { get; set; }

    public virtual ICollection<Patogumai> Patogumais { get; } = new List<Patogumai>();
}
