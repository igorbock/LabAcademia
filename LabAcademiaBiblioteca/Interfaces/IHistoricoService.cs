namespace LabAcademiaBiblioteca.Interfaces;

public interface IHistoricoService
{
    Task<IEnumerable<Treino>> CM_VerHistoricoAsync();
}
