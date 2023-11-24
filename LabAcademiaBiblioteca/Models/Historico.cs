namespace LabAcademiaBiblioteca.Models;

public class Historico
{
    public Historico() { }

    public int Id { get; set; }
    public DateTime Execucao { get; set; }
    public Treino Treino { get; set; }
}
