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

    public static IEnumerable<T>[] SplitParts<T>(this IEnumerable<T> lst,
        params Func<T, bool>[] preds)
    {
        var e = lst.GetEnumerator();
        if (!e.MoveNext())
            throw new ArgumentNullException();

        // maximal lazy implementation that doesn't cause issues when accessing by index
        return preds.Take(preds.Length - 1)
            .Select(pred => GetPart(e, pred).ToArray())
            .Concat(new[] { GetPart(e, preds[preds.Length - 1]) })
            .ToArray();
    }

    private static IEnumerable<T> GetPart<T>(IEnumerator<T> e, Func<T, bool> pred)
    {
        bool hasFound = false;
        do
        {
            bool isMatch = pred(e.Current);
            if (isMatch)
            {
                hasFound = true;
                yield return e.Current;
            }
            if (!isMatch && hasFound)
                break;
        } while (e.MoveNext());
    }
}