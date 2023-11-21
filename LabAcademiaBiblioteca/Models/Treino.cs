namespace LabAcademiaBiblioteca.Models;

[Table("treinos", Schema = "academia")]
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

    [Key]
    public char Id { get; set; }

    [MaxLength(100)]
    [MinLength(5)]
    public string Nome { get; set; }
    public List<Exercicio> Exercicios { get; set; }
}
