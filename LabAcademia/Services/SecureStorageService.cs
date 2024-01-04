namespace LabAcademia.Services;

public class SecureStorageService : ISecureStorageService
{
    public async Task<string> CM_ObterAsync(string p_Chave) => await SecureStorage.GetAsync(p_Chave);

    public async Task CM_SalvarAsync(string p_Chave, string p_Valor) => await SecureStorage.SetAsync(p_Chave, p_Valor);

    public void CM_Remover(string p_Chave) => SecureStorage.Remove(p_Chave);

    public void CM_RemoverTudo() => SecureStorage.RemoveAll();
}
