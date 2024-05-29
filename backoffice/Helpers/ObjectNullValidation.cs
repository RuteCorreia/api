namespace Helpers;

public static class ObjectNullValidation
{

    public static bool IsObjectNull<T>(T obj)
    {
        if(obj == null)
        {
            return true;
        }

        return false;
    }

}
