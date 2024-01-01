using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Poilsiavietes.Models;

public partial class Poilsiaviete
{
    public string Pavadinimas { get; set; } = null!;
    [DisplayName("Nakties kaina")]
    [Range(5, double.MaxValue, ErrorMessage = "Value must be equal or larger than 5,00.")]
    public double NaktiesKaina { get; set; }
    public string Adresas { get; set; } = null!;
    [DisplayName("Pašto kodas")]
    [Range(1, int.MaxValue, ErrorMessage = "Value must be equal or larger than 1.")]
    public int PastoKodas { get; set; }
    [DisplayName("Miegamųjų skaičius")]
    [Range(1, int.MaxValue, ErrorMessage = "Value must be equal or larger than 1.")]
    public int MiegamujuSkaicius { get; set; }
    [DisplayName("Vonios kambarių skaičius")]
    [Range(1, int.MaxValue, ErrorMessage = "Value must be equal or larger than 1.")]
    public int VoniosKambariuSk { get; set; }
    [DisplayName("Taisyklės")]
    public string Taisykles { get; set; } = null!;
    public bool Aktyvumas { get; set; }
    [Range(1, 5, ErrorMessage = "Value must be between 1 and 5")]
    public string Reitingas { get; set; } = null!;
    [DisplayName("Poilsiavietės tipas")]
    public int Tipas { get; set; }

    public int IdPoilsiaviete { get; set; }

    public int FkIdNaudotojas { get; set; }
    [DisplayName("Miestas")]
    public int FkKodas { get; set; }

    public virtual ICollection<AutomobiliuStovejimoAikstele> AutomobiliuStovejimoAiksteles { get; } = new List<AutomobiliuStovejimoAikstele>();

    public virtual Naudotojai? FkIdNaudotojasNavigation { get; set; } = null;

    public virtual Miestai? FkKodasNavigation { get; set; } = null;

    public virtual ICollection<PoilsiavieciuPatogumai> PoilsiavieciuPatogumai { get; } = new List<PoilsiavieciuPatogumai>();

    public virtual ICollection<Rezervacijos> Rezervacijos { get; } = new List<Rezervacijos>();

    public virtual Tipai? TipasNavigation { get; set; } = null;
}
