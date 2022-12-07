using System.Text.RegularExpressions;

namespace AdventOfCode.Day7;

public class Solver
{
    private readonly IEnumerable<string> _lines = File.ReadLines(@"./Day7/input.txt");

    private TreeDirectory _currentDirectory = new TreeDirectory()
    {
        Name = "/"
    };
    private TreeDirectory _root;

    private List<DirectorySize> _directorySizes = new List<DirectorySize>();

    public Solver()
    {
        _root = _currentDirectory;

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
        if (name == "..")
            _currentDirectory = _currentDirectory.Parent;
        else
            _currentDirectory = _currentDirectory.Directories.First(x => x.Name == name);

        if (_currentDirectory is null) throw new Exception($"null {name} directory");
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
                _currentDirectory.Files.Add(new TreeFile
                (
                    Name: match.Groups[2].Value,
                    Size: int.Parse(match.Groups[1].Value),
                    Parent: _currentDirectory
                ));
                continue;
            }
            match = Regex.Match(lsOutput, @"dir (.*)");
            if (match.Success)
            {
                _currentDirectory.Directories.Add(new TreeDirectory()
                {
                    Name = match.Groups[1].Value,
                    Parent = _currentDirectory
                });
                continue;
            }
        }
        return e.Current;
    }

    public int Part1()
    {
        var totalSize = GetDirectorySize(_root, "/");
        return _directorySizes.Where(x => x.Size <= 100000).Sum(x => x.Size);
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