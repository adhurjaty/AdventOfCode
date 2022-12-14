namespace AdventOfCode.Util;

public record Vec2(int X, int Y)
{
    public Vec2 Swap()
    {
        return new Vec2(Y, X);
    }
}