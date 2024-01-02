namespace LabAcademia.ViewModels;

public partial class QRCodePageViewModel : ObservableObject
{
    public IUsuarioService C_UsuarioService { get; private set; }

    public QRCodePageViewModel(IUsuarioService p_UsuarioService)
    {
        C_UsuarioService = p_UsuarioService;
    }

    [RelayCommand]
    public async Task CM_QRCodeDetectadoAsync(string p_QRCode)
    {
        var m_Parametros = new Dictionary<string, object>
        {
            { "matricula", p_QRCode }
        };

        await Shell.Current.GoToAsync(nameof(RegistroPage), true, m_Parametros);
    }
}
