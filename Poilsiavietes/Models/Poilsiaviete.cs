using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Poilsiavietes.Models;

public partial class Poilsiaviete
{
    public string Pavadinimas { get; set; } = null!;

    public double NaktiesKaina { get; set; }

    public string Adresas { get; set; } = null!;

    public int PastoKodas { get; set; }

    public int MiegamujuSkaicius { get; set; }

    public int VoniosKambariuSk { get; set; }

    public string Taisykles { get; set; } = null!;

    public bool Aktyvumas { get; set; }

    public string Reitingas { get; set; } = null!;

    public int Tipas { get; set; }

    public int IdPoilsiaviete { get; set; }

    public int FkIdNaudotojas { get; set; }

    public int FkKodas { get; set; }

    public virtual ICollection<AutomobiliuStovejimoAikstele> AutomobiliuStovejimoAiksteles { get; } = new List<AutomobiliuStovejimoAikstele>();

    public virtual Naudotojai FkIdNaudotojasNavigation { get; set; } = null!;

    public virtual Miestai FkKodasNavigation { get; set; } = null!;

    public virtual ICollection<PoilsiavieciuPatogumai> PoilsiavieciuPatogumais { get; } = new List<PoilsiavieciuPatogumai>();

    public virtual ICollection<Rezervacijo> Rezervacijos { get; } = new List<Rezervacijo>();

    public virtual Tipai TipasNavigation { get; set; } = null!;
}
