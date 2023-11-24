namespace LabAcademia.Pages;

public partial class PraticaPage : ContentPage
{
    public Treino C_Treino { get; set; }

    public PraticaPage(Treino p_Treino)
	{
		InitializeComponent();
        C_Treino = p_Treino;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        lstVw_Pratica.ItemsSource = C_Treino.Exercicios;
    }
}