using Microsoft.EntityFrameworkCore;

namespace Poilsiavietes.Models;

public partial class PoilsiavietesContext : DbContext
{
    public PoilsiavietesContext()
    {
    }

    public PoilsiavietesContext(DbContextOptions<PoilsiavietesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AutomobiliuAiksteliuSavininkai> AutomobiliuAiksteliuSavininkais { get; set; }

    public virtual DbSet<AutomobiliuStovejimoAikstele> AutomobiliuStovejimoAiksteles { get; set; }

    public virtual DbSet<Buseno> Busenos { get; set; }

    public virtual DbSet<Kategorijo> Kategorijos { get; set; }

    public virtual DbSet<Miestai> Miestais { get; set; }

    public virtual DbSet<Mokejimai> Mokejimais { get; set; }

    public virtual DbSet<Naudotojai> Naudotojais { get; set; }

    public virtual DbSet<NaudotojuTipai> NaudotojuTipais { get; set; }

    public virtual DbSet<Patogumai> Patogumais { get; set; }

    public virtual DbSet<PoilsiavieciuPatogumai> PoilsiavieciuPatogumais { get; set; }

    public virtual DbSet<Poilsiaviete> Poilsiavietes { get; set; }

    public virtual DbSet<Rezervacija> Rezervacijos { get; set; }

    public virtual DbSet<Tipai> Tipais { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;user=root;database=poilsiavietes", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AutomobiliuAiksteliuSavininkai>(entity =>
        {
            entity.HasKey(e => e.IdAutomobiliuAikstelesSavininkas).HasName("PRIMARY");

            entity.ToTable("automobiliu_aiksteliu_savininkai");

            entity.Property(e => e.IdAutomobiliuAikstelesSavininkas)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_Automobiliu_aiksteles_savininkas");
            entity.Property(e => e.ElPastas)
                .HasMaxLength(255)
                .HasColumnName("el_pastas");
            entity.Property(e => e.Pavarde)
                .HasMaxLength(255)
                .HasColumnName("pavarde");
            entity.Property(e => e.TelNumeris)
                .HasMaxLength(255)
                .HasColumnName("tel_numeris");
            entity.Property(e => e.Vardas)
                .HasMaxLength(255)
                .HasColumnName("vardas");
        });

        modelBuilder.Entity<AutomobiliuStovejimoAikstele>(entity =>
        {
            entity.HasKey(e => new { e.FkIdPoilsiaviete, e.FkIdAutomobiliuAikstelesSavininkas })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("automobiliu_stovejimo_aiksteles");

            entity.HasIndex(e => e.FkIdAutomobiliuAikstelesSavininkas, "priziuri");

            entity.Property(e => e.FkIdPoilsiaviete)
                .HasColumnType("int(11)")
                .HasColumnName("fk_id_Poilsiaviete");
            entity.Property(e => e.FkIdAutomobiliuAikstelesSavininkas)
                .HasColumnType("int(11)")
                .HasColumnName("fk_id_Automobiliu_aiksteles_savininkas");
            entity.Property(e => e.Adresas)
                .HasMaxLength(255)
                .HasColumnName("adresas");
            entity.Property(e => e.Apmokama).HasColumnName("apmokama");
            entity.Property(e => e.Pavadinimas)
                .HasMaxLength(255)
                .HasColumnName("pavadinimas");
            entity.Property(e => e.VietosAprasymas)
                .HasMaxLength(255)
                .HasColumnName("vietos_aprasymas");
            entity.Property(e => e.VietuSk)
                .HasColumnType("int(11)")
                .HasColumnName("vietu_sk");

            entity.HasOne(d => d.FkIdAutomobiliuAikstelesSavininkasNavigation).WithMany(p => p.AutomobiliuStovejimoAiksteles)
                .HasForeignKey(d => d.FkIdAutomobiliuAikstelesSavininkas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("priziuri");

            entity.HasOne(d => d.FkIdPoilsiavieteNavigation).WithMany(p => p.AutomobiliuStovejimoAiksteles)
                .HasForeignKey(d => d.FkIdPoilsiaviete)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("yra_susijusi");
        });

        modelBuilder.Entity<Buseno>(entity =>
        {
            entity.HasKey(e => e.IdBusena).HasName("PRIMARY");

            entity.ToTable("busenos");

            entity.Property(e => e.IdBusena)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_busena");
            entity.Property(e => e.Name)
                .HasMaxLength(11)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Kategorijo>(entity =>
        {
            entity.HasKey(e => e.IdKategorija).HasName("PRIMARY");

            entity.ToTable("kategorijos");

            entity.Property(e => e.IdKategorija)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_Kategorija");
            entity.Property(e => e.Pavadinimas)
                .HasMaxLength(255)
                .HasColumnName("pavadinimas");
        });

        modelBuilder.Entity<Miestai>(entity =>
        {
            entity.HasKey(e => e.Kodas).HasName("PRIMARY");

            entity.ToTable("miestai");

            entity.Property(e => e.Kodas)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("kodas");
            entity.Property(e => e.Pavadinimas)
                .HasMaxLength(255)
                .HasColumnName("pavadinimas");
        });

        modelBuilder.Entity<Mokejimai>(entity =>
        {
            entity.HasKey(e => e.IdMokejimas).HasName("PRIMARY");

            entity.ToTable("mokejimai");

            entity.HasIndex(e => e.FkNumeris, "fk_numeris").IsUnique();

            entity.HasIndex(e => e.FkIdNaudotojas, "sumoka");

            entity.Property(e => e.IdMokejimas)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_Mokejimas");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.FkIdNaudotojas)
                .HasColumnType("int(11)")
                .HasColumnName("fk_id_Naudotojas");
            entity.Property(e => e.FkNumeris)
                .HasColumnType("int(11)")
                .HasColumnName("fk_numeris");
            entity.Property(e => e.Suma).HasColumnName("suma");

            entity.HasOne(d => d.FkIdNaudotojasNavigation).WithMany(p => p.Mokejimais)
                .HasForeignKey(d => d.FkIdNaudotojas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sumoka");

            entity.HasOne(d => d.FkNumerisNavigation).WithOne(p => p.Mokejimai)
                .HasForeignKey<Mokejimai>(d => d.FkNumeris)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("apmoka");
        });

        modelBuilder.Entity<Naudotojai>(entity =>
        {
            entity.HasKey(e => e.IdNaudotojas).HasName("PRIMARY");

            entity.ToTable("naudotojai");

            entity.HasIndex(e => e.NaudotojoTipas, "naudotojo_tipas");

            entity.Property(e => e.IdNaudotojas)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_Naudotojas");
            entity.Property(e => e.ElPastas)
                .HasMaxLength(255)
                .HasColumnName("el_pastas");
            entity.Property(e => e.GimimoData).HasColumnName("gimimo_data");
            entity.Property(e => e.NaudotojoTipas)
                .HasColumnType("int(11)")
                .HasColumnName("naudotojo_tipas");
            entity.Property(e => e.Pavarde)
                .HasMaxLength(255)
                .HasColumnName("pavarde");
            entity.Property(e => e.Slaptazodis)
                .HasMaxLength(255)
                .HasColumnName("slaptazodis");
            entity.Property(e => e.TelNumeris)
                .HasMaxLength(255)
                .HasColumnName("tel_numeris");
            entity.Property(e => e.Vardas)
                .HasMaxLength(255)
                .HasColumnName("vardas");
            entity.Property(e => e.VartotojoVardas)
                .HasMaxLength(255)
                .HasColumnName("vartotojo_vardas");

            entity.HasOne(d => d.NaudotojoTipasNavigation).WithMany(p => p.Naudotojais)
                .HasForeignKey(d => d.NaudotojoTipas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("naudotojai_ibfk_1");
        });

        modelBuilder.Entity<NaudotojuTipai>(entity =>
        {
            entity.HasKey(e => e.IdNaudojoTipas).HasName("PRIMARY");

            entity.ToTable("naudotoju_tipai");

            entity.Property(e => e.IdNaudojoTipas)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_naudojo_tipas");
            entity.Property(e => e.Name)
                .HasMaxLength(16)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Patogumai>(entity =>
        {
            entity.HasKey(e => e.IdPatogumas).HasName("PRIMARY");

            entity.ToTable("patogumai");

            entity.HasIndex(e => e.FkIdKategorija, "priklauso");

            entity.Property(e => e.IdPatogumas)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_Patogumas");
            entity.Property(e => e.FkIdKategorija)
                .HasColumnType("int(11)")
                .HasColumnName("fk_id_Kategorija");
            entity.Property(e => e.Paskirtis)
                .HasMaxLength(255)
                .HasColumnName("paskirtis");
            entity.Property(e => e.Pavadinimas)
                .HasMaxLength(255)
                .HasColumnName("pavadinimas");

            entity.HasOne(d => d.FkIdKategorijaNavigation).WithMany(p => p.Patogumais)
                .HasForeignKey(d => d.FkIdKategorija)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("priklauso");
        });

        modelBuilder.Entity<PoilsiavieciuPatogumai>(entity =>
        {
            entity.HasKey(e => new { e.FkIdPoilsiaviete, e.FkIdPatogumas })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("poilsiavieciu_patogumai");

            entity.HasIndex(e => e.FkIdPatogumas, "sudaro");

            entity.Property(e => e.FkIdPoilsiaviete)
                .HasColumnType("int(11)")
                .HasColumnName("fk_id_Poilsiaviete");
            entity.Property(e => e.FkIdPatogumas)
                .HasColumnType("int(11)")
                .HasColumnName("fk_id_Patogumas");
            entity.Property(e => e.Kiekis)
                .HasColumnType("int(11)")
                .HasColumnName("kiekis");

            entity.HasOne(d => d.FkIdPatogumasNavigation).WithMany(p => p.PoilsiavieciuPatogumais)
                .HasForeignKey(d => d.FkIdPatogumas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sudaro");

            entity.HasOne(d => d.FkIdPoilsiavieteNavigation).WithMany(p => p.PoilsiavieciuPatogumais)
                .HasForeignKey(d => d.FkIdPoilsiaviete)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("yra");
        });

        modelBuilder.Entity<Poilsiaviete>(entity =>
        {
            entity.HasKey(e => e.IdPoilsiaviete).HasName("PRIMARY");

            entity.ToTable("poilsiavietes");

            entity.HasIndex(e => e.FkKodas, "ikurta");

            entity.HasIndex(e => e.Tipas, "tipas");

            entity.HasIndex(e => e.FkIdNaudotojas, "valdo");

            entity.Property(e => e.IdPoilsiaviete)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_Poilsiaviete");
            entity.Property(e => e.Adresas)
                .HasMaxLength(255)
                .HasColumnName("adresas");
            entity.Property(e => e.Aktyvumas).HasColumnName("aktyvumas");
            entity.Property(e => e.FkIdNaudotojas)
                .HasColumnType("int(11)")
                .HasColumnName("fk_id_Naudotojas");
            entity.Property(e => e.FkKodas)
                .HasColumnType("int(11)")
                .HasColumnName("fk_kodas");
            entity.Property(e => e.MiegamujuSkaicius)
                .HasColumnType("int(11)")
                .HasColumnName("miegamuju_skaicius");
            entity.Property(e => e.NaktiesKaina).HasColumnName("nakties_kaina");
            entity.Property(e => e.PastoKodas)
                .HasColumnType("int(11)")
                .HasColumnName("pasto_kodas");
            entity.Property(e => e.Pavadinimas)
                .HasMaxLength(255)
                .HasColumnName("pavadinimas");
            entity.Property(e => e.Reitingas)
                .HasMaxLength(255)
                .HasColumnName("reitingas");
            entity.Property(e => e.Taisykles)
                .HasMaxLength(255)
                .HasColumnName("taisykles");
            entity.Property(e => e.Tipas)
                .HasColumnType("int(11)")
                .HasColumnName("tipas");
            entity.Property(e => e.VoniosKambariuSk)
                .HasColumnType("int(11)")
                .HasColumnName("vonios_kambariu_sk");

            entity.HasOne(d => d.FkIdNaudotojasNavigation).WithMany(p => p.Poilsiavietes)
                .HasForeignKey(d => d.FkIdNaudotojas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("valdo");

            entity.HasOne(d => d.FkKodasNavigation).WithMany(p => p.Poilsiavietes)
                .HasForeignKey(d => d.FkKodas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ikurta");

            entity.HasOne(d => d.TipasNavigation).WithMany(p => p.Poilsiavietes)
                .HasForeignKey(d => d.Tipas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("poilsiavietes_ibfk_1");
        });

        modelBuilder.Entity<Rezervacija>(entity =>
        {
            entity.HasKey(e => e.RezNumeris).HasName("PRIMARY");

            entity.ToTable("rezervacijos");

            entity.HasIndex(e => e.Busena, "busena");

            entity.HasIndex(e => e.FkIdPoilsiaviete, "turi");

            entity.HasIndex(e => e.FkIdNaudotojas, "tvarko");

            entity.Property(e => e.RezNumeris)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("numeris");
            entity.Property(e => e.Apmoketa).HasColumnName("apmoketa");
            entity.Property(e => e.Busena)
                .HasColumnType("int(11)")
                .HasColumnName("busena");
            entity.Property(e => e.ElPastas)
                .HasMaxLength(255)
                .HasColumnName("el_pastas");
            entity.Property(e => e.FkIdNaudotojas)
                .HasColumnType("int(11)")
                .HasColumnName("fk_id_Naudotojas");
            entity.Property(e => e.FkIdPoilsiaviete)
                .HasColumnType("int(11)")
                .HasColumnName("fk_id_Poilsiaviete");
            entity.Property(e => e.Pabaiga).HasColumnName("pabaiga");
            entity.Property(e => e.Pavarde)
                .HasMaxLength(255)
                .HasColumnName("pavarde");
            entity.Property(e => e.Pradzia).HasColumnName("pradzia");
            entity.Property(e => e.SukurimoData).HasColumnName("sukurimo_data");
            entity.Property(e => e.Suma).HasColumnName("suma");
            entity.Property(e => e.TelNumeris)
                .HasMaxLength(255)
                .HasColumnName("tel_numeris");
            entity.Property(e => e.Vardas)
                .HasMaxLength(255)
                .HasColumnName("vardas");
            entity.Property(e => e.YraAugintinis).HasColumnName("yra_augintinis");
            entity.Property(e => e.ZmoniuSk)
                .HasColumnType("int(11)")
                .HasColumnName("zmoniu_sk");

            entity.HasOne(d => d.BusenaNavigation).WithMany(p => p.Rezervacijos)
                .HasForeignKey(d => d.Busena)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rezervacijos_ibfk_1");

            entity.HasOne(d => d.FkIdNaudotojasNavigation).WithMany(p => p.Rezervacijos)
                .HasForeignKey(d => d.FkIdNaudotojas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tvarko");

            entity.HasOne(d => d.FkIdPoilsiavieteNavigation).WithMany(p => p.Rezervacijos)
                .HasForeignKey(d => d.FkIdPoilsiaviete)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("turi");
        });

        modelBuilder.Entity<Tipai>(entity =>
        {
            entity.HasKey(e => e.IdTipas).HasName("PRIMARY");

            entity.ToTable("tipai");

            entity.Property(e => e.IdTipas)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_tipas");
            entity.Property(e => e.Name)
                .HasMaxLength(9)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
