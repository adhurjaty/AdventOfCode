namespace AdventOfCode.Day1;

public class Solver
{
    private readonly string _inputText = File.ReadAllText(@"./Day1/input.txt");

    public int Part1()
    {
        return SumGroups(_inputText)
            .Max();
    }

    public int Part2()
    {
        return SumGroups(_inputText)
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();
    }

    private IEnumerable<int> SumGroups(string inputText)
    {
        return inputText
            .Split("\n\n")
            .Select(group => group
                .Split("\n")
                .Select(line => int.Parse(line))
                .Sum());
    }
}