namespace LabAcademiaBiblioteca.Models;

public class Exercicio
{
    public Exercicio() { }

    public Exercicio(string p_Nome, string p_Repeticao, int p_Carga)
    {
        Nome = p_Nome;
        Repeticao = p_Repeticao;
        Carga = p_Carga;
    }

    public string Nome { get; set; }
    public string Repeticao { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Carga { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Concluido { get; set; }
}
