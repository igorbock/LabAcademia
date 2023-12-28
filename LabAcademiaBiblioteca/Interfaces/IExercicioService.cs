namespace LabAcademiaBiblioteca.Interfaces;

public interface IExercicioService
{
    Task CM_AlterarCargaExercicioAsync(Exercicio p_Exercicio, double p_NovaCarga);
}
