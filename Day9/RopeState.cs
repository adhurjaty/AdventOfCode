namespace AdventOfCode.Day9;

public class RopeState
{
    private Vec2 _hPos = new Vec2(0, 0);
    private List<Vec2> _tails;
    private List<Vec2> _visited = new() { new Vec2(0, 0) };

    private readonly List<MovePosition> _positionInstructions;

    public RopeState(int numTails = 1)
    {
        _tails = Enumerable.Range(0, numTails).Select(_ => new Vec2(0, 0)).ToList();
        _positionInstructions = new List<MovePosition>()
        {
            new MovePosition(new Vec2(0, -2), Move.Down),
            new MovePosition(new Vec2(0, 2), Move.Up),
            new MovePosition(new Vec2(-2, 0), Move.Right),
            new MovePosition(new Vec2(2, 0), Move.Left),
        }.Concat(new[] 
        { 
            new Vec2(1, -2),
            new Vec2(2, -1),
            new Vec2(2, -2)
        }.Select(v => new MovePosition(v, x => Move.Left(Move.Down(x)))))
        .Concat(new[] 
        { 
            new Vec2(1, 2),
            new Vec2(2, 1),
            new Vec2(2, 2)
        }.Select(v => new MovePosition(v, x => Move.Left(Move.Up(x)))))
        .Concat(new[] 
        { 
            new Vec2(-1, -2),
            new Vec2(-2, -1),
            new Vec2(-2, -2)
        }.Select(v => new MovePosition(v, x => Move.Right(Move.Down(x)))))
        .Concat(new[] 
        { 
            new Vec2(-1, 2),
            new Vec2(-2, 1),
            new Vec2(-2, 2)
        }.Select(v => new MovePosition(v, x => Move.Right(Move.Up(x)))))
        .ToList();
    }

    public void MoveHead(Instruction instruction)
    {
        for (int _ = 0; _ < instruction.Distance; _++)
        {
            _hPos = instruction.MoveFn(_hPos);
            MoveKnot(_hPos, 0);
        }
    }

    private void MoveKnot(Vec2 prevPos, int tailIndex)
    {
        if (tailIndex == _tails.Count)
        {
            _visited.Add(prevPos);
            return;
        }

        var newTail = _positionInstructions
            .FirstOrDefault(i => i.IsAtPosition(prevPos, _tails[tailIndex]))
            ?.Move(_tails[tailIndex]);
        if (newTail is not null)
        {
            _tails[tailIndex] = newTail;
            MoveKnot(newTail, tailIndex + 1);
        }
    }

    public int TailVisited()
    {
        return _visited.Distinct().Count();
    }
}

public class MovePosition
{
    private readonly Vec2 _vecDiff;

    public Func<Vec2, Vec2> Move { get; set; }

    public MovePosition(Vec2 vecDiff, Func<Vec2, Vec2> move)
    {
        _vecDiff = vecDiff;
        Move = move;
    }

    public bool IsAtPosition(Vec2 src, Vec2 dest)
    {
        return src with 
        { 
            X = src.X + _vecDiff.X, 
            Y = src.Y + _vecDiff.Y
        } == dest;
    }
}