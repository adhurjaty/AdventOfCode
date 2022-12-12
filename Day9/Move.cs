namespace AdventOfCode.Day9;

public static class Move
{
    public static Vec2 Up(Vec2 vec)
    {
        return vec with { Y = vec.Y - 1 };
    }

    public static Vec2 Down(Vec2 vec)
    {
        return vec with { Y = vec.Y + 1 };
    }

    public static Vec2 Left(Vec2 vec)
    {
        return vec with { X = vec.X - 1 };
    }

    public static Vec2 Right(Vec2 vec)
    {
        return vec with { X = vec.X + 1 };
    }
}