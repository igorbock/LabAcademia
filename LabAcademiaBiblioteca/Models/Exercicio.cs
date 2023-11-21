namespace LabAcademiaBiblioteca.Models;

[Table("exercicios", Schema = "academia")]
public class Exercicio
{
    public Exercicio() { }

    public Exercicio(string p_Nome, string p_Repeticao, int p_Carga)
    {
        Nome = p_Nome;
        Repeticao = p_Repeticao;
        Carga = p_Carga;
    }

    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    [MinLength(5)]
    public string Nome { get; set; }
    public string Repeticao { get; set; }
    public int? Carga { get; set; }
}
