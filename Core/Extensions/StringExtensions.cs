namespace Core.Extensions;

public static class StringExtensions
{
    public static string? CheckNullString(this string? value)
    {
        if (value == null) return null;

        return value;
    }

    public static bool CheckOutOfRange(int index, string[] array)
    {
        return (index >= 0) && (index < array.Length);
    }
}