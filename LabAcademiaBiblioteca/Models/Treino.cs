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

    public char Id { get; set; }
    public string Nome { get; set; }
    public List<Exercicio> Exercicios { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? Inicio { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? Fim { get; set; }
}
