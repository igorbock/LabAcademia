namespace LabAcademiaBiblioteca.Helpers;

public class TreinoHelper : ITreinoHelper
{
    public void CM_SelecionarTreinoEAdicionarExercicio(char p_TreinoId, IEnumerable<Treino> p_Treinos, Exercicio p_Exercicio)
    {
        var m_TreinoSelecionado = p_Treinos.FirstOrDefault(a => a.Codigo == p_TreinoId);
        if (m_TreinoSelecionado.Exercicios == null)
        {
            m_TreinoSelecionado.Exercicios = new List<Exercicio>
            {
                p_Exercicio
            };
        }
        else
            m_TreinoSelecionado.Exercicios.Add(p_Exercicio);
    }

    public void CM_SelecionarTreinoERemover(char p_TreinoId, List<Treino> p_Treinos)
    {
        var m_Treino = p_Treinos.First(a => a.Codigo == p_TreinoId) ?? throw new KeyNotFoundException();
        p_Treinos.Remove(m_Treino);
    }

    public void CM_SelecionarTreinoERemoverExercicio(char p_TreinoId, IEnumerable<Treino> p_Treinos, Exercicio p_Exercicio)
    {
        var m_TreinoSelecionado = p_Treinos.FirstOrDefault(a => a.Codigo == p_TreinoId);

        var m_ExercicioParaRemover = m_TreinoSelecionado.Exercicios.First(a => a.Descricao == p_Exercicio.Descricao) ?? throw new KeyNotFoundException();
        m_TreinoSelecionado.Exercicios.Remove(m_ExercicioParaRemover);
    }

    public char CM_IdNextValue(IEnumerable<Treino> p_Treinos)
    {
        var m_TreinosEhNullOuVazio = p_Treinos == null || p_Treinos.Count() == 0;
        if (m_TreinosEhNullOuVazio)
            return 'A';

        var m_IdsEmASCII = p_Treinos.Select(a => Convert.ToByte(a.Codigo)).Order();

        var m_PrimeiroIdNaoEhA = m_IdsEmASCII.First() != 65;
        if (m_PrimeiroIdNaoEhA)
            return 'A';

        var m_UltimoId = m_IdsEmASCII.Last();
        var m_UltimoIdEhMaiorOuIgualZ = m_UltimoId >= 90;
        if (m_UltimoIdEhMaiorOuIgualZ)
            throw new Exception("Você atingiu o limite de treinos disponíveis");

        bool m_IdJaExistente;
        for(byte id = 65;id < 90;id++)
        {
            m_IdJaExistente = m_IdsEmASCII.Any(a => a.Equals(id));
            if (m_IdJaExistente == false)
                return Convert.ToChar(id);
        }

        throw new Exception("Você atingiu o limite de treinos disponíveis");
    }
}
