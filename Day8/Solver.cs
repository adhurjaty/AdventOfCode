using AdventOfCode.Util;

namespace AdventOfCode.Day8;

public class Solver
{
    private readonly int[][] _grid;

    public Solver()
    {
        var lines = File.ReadLines(@"./Day8/input.txt");
        _grid = lines
            .Select(line => line.ToCharArray().Select(x => x.ToInt()).ToArray())
            .ToArray();
    }

    public int Part1()
    {
        return GetVisibleCoords(_grid)
            .Concat(GetVisibleCoords(Transpose(_grid))
                .Select(coord => coord.Swap()))
            .Distinct()
            .Count()
            + 2 * _grid.Length + 2 * _grid.First().Length - 4; // for the outer edges
    }

    public int Part2()
    {
        return Enumerable.Range(1, _grid.Length - 2)
            .SelectMany(y => Enumerable.Range(1, _grid[0].Length - 2)
                .Select(x => new Vec2(x, y)))
            .Select(vec => GetScore(vec))
            .Max();
    }

    private IEnumerable<Vec2> GetVisibleCoords(int[][] grid)
    {
        return Enumerable.Range(1, grid.Length - 2)
            .SelectMany(y => GetVisibleCoordsInRowBothSides(grid[y], y));
    }

    private IEnumerable<Vec2> GetVisibleCoordsInRowBothSides(int[] row, int y)
    {
        return GetVisibleCoordsInRow(row, y)
            .Concat(GetVisibleCoordsInRow(row.Reverse().ToArray(), y)
                .Select(vec => new Vec2(row.Length - 1 - vec.X, vec.Y)));
    }

    private IEnumerable<Vec2> GetVisibleCoordsInRow(int[] row, int y)
    {
        int maxHeight = row[0];
        for (int x = 1; x < row.Length - 1; x++)
        {
            var tree = row[x];
            if (tree > maxHeight)
            {
                yield return new Vec2(x, y);
                maxHeight = tree;
            }
        }
    }

    private int[][] Transpose(int[][] grid)
    {
        return Enumerable.Range(0, grid.Length)
            .Select(i => GetColumn(grid, i))
            .ToArray();
    }

    private int[] GetColumn(int[][] grid, int x)
    {
        return grid.Select(row => row[x]).ToArray();
    }

    private int GetScore(Vec2 pos)
    {
        return GetRowScore(_grid[pos.Y], pos.X)
            * GetRowScore(GetColumn(_grid, pos.X), pos.Y);
    }

    private int GetRowScore(int[] row, int x)
    {
        return RowView(row, x)
            .Select(view => GetVisibleTreesFromTree(view.ToArray()))
            .Select(trees => trees.Count() + 1)
            .Aggregate((product, count) => product * count);
    }

    private IEnumerable<int>[] RowView(IEnumerable<int> row, int x)
    {
        return new[]
        {
            row.Take(x + 1).Reverse(),
            row.Skip(x)
        };
    }

    private IEnumerable<int> GetVisibleTreesFromTree(int[] row)
    {
        int treeHeight = row[0];
        for (int x = 1; x < row.Length - 1; x++)
        {
            var tree = row[x];
            if (tree >= treeHeight)
                yield break;

            yield return tree;
        }
    }
}