namespace Helpers;

public static class ConvertIdEmpresaFromStringToInt
{
    public static int GetIdEmpresaAsInt(string? idEmpresa)
    {
        return !string.IsNullOrEmpty(idEmpresa) ? Convert.ToInt32(idEmpresa) : 0;
    }
}
