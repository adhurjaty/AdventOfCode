namespace AdventOfCode.Day2;

public class Solver
{
    private readonly IEnumerable<string> _inputLines = File.ReadLines(@"./Day2/input.txt");
    private readonly PlayList _playList = new PlayList();
    private readonly Rules _rules = new Rules();

    public int Part1()
    {
        return SolveHelper(NaiveInterpretation);
    }

    public int Part2()
    {
        return SolveHelper(TrueInterpretation);
    }

    private int SolveHelper(Func<string, string, Plays> playFn)
    {
        return _inputLines
            .Select(line =>
            {
                var plays = line.Split(' ');
                return playFn(plays[0], plays[1]);
            })
            .Select(plays => 
            {
                return _rules.Score(plays.Play.CompareTo(plays.Opponent)) 
                    + _rules.MoveScore(plays.Play.Choice);
            })
            .Sum();
    }

    private Plays NaiveInterpretation(string opponentCode, string playCode)
    {
        return new Plays(
            Opponent: GetOpponentPlay(opponentCode),
            Play: _playList.GetNode(_rules.PlayCode(playCode))
        );
    }

    private Plays TrueInterpretation(string opponentCode, string playCode)
    {
        var opponent = GetOpponentPlay(opponentCode);
        return new Plays(
            Opponent: opponent,
            Play: _rules.WinLoseDrawPlay(playCode, opponent)
        );
    }

    private RpsNode GetOpponentPlay(string opponentCode)
    {
        return _playList.GetNode(_rules.OpponentCode(opponentCode));
    }

    private record Plays
    (
        RpsNode Play,
        RpsNode Opponent
    );
}