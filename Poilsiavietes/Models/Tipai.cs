using System;
using System.Collections.Generic;

namespace Poilsiavietes.Models;

public partial class Tipai
{
    public int IdTipas { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Poilsiaviete> Poilsiavietes { get; } = new List<Poilsiaviete>();
}
