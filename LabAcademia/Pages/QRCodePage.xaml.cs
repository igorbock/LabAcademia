namespace LabAcademia.Pages;

public partial class QRCodePage : ContentPage
{
    public QRCodePage(QRCodePageViewModel p_QRCodePageViewModel)
    {
        InitializeComponent();

        BindingContext = p_QRCodePageViewModel;
        camQRCodeView.Options = new()
        {
            AutoRotate = true
        };
    }

    private void camQRCodeView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        camQRCodeView.BarcodesDetected -= camQRCodeView_BarcodesDetected;

        Dispatcher.Dispatch(async () =>
        {
            var m_Valor = e.Results[0].Value;
            await (BindingContext as QRCodePageViewModel).CM_QRCodeDetectadoAsync(m_Valor);
        });

        camQRCodeView.BarcodesDetected += camQRCodeView_BarcodesDetected;
    }
}