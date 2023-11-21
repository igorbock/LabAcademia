namespace LabAcademiaAPI.Context;

public class AcademiaDbContext : DbContext
{
    public AcademiaDbContext(DbContextOptions<AcademiaDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql("Host=isabelle.db.elephantsql.com;Port=5432;Database=dxbksyqm;User Id=dxbksyqm;Password=tg3KF4eEITi-_dwPrGFzu7VDXl2zMFaG");
    }

    public DbSet<Treino> Treinos { get; set; }
    public DbSet<Exercicio> Exercicios { get; set; }
}
