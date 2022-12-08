using AdventOfCode.Util;

namespace AdventOfCode.Day8;

public class Solver
{
    private readonly int[][] _grid;

    public Solver()
    {
        var lines = File.ReadLines(@"./Day8/input.txt");
        // var lines = @"30373
        //             25512
        //             65332
        //             33549
        //             35390"
        //     .Split("\n")
        //     .Select(x => x.Trim());
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

    private IEnumerable<Vec2> GetVisibleCoords(int[][] grid)
    {
        return Enumerable.Range(1, grid.Length - 2)
            .SelectMany(y => GetVisibleCoordsInRow(grid[y], y));
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

        maxHeight = row[row.Length - 1];
        for (int x = row.Length - 2; x >= 1; x--)
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
            .Select(i => grid.Select(row => row[i]).ToArray())
            .ToArray();
    }
}