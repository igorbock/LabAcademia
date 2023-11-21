namespace LabAcademiaBiblioteca.Interfaces;

public interface ITreinoHelper
{
    void CM_SelecionarTreinoEAdicionarExercicio(char p_TreinoId, IEnumerable<Treino> p_Treinos, Exercicio p_Exercicio);
    void CM_SelecionarTreinoERemoverExercicio(char p_TreinoId, IEnumerable<Treino> p_Treinos, Exercicio p_Exercicio);
    void CM_SelecionarTreinoERemover(char p_TreinoId, List<Treino> p_Treinos);
    char CM_IdNextValue(IEnumerable<Treino> p_Treinos);
}
