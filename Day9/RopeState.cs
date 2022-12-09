namespace AdventOfCode.Day9;

public class RopeState
{
    private Vec2 _hPos = new Vec2(0, 0);
    private Vec2 _tPos = new Vec2(0, 0);
    private List<Vec2> _visited = new() { new Vec2(0, 0) };

    public void MoveHead(Instruction instruction)
    {
        _hPos = instruction.Execute(_hPos, instruction.Distance);
        var tDistance = DistanceOutside(_hPos, _tPos);

        _visited.AddRange(Enumerable.Range(1, tDistance).Select(d => instruction.Execute(_tPos, d)));
        _tPos = _visited.Last();
    }

    private int DistanceOutside(Vec2 origin, Vec2 target, int distance = 1)
    {
        return new[]
        {
            origin.X - target.X,
            target.X - origin.X,
            origin.Y - target.Y,
            target.Y - origin.Y
        }
        .Select(d => d - distance)
        .Select(d => Math.Max(0, d))
        .Max();
    }

    public int TailVisited()
    {
        return _visited.Distinct().Count();
    }
}