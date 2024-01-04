namespace LabAcademiaBiblioteca.Interfaces;

public interface ISecureStorageService
{
    Task CM_SalvarAsync(string p_Chave, string p_Valor);
    Task<string> CM_ObterAsync(string p_Chave);
    void CM_Remover(string p_Chave);
    void CM_RemoverTudo();
}
