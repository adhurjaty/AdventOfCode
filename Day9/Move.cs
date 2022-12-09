namespace AdventOfCode.Day9;

public static class Move
{
    public static Vec2 Up(Vec2 vec, int distance)
    {
        return vec with { Y = vec.Y - distance };
    }

    public static Vec2 Down(Vec2 vec, int distance)
    {
        return vec with { Y = vec.Y + distance };
    }

    public static Vec2 Left(Vec2 vec, int distance)
    {
        return vec with { X = vec.X - distance };
    }

    public static Vec2 Right(Vec2 vec, int distance)
    {
        return vec with { X = vec.X + distance };
    }
}