namespace AdventOfCode.Day2;

public class Solver
{
    private readonly IEnumerable<string> _inputLines = File.ReadLines(@"./Day2/input.txt");

    private readonly Dictionary<string, Play> _opponentCode = new()
    {
        { "A", new Rock() },
        { "B", new Paper() },
        { "C", new Scissors() }
    };

    private readonly Dictionary<string, Play> _playCode = new()
    {
        { "X", new Rock() },
        { "Y", new Paper() },
        { "Z", new Scissors() }
    };

    private readonly Dictionary<int, int> _scoring = new()
    {
        { -1, 0 },
        { 0, 3 },
        { 1, 6 }
    };

    private readonly Dictionary<string, int> _playWinLoseDraw = new()
    {
        { "X", -1 },
        { "Y", 0 },
        { "Z", 1 }
    };

    public int Part1()
    {
        return _inputLines
            .Select(line =>
            {
                var plays = line.Split(' ');
                var opponent = _opponentCode[plays[0]];
                var play = _playCode[plays[1]];
                return _scoring[play.CompareTo(opponent)] + play.Points;
            })
            .Sum();
    }

    public int Part2()
    {
        return _inputLines
            .Select(line =>
            {
                var plays = line.Split(' ');
                var opponent = _opponentCode[plays[0]];
                var comparison = _playWinLoseDraw[plays[1]];
                var play = opponent.GetComparePlay(comparison);
                return _scoring[comparison] + play.Points;
            })
            .Sum();
    }
}