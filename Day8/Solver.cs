using AdventOfCode.Util;

namespace AdventOfCode.Day8;

public class Solver
{
    private readonly Matrix _matrix;

    public Solver()
    {
        var lines = File.ReadLines(@"./Day8/input.txt");
        var grid = lines
            .Select(line => line.ToCharArray().Select(x => x.ToInt()).ToArray())
            .ToArray();
        _matrix = new Matrix(grid);
    }

    public int Part1()
    {
        return _matrix.Coords()
            .Select(pos => _matrix.GetMatrixPivot(pos)
                .Select(dim => GetVisibleTreesFromTree(dim.ToArray()).Count())
                .Zip(DistancesToEdge(pos))
                .Any(tup => tup.Item1 == tup.Item2))
            .Count(x => x);
    }

    private int[] DistancesToEdge(Vec2 pos)
    {
        return _matrix.GetMatrixPivot(pos).Select(x => x.Count() - 1).ToArray();
    }

    public int Part2()
    {
        return _matrix.Coords(padding: 1)
            .Select(vec => GetScore(vec))
            .Max();
    }

    private int GetScore(Vec2 pos)
    {
        return _matrix.GetMatrixPivot(pos)
            .Select(view => view.Take(view.Count() - 1))
            .Select(view => GetVisibleTreesFromTree(view.ToArray()))
            .Select(trees => trees.Count() + 1)
            .Aggregate((product, count) => product * count);
    }

    private IEnumerable<int> GetVisibleTreesFromTree(int[] row)
    {
        int treeHeight = row[0];
        for (int x = 1; x < row.Length; x++)
        {
            var tree = row[x];
            if (tree >= treeHeight)
                yield break;

            yield return tree;
        }
    }
}