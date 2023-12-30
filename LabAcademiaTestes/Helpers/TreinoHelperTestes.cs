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
            new Treino("Teste1"),
            new Treino("Teste2"),
            new Treino("Teste3"),
            new Treino("Teste4")
        };
        C_TodosTreinos = new List<Treino>
        {
            new Treino("Teste1"),
            new Treino("Teste2"),
            new Treino("Teste3"),
            new Treino("Teste4"),
            new Treino("Teste5"),
            new Treino("Teste6"),
            new Treino("Teste7"),
            new Treino("Teste8"),
            new Treino("Teste9"),
            new Treino("Teste10"),
            new Treino("Teste11"),
            new Treino("Teste12"),
            new Treino("Teste13"),
            new Treino("Teste14"),
            new Treino("Teste15"),
            new Treino("Teste16"),
            new Treino("Teste17"),
            new Treino("Teste18"),
            new Treino("Teste19"),
            new Treino("Teste20"),
            new Treino("Teste21"),
            new Treino("Teste22"),
            new Treino("Teste23"),
            new Treino("Teste24"),
            new Treino("Teste25"),
            new Treino("Teste26")
        };
        C_SemTreinos = new List<Treino>();
        C_SemTreinoA = new List<Treino>
        {
            new Treino("Teste1"),
            new Treino("Teste2"),
            new Treino("Teste3")
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