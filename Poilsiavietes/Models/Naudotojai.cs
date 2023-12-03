using System;
using System.Collections.Generic;

namespace Poilsiavietes.Models;

public partial class Naudotojai
{
    public string VartotojoVardas { get; set; } = null!;

    public string Vardas { get; set; } = null!;

    public string Pavarde { get; set; } = null!;

    public DateOnly GimimoData { get; set; }

    public string TelNumeris { get; set; } = null!;

    public string ElPastas { get; set; } = null!;

    public string Slaptazodis { get; set; } = null!;

    public int NaudotojoTipas { get; set; }

    public int IdNaudotojas { get; set; }

    public virtual ICollection<Mokejimai> Mokejimais { get; } = new List<Mokejimai>();

    public virtual NaudotojuTipai NaudotojoTipasNavigation { get; set; } = null!;

    public virtual ICollection<Poilsiaviete> Poilsiavietes { get; } = new List<Poilsiaviete>();

    public virtual ICollection<Rezervacijo> Rezervacijos { get; } = new List<Rezervacijo>();
}
