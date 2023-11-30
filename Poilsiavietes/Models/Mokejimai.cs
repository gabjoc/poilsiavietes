using System;
using System.Collections.Generic;

namespace Poilsiavietes.Models;

public partial class Mokejimai
{
    public DateOnly Data { get; set; }

    public double Suma { get; set; }

    public int IdMokejimas { get; set; }

    public int FkIdNaudotojas { get; set; }

    public int FkNumeris { get; set; }

    public virtual Naudotojai FkIdNaudotojasNavigation { get; set; } = null!;

    public virtual Rezervacijo FkNumerisNavigation { get; set; } = null!;
}
