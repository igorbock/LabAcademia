namespace LabAcademiaBiblioteca.Models;

public class Treino
{
    public Treino() { }

    public Treino(string p_Nome)
    {
        Nome = p_Nome;
    }

    public Treino(char p_Id, string p_Nome)
    {
        Id = p_Id;
        Nome = p_Nome;
    }

    [PrimaryKey]
    public int Codigo { get; set; }

    public char Id { get; set; }

    [MaxLength(50)]
    public string Nome { get; set; }

    [Ignore]
    public List<Exercicio> Exercicios { get; set; }
}
