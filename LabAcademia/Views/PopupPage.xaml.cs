namespace LabAcademia.Pages;

public partial class PopupPage : Popup
{
    public PopupPage(Exercicio p_Exercicio)
    {
        InitializeComponent();

        var m_BindingNome = new Binding();
        m_BindingNome.Source = p_Exercicio;
        m_BindingNome.Path = "Nome";

        C_Nome.SetBinding(Editor.TextProperty, m_BindingNome);

        var m_BindingRepeticao = new Binding();
        m_BindingRepeticao.Source = p_Exercicio;
        m_BindingRepeticao.Path = "Repeticao";

        C_Repeticao.SetBinding(Editor.TextProperty, m_BindingRepeticao);

        var m_BindingCarga = new Binding();
        m_BindingCarga.Source = p_Exercicio;
        m_BindingCarga.Path = "Carga";

        C_Carga.SetBinding(Editor.TextProperty, m_BindingCarga);
    }
}