namespace AdventOfCode.Day3;

public class Solver
{
    private readonly IEnumerable<string> _lines = File.ReadLines(@"./Day3/input.txt");

    public int Part1()
    {
        var chunks = _lines
            .Select(line => line.ToCharArray())
            .Select(chars => chars.Chunk(chars.Length / 2));
        return CalculateChunksSum(chunks);
    }

    public int Part2()
    {
        var chunks = _lines
            .Chunk(3)
            .Select(chunk => chunk.Select(x => x.ToCharArray()));
        return CalculateChunksSum(chunks);
    }

    private int CalculateChunksSum(IEnumerable<IEnumerable<IEnumerable<char>>> chunks)
    {
        return chunks
            .Select(chunk => 
                chunk.Aggregate((intersection, x) => intersection.Intersect(x)))
            .SelectMany(intersection => intersection.Select(Priority))
            .Sum();
    }

    private int Priority(char c)
    {
        return (int)c > 90
            ? (int)c - 96
            : (int)c - 38;
    }
}