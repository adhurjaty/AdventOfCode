namespace AdventOfCode.Day2;

public enum Choice
{
    ROCK,
    PAPER,
    SCISSORS
}

public class RpsNode : IComparable<RpsNode>
{
    public RpsNode? Beats { get; set; }
    public RpsNode? LosesTo { get; set; }
    public Choice Choice { get; set; }

    public int CompareTo(RpsNode? other)
    {
        if (other == null)
            throw new ArgumentException("Cannot be null");

        if (other == Beats)
            return 1;
        if (other == LosesTo)
            return -1;
        return 0;
    }
}