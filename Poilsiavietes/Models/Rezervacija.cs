using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Poilsiavietes.Models;

public partial class Rezervacija
{
    
    public string Vardas { get; set; } = null!;

    [DisplayName("Pavardė")]
    public string Pavarde { get; set; } = null!;

    [DisplayName("Telefono numeris")]
    public string TelNumeris { get; set; } = null!;

    [DisplayName("Elektroninis paštas")]
    public string ElPastas { get; set; } = null!;

    [DisplayName("Svečių skaičius")]
    [Range(1, int.MaxValue, ErrorMessage = "Svečių turi būti 1 arba daugiau.")]
    public int ZmoniuSk { get; set; }

    [DisplayName("Atvažiuosite su augintiniu")]
    public bool YraAugintinis { get; set; }

    public int RezNumeris { get; set; }

    [DisplayName("Rezervacijos pradžia")]
    public DateOnly Pradzia { get; set; }

    [DisplayName("Rezervacijos pabaiga")]
    public DateOnly Pabaiga { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Suma negali būti neigiama.")]
    public double Suma { get; set; }

    [DisplayName("Apmokėta")]
    public bool Apmoketa { get; set; }

    [DisplayName("Rezervacijos sudarymo data")]
    public DateOnly SukurimoData { get; set; }

    [DisplayName("Būsena")]
    public int Busena { get; set; }

    [DisplayName("Poilsiavietės pavadinimas")]
    public int FkIdPoilsiaviete { get; set; }

    public int FkIdNaudotojas { get; set; }

    [DisplayName("Būsena")]
    public virtual Buseno BusenaNavigation { get; set; } = null!;

    public virtual Naudotojai FkIdNaudotojasNavigation { get; set; } = null!;

    [DisplayName("Poilsiavietė")]
    public virtual Poilsiaviete FkIdPoilsiavieteNavigation { get; set; } = null!;

    public virtual Mokejimai? Mokejimai { get; set; }
}
