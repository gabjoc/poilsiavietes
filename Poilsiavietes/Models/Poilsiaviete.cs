using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Poilsiavietes.Models;

public partial class Poilsiaviete
{
    public string Pavadinimas { get; set; } = null!;
    [DisplayName("Nakties kaina")]
    public double NaktiesKaina { get; set; }
    public string Adresas { get; set; } = null!;
    [DisplayName("Pašto kodas")]
    public int PastoKodas { get; set; }
    [DisplayName("Miegamųjų skaičius")]
    public int MiegamujuSkaicius { get; set; }
    [DisplayName("Vonios kambarių skaičius")]
    public int VoniosKambariuSk { get; set; }
    [DisplayName("Taisyklės")]
    public string Taisykles { get; set; } = null!;

    public bool Aktyvumas { get; set; }

    public string Reitingas { get; set; } = null!;
    [DisplayName("Poilsiavietės tipas")]
    public int Tipas { get; set; }

    public int IdPoilsiaviete { get; set; }

    public int FkIdNaudotojas { get; set; }

    public int FkKodas { get; set; }

    public virtual ICollection<AutomobiliuStovejimoAikstele> AutomobiliuStovejimoAiksteles { get; } = new List<AutomobiliuStovejimoAikstele>();

    public virtual Naudotojai? FkIdNaudotojasNavigation { get; set; } = null;

    public virtual Miestai? FkKodasNavigation { get; set; } = null;

    public virtual ICollection<PoilsiavieciuPatogumai> PoilsiavieciuPatogumais { get; } = new List<PoilsiavieciuPatogumai>();

    public virtual ICollection<Rezervacija> Rezervacijos { get; } = new List<Rezervacija>();

    public virtual Tipai? TipasNavigation { get; set; } = null;
}
