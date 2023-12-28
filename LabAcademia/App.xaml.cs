namespace LabAcademia
{
    public partial class App : Application
    {
        public App(AppShell p_AppShell)
        {
            InitializeComponent();

            MainPage = p_AppShell;
        }
    }
}