using CommunityToolkit.Maui.Alerts;

namespace LabAcademia.Pages;

public partial class RegistroPage : ContentPage
{
    public IUsuarioService C_UsuarioService { get; private set; }
    public string C_Matricula { get; private set; }

    public RegistroPage(string p_Matricula, IUsuarioService p_UsuarioService)
    {
        InitializeComponent();

        C_UsuarioService = p_UsuarioService;
        C_Matricula = p_Matricula;
    }

    private async void btn_Registrar_Clicked(object sender, EventArgs e)
    {
        var m_Senha = txt_Senha.Text;
        var m_ConfirmaSenha = txt_ConfirmaSenha.Text;
        var m_SenhasIguais = string.Equals(m_Senha, m_ConfirmaSenha);

        if (m_SenhasIguais == false)
        {
            var m_Toast = Toast.Make("As senhas devem ser iguais!");
            await m_Toast.Show();
            return;
        }

        await C_UsuarioService.CM_RegistrarUsuarioAsync(C_Matricula, m_Senha);
        await Navigation.PopToRootAsync();
    }
}