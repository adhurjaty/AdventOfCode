namespace AdventOfCode;

public static class Day1
{
    public static int Part1(string inputText)
    {
        return SumGroups(inputText)
            .Max();
    }

    public static int Part2(string inputText)
    {
        return SumGroups(inputText)
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();
    }

    private static IEnumerable<int> SumGroups(string inputText)
    {
        return inputText
            .Split("\n\n")
            .Select(group => group
                .Split("\n")
                .Select(line => int.Parse(line))
                .Sum());
    }
}