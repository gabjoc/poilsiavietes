using System;
using System.Collections.Generic;

namespace Poilsiavietes.Models;

public partial class Rezervacijos
{
    public int Numeris { get; set; }

    public DateOnly Pradzia { get; set; }

    public DateOnly Pabaiga { get; set; }

    public double Suma { get; set; }

    public int ZmoniuSk { get; set; }

    public bool Apmoketa { get; set; }

    public string Vardas { get; set; } = null!;

    public string Pavarde { get; set; } = null!;

    public string TelNumeris { get; set; } = null!;

    public string ElPastas { get; set; } = null!;

    public bool YraAugintinis { get; set; }

    public DateOnly SukurimoData { get; set; }

    public int Busena { get; set; }

    public int FkIdPoilsiaviete { get; set; }

    public int FkIdNaudotojas { get; set; }

    public virtual Buseno BusenaNavigation { get; set; } = null!;

    public virtual Naudotojai FkIdNaudotojasNavigation { get; set; } = null!;

    public virtual Poilsiaviete FkIdPoilsiavieteNavigation { get; set; } = null!;

    public virtual Mokejimai? Mokejimai { get; set; }
}
