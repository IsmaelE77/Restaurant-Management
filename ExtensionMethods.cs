namespace Restaurant_Management;

public static class ExtensionMethods
{
    public static string ConvertToString<T>(this List<T> list)
    {
        if (list.Count == 0) return "[]";
        return $"\n[\n\t{string.Join("\n\t", list)}\n]";
    }
}