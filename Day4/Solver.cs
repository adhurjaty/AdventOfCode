using AdventOfCode.Util;

namespace AdventOfCode.Day4;

public class Solver
{
    private readonly IEnumerable<string> _lines = File.ReadLines(@"./Day4/input.txt");

    public int Part1()
    {
        return GetRanges()
            .Count(rangePair => rangePair.Item2.Item2 <= rangePair.Item1.Item2);
    }

    public int Part2()
    {
        return GetRanges()
            .Count(rangePair => rangePair.Item1.Item2 >= rangePair.Item2.Item1);
    }

    private IEnumerable<((int, int), (int, int))> GetRanges()
    {
        return _lines
            .Select(line => line.Split(',')
                .Select(part => part.Split('-').Select(x => int.Parse(x)).ToTuple())
                .OrderBy(range => range.Item1)
                .ThenByDescending(range => range.Item2)
                .ToTuple());
    }
}
