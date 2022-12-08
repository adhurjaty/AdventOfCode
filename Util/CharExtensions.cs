namespace AdventOfCode.Util;

public static class CharExtensions
{
    public static int ToInt(this char c) =>
        c is >= '0' and <= '9' ? c-'0' : -1;
}