namespace LabAcademiaBiblioteca.Helpers;

public static class DateTimeHelper
{
    public static TimeSpan CM_ObterDiferenca(DateTime p_Inicio, DateTime p_Fim) => p_Fim - p_Inicio;
}
