namespace AdventOfCode.Day8;

public class Matrix
{
    private readonly int[][] _grid;

    public int Width => _grid[0].Length;
    public int Height => _grid.Length;

    public Matrix(int[][] grid)
    {
        _grid = grid;
    }

    public IEnumerable<Vec2> Coords(int padding = 0)
    {
        return Enumerable.Range(padding, _grid.Length - 2 * padding)
            .SelectMany(y => Enumerable.Range(padding, _grid[0].Length - 2 * padding)
                .Select(x => new Vec2(x, y)));
    }

    public IEnumerable<int>[] GetMatrixPivot(Vec2 pos)
    {
        return RowAboutX(_grid[pos.Y], pos.X)
            .Concat(RowAboutX(GetColumn(pos.X), pos.Y))
            .ToArray();
    }

    public IEnumerable<int>[] RowAboutX(IEnumerable<int> row, int x)
    {
        return new[]
        {
            row.Take(x + 1).Reverse(),
            row.Skip(x)
        };
    }

    private int[] GetColumn(int x)
    {
        return _grid.Select(row => row[x]).ToArray();
    }
}