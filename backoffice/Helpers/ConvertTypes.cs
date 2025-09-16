namespace Helpers;

public static class ConvertTypes
{
    public static int ConvertStringToInt(string? valeu)
    {
        return !string.IsNullOrEmpty(valeu) ? Convert.ToInt32(valeu) : 0;
    }
}
