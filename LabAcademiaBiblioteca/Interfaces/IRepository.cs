namespace LabAcademiaBiblioteca.Interfaces;

public interface IRepository<TipoT>
{
    Task CM_SalvarAsync(TipoT p_Entidade);
    Task CM_EditarAsync(TipoT p_Entidade);
    Task CM_RemoverAsync(char p_Id);
    Task CM_RemoverAsync(int p_Id);
    Task<TipoT> CM_LerAsync(char p_Id);
    Task<TipoT> CM_LerAsync(int p_Id);
    Task<IEnumerable<TipoT>> CM_LerTodosAsync();
    IEnumerable<TipoT> CM_LerTodos();
}
