namespace LabAcademiaBiblioteca.Interfaces;

public interface ITreinoService
{
    Task CM_EscreverTreinoAsync(string p_Nome);
    Task CM_EditarTreinoAsync(Treino p_Treino);
    Task<Treino> CM_LerTreinoAsync(char p_Id);
    Treino CM_LerTreino(char p_Id);
    Task CM_ApagarTreinoAsync(char p_Id);
    IEnumerable<Treino> CM_TodosTreinos();
    Task CM_AdicionarExercicioAoTreinoAsync(char p_TreinoId, Exercicio p_Exercicio);
    Task CM_RemoverExercicioDoTreinoAsync(char p_IdTreino, Exercicio p_Exercicio);
}
