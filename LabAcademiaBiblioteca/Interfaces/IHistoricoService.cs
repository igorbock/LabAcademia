namespace LabAcademiaBiblioteca.Interfaces;

public interface IHistoricoService
{
    Task<IEnumerable<Treino>> CM_VerHistoricoAsync(DateTime? p_Inicio, DateTime? p_Fim);
}
