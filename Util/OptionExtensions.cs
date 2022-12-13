using LanguageExt;

namespace AdventOfCode.Util;

public static class OptionExtensions
{
    public static Option<T> Tee<T>(this Option<T> option, Action<T> action)
    {
        return option.Map(x =>
        {
            action(x);
            return x;
        });
    }

    public static Option<T> TeeOption<T, U>(this Option<T> option, Func<T, Option<U>> fn)
    {
        return option.Bind(x => fn(x).Map(_ => x));
    }
}