using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Poilsiavietes.Models;

public partial class Naudotojai
{
    [Required(ErrorMessage = "Vartotojo vardas yra privalomas")]
    [Display(Name = "Vartotojo vardas")]
    public string VartotojoVardas { get; set; } = null!;

    [Required(ErrorMessage = "Vardas yra privalomas")]
    [Display(Name = "Vardas")]
    public string Vardas { get; set; } = null!;

    [Required(ErrorMessage = "Pavarde yra privaloma")]
    [Display(Name = "Pavarde")]
    public string Pavarde { get; set; } = null!;

    [Required(ErrorMessage = "Gimimo data yra privaloma")]
    [Display(Name = "Gimimo data")]
    public DateOnly GimimoData { get; set; }

    [Required(ErrorMessage = "Telefono numeris yra privalomas")]
    [Display(Name = "Telefono numeris")]
    public string TelNumeris { get; set; } = null!;

    [Required(ErrorMessage = "El. paštas yra privalomas")]
    [EmailAddress(ErrorMessage = "Neteisingas el. pašto formatas")]
    [Display(Name = "Elektronis pastas")]
    public string ElPastas { get; set; } = null!;

    [Required(ErrorMessage = "Slaptažodis yra privalomas")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{9,}$", ErrorMessage = "Slaptažodis turi būti bent 9 simbolių ilgio ir turėti bent vieną didžiąją raidę, vieną skaičių ir vieną specialų simbolį.")]
    [Display(Name = "Slaptazodis")]
    public string Slaptazodis { get; set; } = null!;

    public int NaudotojoTipas { get; set; }

    public int IdNaudotojas { get; set; }

    public virtual ICollection<Mokejimai> Mokejimais { get; } = new List<Mokejimai>();

    public virtual NaudotojuTipai NaudotojoTipasNavigation { get; set; } = null!;

    public virtual ICollection<Poilsiaviete> Poilsiavietes { get; } = new List<Poilsiaviete>();

    public virtual ICollection<Rezervacijos> Rezervacijos { get; } = new List<Rezervacijos>();
}
