using System;
using System.Collections.Generic;

namespace Poilsiavietes.Models;

public partial class AutomobiliuAiksteliuSavininkai
{
    public string Vardas { get; set; } = null!;

    public string Pavarde { get; set; } = null!;

    public string TelNumeris { get; set; } = null!;

    public string ElPastas { get; set; } = null!;

    public int IdAutomobiliuAikstelesSavininkas { get; set; }

    public virtual ICollection<AutomobiliuStovejimoAikstele> AutomobiliuStovejimoAiksteles { get; } = new List<AutomobiliuStovejimoAikstele>();
}
