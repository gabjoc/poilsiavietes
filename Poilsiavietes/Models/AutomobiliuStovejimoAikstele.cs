using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Poilsiavietes.Models;

public partial class AutomobiliuStovejimoAikstele
{
    public string? Pavadinimas { get; set; } = null!;
    [DisplayName("Vietų skaičius")]
    [Range(1, int.MaxValue, ErrorMessage = "Value must be equal or larger than 1.")]
    public int? VietuSk { get; set; } = 0;
    [DisplayName("Vietos aprašymas")]
    public string? VietosAprasymas { get; set; } = null!;
 
    public string? Adresas { get; set; } = null!;

    public bool Apmokama { get; set; }

    public int FkIdPoilsiaviete { get; set; }

    public int FkIdAutomobiliuAikstelesSavininkas { get; set; }

    public virtual AutomobiliuAiksteliuSavininkai? FkIdAutomobiliuAikstelesSavininkasNavigation { get; set; } = null!;

    public virtual Poilsiaviete? FkIdPoilsiavieteNavigation { get; set; } = null!;
}
