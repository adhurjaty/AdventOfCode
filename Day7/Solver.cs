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
    private List<DirectorySize> _directorySizes = new List<DirectorySize>();

    public Solver()
    {
        var e = _lines.GetEnumerator();
        e.MoveNext();
        BuildDirectoryTree(e);
    }

    private void BuildDirectoryTree(IEnumerator<string> e)
    {
        e.MoveNext();
        string currentCommand = e.Current;

        while (!string.IsNullOrEmpty(currentCommand))
        {
            var match = Regex.Match(currentCommand, @"\$ cd (.*)");
            if (match.Success)
            {
                currentCommand = ChangeDirCommand(match.Groups[1].Value, e);
                continue;
            }

            match = Regex.Match(currentCommand, @"\$ ls");
            if (match.Success)
            {
                currentCommand = ListDirCommand(e);
                continue;
            }

            throw new Exception("Invalid command");
        }
    }

    private string ChangeDirCommand(string name, IEnumerator<string> e)
    {
        _fs.Cd(name);
        e.MoveNext();
        return e.Current;
    }

    private string ListDirCommand(IEnumerator<string> e)
    {
        while (e.MoveNext() && !e.Current.StartsWith("$"))
        {
            string lsOutput = e.Current;
            var match = Regex.Match(lsOutput, @"(\d+) (.*)");
            if (match.Success)
            {
                _fs.Touch(match.Groups[2].Value, int.Parse(match.Groups[1].Value));
                continue;
            }
            match = Regex.Match(lsOutput, @"dir (.*)");
            if (match.Success)
            {
                _fs.MkDir(match.Groups[1].Value);
                continue;
            }
        }
        return e.Current;
    }

    public int Part1()
    {
        var totalSize = GetDirectorySize(_fs.Root, "/");
        return _directorySizes.Where(x => x.Size <= 100000).Sum(x => x.Size);
    }

    public int Part2()
    {
        var spaceUsed = GetDirectorySize(_fs.Root, "/");
        var spaceToFree = spaceUsed - (TOTAL_SPACE - SPACE_NEEDED);
        return _directorySizes
            .OrderBy(x => x.Size)
            .First(x => x.Size >= spaceToFree)
            .Size;
    }

    private int GetDirectorySize(TreeDirectory dir, string path)
    {
        var size = dir.Files.Sum(x => x.Size)
            + dir.Directories.Sum(x => GetDirectorySize(x, Path.Join(path, x.Name)));
        _directorySizes.Add(new DirectorySize(path, size));
        return size;
    }
}

public record DirectorySize
(
    string Path,
    int Size
);