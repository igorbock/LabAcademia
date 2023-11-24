namespace LabAcademia.Repositories;

public class TreinoRepository
{
    public IStreamHelper<Treino> C_StreamHelper { get; set; }
    public ITreinoHelper C_TreinoHelper { get; set; }
    public string C_DiretorioDB { get; set; }

    public SQLiteAsyncConnection C_Connection { get; set; }

    public TreinoRepository(string p_DiretorioDB)
    {
        C_DiretorioDB = p_DiretorioDB;
    }

    public async Task CM_IniciarConexao()
    {
        if (C_Connection != null)
            return;

        C_Connection = new SQLiteAsyncConnection(C_DiretorioDB);
        await C_Connection.CreateTableAsync<Treino>();
    }

    public async Task CM_AdicionarAsync(Treino p_Treino)
    {
        await CM_IniciarConexao();

        try
        {
            await C_Connection.InsertAsync(p_Treino);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Treino>> CM_ObterAsync()
    {
        await CM_IniciarConexao();

        try
        {
            return await C_Connection.Table<Treino>().ToListAsync();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task CM_EditarAsync(Treino p_Treino)
    {
        await CM_IniciarConexao();

        try
        {
            await C_Connection.UpdateAsync(p_Treino);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task CM_RemoverAsync(Treino p_Treino)
    {
        await CM_IniciarConexao();

        try
        {
            await C_Connection.DeleteAsync(p_Treino);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
