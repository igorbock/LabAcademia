namespace LabAcademiaBiblioteca.Interfaces;

public interface IPraticaService
{
    Task CM_IniciarPraticaAsync(Treino p_Treino);
    Task CM_ConcluirPraticaAsync(Treino p_Treino);
    void CM_RemoverPratica(Treino p_Treino);
}
