namespace AdventOfCode.Day2;

public class Rules
{
    private readonly Dictionary<string, Choice> _opponentCode = new()
    {
        { "A", Choice.ROCK },
        { "B", Choice.PAPER },
        { "C", Choice.SCISSORS }
    };

    private readonly Dictionary<string, Choice> _playCode = new()
    {
        { "X", Choice.ROCK },
        { "Y", Choice.PAPER },
        { "Z", Choice.SCISSORS }
    };

    private readonly Dictionary<Choice, int> _moveScore = new()
    {
        { Choice.ROCK, 1 },
        { Choice.PAPER, 2 },
        { Choice.SCISSORS, 3 }
    };

    private readonly Dictionary<int, int> _scoring = new()
    {
        { -1, 0 },
        { 0, 3 },
        { 1, 6 }
    };

    private readonly Dictionary<string, Func<RpsNode, RpsNode>> _playWinLoseDraw = new()
    {
        { "X", node => node.Beats! },
        { "Y", node => node },
        { "Z", node => node.LosesTo! }
    };

    public Choice OpponentCode(string code)
    {
        return _opponentCode[code];
    }

    public Choice PlayCode(string code)
    {
        return _playCode[code];
    }

    public int MoveScore(Choice choice)
    {
        return _moveScore[choice];
    }

    public int Score(int winLoseDraw)
    {
        return _scoring[winLoseDraw];
    }

    public RpsNode WinLoseDrawPlay(string code, RpsNode opponent)
    {
        return _playWinLoseDraw[code](opponent);
    }
}