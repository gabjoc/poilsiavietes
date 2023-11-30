using System;
using System.Collections.Generic;

namespace Poilsiavietes.Models;

public partial class AutomobiliuStovejimoAikstele
{
    public string Pavadinimas { get; set; } = null!;

    public int VietuSk { get; set; }

    public string VietosAprasymas { get; set; } = null!;

    public string Adresas { get; set; } = null!;

    public bool Apmokama { get; set; }

    public int FkIdPoilsiaviete { get; set; }

    public int FkIdAutomobiliuAikstelesSavininkas { get; set; }

    public virtual AutomobiliuAiksteliuSavininkai FkIdAutomobiliuAikstelesSavininkasNavigation { get; set; } = null!;

    public virtual Poilsiaviete FkIdPoilsiavieteNavigation { get; set; } = null!;
}
