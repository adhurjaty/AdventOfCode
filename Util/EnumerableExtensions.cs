namespace AdventOfCode.Util;

public static class EnumerableExtensions
{
    public static (T, T) ToTuple<T>(this IEnumerable<T> lst)
    {
        try
        {
            return (lst.First(), lst.Skip(1).First());
        }
        catch (InvalidOperationException)
        {
            throw new Exception("List is too short");
        }
    }

    public static IEnumerable<T> WhereStart<T>(this IEnumerable<T> lst, Func<T, bool> pred)
    {
        foreach (var item in lst)
        {
            if (!pred(item))
                break;
            yield return item;
        }
    }
}