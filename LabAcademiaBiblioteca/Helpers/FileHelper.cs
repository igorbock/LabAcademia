namespace LabAcademiaBiblioteca.Helpers;

public static class FileHelper
{
    public static string CM_ObterDiretorioLocalComArquivo(string p_Arquivo) => Path.Combine(FileSystem.AppDataDirectory, p_Arquivo);
}
