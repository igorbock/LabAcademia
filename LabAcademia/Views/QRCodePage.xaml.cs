namespace LabAcademia.Pages;

public partial class QRCodePage : ContentPage
{
    public IUsuarioService C_UsuarioService { get; private set; }

    public QRCodePage(IUsuarioService p_UsuarioService)
    {
        InitializeComponent();

        C_UsuarioService = p_UsuarioService;

        camQRCodeView.Options = new()
        {
            AutoRotate = true
        };
    }

	private void camQRCodeView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
	{
		Dispatcher.Dispatch(async () =>
		{
			var m_Valor = e.Results[0].Value;
			var m_Toast = Toast.Make($"O QRCode foi detectado! O valor é: '{m_Valor}'");
			await m_Toast.Show();

			await Navigation.PushAsync(new RegistroPage(m_Valor, C_UsuarioService));
		});		
	}
}