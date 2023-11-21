namespace LabAcademiaBiblioteca.Interfaces;

public interface IExercicioService
{
    Task CM_AlterarCargaExercicioAsync(Exercicio p_Exercicio, int p_NovaCarga);
}
