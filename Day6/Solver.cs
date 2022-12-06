using AdventOfCode.Util;

namespace AdventOfCode.Day6;

public class Solver
{
    private readonly string _input = File.ReadAllText(@"./Day6/input.txt");

    public int Part1()
    {
        return FindIndexOfUniqueChars(4);
    }

    public int Part2()
    {
        return FindIndexOfUniqueChars(14);
    }

    private int FindIndexOfUniqueChars(int size)
    {
        return _input.ToCharArray()
            .SlidingBuffer(size)
            .Select(x => new HashSet<char>(x))
            .WhereStart(x => x.Count() < size)
            .Count() + size;
    }
}