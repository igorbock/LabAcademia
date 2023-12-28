namespace LabAcademiaBiblioteca.Models;

public class Exercicio
{
    public Exercicio() { }

    public Exercicio(string p_Descricao, int p_Repeticao, int p_Carga)
    {
        Descricao = p_Descricao;
        Repeticao = p_Repeticao;
        Carga = p_Carga;
    }

    public int Id { get; set; }
    public string Descricao { get; set; }
    public int? Series { get; set; }
    public int? Repeticao { get; set; }
    public double? Tempo { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Carga { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Concluido { get; set; }
    public int CodigoTreino { get; set; }
}
