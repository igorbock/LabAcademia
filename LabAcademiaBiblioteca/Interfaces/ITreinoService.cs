namespace LabAcademiaBiblioteca.Interfaces;

public interface ITreinoService
{
    Task<IEnumerable<Treino>> CM_TodosTreinosAsync(string p_Matricula);
    Task<IEnumerable<Exercicio>> CM_ObterExerciciosDoTreinoAsync(int p_CodigoTreino);
}
