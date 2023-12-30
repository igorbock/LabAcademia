namespace LabAcademiaBiblioteca.Models;

public class Treino
{
    public Treino() { }

    public Treino(string p_Nome)
    {
        Nome = p_Nome;
    }

    public int Id { get; set; }
    public char Codigo { get; set; }
    public string? Nome { get; set; }
    public List<Exercicio> Exercicios { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? Inicio { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? Fim { get; set; }
}
