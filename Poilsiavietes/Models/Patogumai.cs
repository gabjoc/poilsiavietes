using System;
using System.Collections.Generic;

namespace Poilsiavietes.Models;

public partial class Patogumai
{
    public string Pavadinimas { get; set; } = null!;

    public string? Paskirtis { get; set; }

    public int IdPatogumas { get; set; }

    public int FkIdKategorija { get; set; }

    public virtual Kategorijo FkIdKategorijaNavigation { get; set; } = null!;

    public virtual ICollection<PoilsiavieciuPatogumai> PoilsiavieciuPatogumais { get; } = new List<PoilsiavieciuPatogumai>();
}
