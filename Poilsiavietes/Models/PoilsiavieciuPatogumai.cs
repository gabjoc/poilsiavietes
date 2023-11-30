using System;
using System.Collections.Generic;

namespace Poilsiavietes.Models;

public partial class PoilsiavieciuPatogumai
{
    public int Kiekis { get; set; }

    public int FkIdPoilsiaviete { get; set; }

    public int FkIdPatogumas { get; set; }

    public virtual Patogumai FkIdPatogumasNavigation { get; set; } = null!;

    public virtual Poilsiaviete FkIdPoilsiavieteNavigation { get; set; } = null!;
}
