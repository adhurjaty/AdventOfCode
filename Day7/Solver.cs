using System.Text.RegularExpressions;

namespace AdventOfCode.Day7;

public class Solver
{
    private const int TOTAL_SPACE = 70000000;
    private const int SPACE_NEEDED = 30000000;

    private readonly IEnumerable<string> _lines = File.ReadLines(@"./Day7/input.txt");

    private readonly Filesystem _fs = new Filesystem(new TreeDirectory()
    {
        Name = "/"
    });

    public Solver()
    {
        var e = _lines.GetEnumerator();
        BuildDirectoryTree(e);
    }

    private void BuildDirectoryTree(IEnumerator<string> e)
    {
        var strategies = new ICommandParser[]
        {
            new CdCommand(_fs),
            new LsCommand(_fs)
        };

        e.MoveNext();
        string currentCommand = e.Current;

        while (!string.IsNullOrEmpty(currentCommand))
        {
            currentCommand = strategies
                .Select(x => x.Execute(currentCommand, e))
                .First(x => x.IsSome)
                .Match(cmd => cmd, () => throw new Exception("No matching strategy"));
        }
    }

    public int Part1()
    {
        return GetDirectorySizes(_fs.Root, "/")
            .Where(x => x.Size <= 100000)
            .Sum(x => x.Size);
    }

    public int Part2()
    {
        var spaceUsed = _fs.Root.Size;
        var spaceToFree = spaceUsed - (TOTAL_SPACE - SPACE_NEEDED);
        return GetDirectorySizes(_fs.Root, "/")
            .OrderBy(x => x.Size)
            .First(x => x.Size >= spaceToFree)
            .Size;
    }

    private IEnumerable<DirectorySize> GetDirectorySizes(TreeDirectory dir, string path)
    {
        return new[] { new DirectorySize(path, dir.Size) }
            .Concat(dir.Directories.SelectMany(x => GetDirectorySizes(x, Path.Join(path, x.Name))));
    }
}

public record DirectorySize
(
    string Path,
    int Size
);