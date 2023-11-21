namespace LabAcademiaTestes.Helpers;

public class TreinoHelperTestes
{
    public ITreinoHelper? C_TreinoHelper { get; set; }
    public List<Treino>? C_TreinosDesordenados { get; set; }
    public List<Treino>? C_TodosTreinos { get; set; }
    public List<Treino>? C_SemTreinos { get; set; }
    public List<Treino>? C_SemTreinoA { get; set; }

    [SetUp]
    public void Setup()
    {
        C_TreinoHelper = new TreinoHelper();
        C_TreinosDesordenados = new List<Treino>
        {
            new Treino('A', "Teste1"),
            new Treino('F', "Teste2"),
            new Treino('B', "Teste3"),
            new Treino('U', "Teste4")
        };
        C_TodosTreinos = new List<Treino>
        {
            new Treino('A', "Teste1"),
            new Treino('B', "Teste2"),
            new Treino('C', "Teste3"),
            new Treino('D', "Teste4"),
            new Treino('E', "Teste5"),
            new Treino('F', "Teste6"),
            new Treino('G', "Teste7"),
            new Treino('H', "Teste8"),
            new Treino('I', "Teste9"),
            new Treino('J', "Teste10"),
            new Treino('K', "Teste11"),
            new Treino('L', "Teste12"),
            new Treino('M', "Teste13"),
            new Treino('N', "Teste14"),
            new Treino('O', "Teste15"),
            new Treino('P', "Teste16"),
            new Treino('Q', "Teste17"),
            new Treino('R', "Teste18"),
            new Treino('S', "Teste19"),
            new Treino('T', "Teste20"),
            new Treino('U', "Teste21"),
            new Treino('V', "Teste22"),
            new Treino('W', "Teste23"),
            new Treino('X', "Teste24"),
            new Treino('Y', "Teste25"),
            new Treino('Z', "Teste26")
        };
        C_SemTreinos = new List<Treino>();
        C_SemTreinoA = new List<Treino>
        {
            new Treino('F', "Teste1"),
            new Treino('B', "Teste2"),
            new Treino('U', "Teste3")
        };
    }

    [Test]
    public void ObterNextValue()
    {
        var m_NextValue = C_TreinoHelper!.CM_IdNextValue(C_TreinosDesordenados);
        Assert.That(m_NextValue, Is.EqualTo('C'));
        Assert.Pass();
    }

    [Test]
    public void ObterExceptionLimiteIndisponivel()
    {
        try
        {
            C_TreinoHelper!.CM_IdNextValue(C_TodosTreinos);
        }
        catch (Exception ex)
        {
            Assert.That(ex.Message, Is.EqualTo("Você atingiu o limite de treinos disponíveis"));
            Assert.Pass();
        }
    }

    [Test]
    public void ObterNextValueASemTreinos()
    {
        var m_NextValueSemTreinos = C_TreinoHelper!.CM_IdNextValue(C_SemTreinos);
        Assert.That(m_NextValueSemTreinos, Is.EqualTo('A'));

        var m_NextValueParametroNull = C_TreinoHelper!.CM_IdNextValue(null);
        Assert.That(m_NextValueParametroNull, Is.EqualTo('A'));

        Assert.Pass();
    }

    [Test]
    public void ObterNextValueSemTreinoA()
    {
        var m_NextValue = C_TreinoHelper!.CM_IdNextValue(C_SemTreinoA);
        Assert.That(m_NextValue, Is.EqualTo('A'));
        Assert.Pass();
    }
}