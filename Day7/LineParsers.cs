using System.Text.RegularExpressions;
using LanguageExt;
using static LanguageExt.Prelude;

namespace AdventOfCode.Day7;

public abstract class LineParser
{
    protected readonly string _pattern;
    protected readonly Filesystem _fs;

    public LineParser(Filesystem fs, string pattern)
    {
        _fs = fs;
        _pattern = pattern;
    }

    protected virtual Option<Match> GetMatch(string command)
    {
        var match = Regex.Match(command, _pattern);
        if (!match.Success)
            return None;

        return Some(match);
    }
}

public interface ICommandParser
{
    Option<string> Execute(string command, IEnumerator<string> e);
}

public class CdCommand : LineParser, ICommandParser
{
    public CdCommand(Filesystem fs) : base(fs, @"\$ cd (.*)") { }

    public Option<string> Execute(string command, IEnumerator<string> e)
    {
        return GetMatch(command)
            .Map(match =>
            {
                _fs.Cd(match.Groups[1].Value);
                e.MoveNext();
                return e.Current;
            });
    }
}

public class LsCommand : LineParser, ICommandParser
{
    private readonly List<IOutputParser> _parserStrategies;

    public LsCommand(Filesystem fs) : base(fs, @"\$ ls") 
    { 
        _parserStrategies = new List<IOutputParser>()
        {
            new FileOutput(fs),
            new DirectoryOutput(fs)
        };
    }


    public Option<string> Execute(string command, IEnumerator<string> e)
    {
        return GetMatch(command)
            .Map(match =>
            {
                while (e.MoveNext() && !e.Current.StartsWith("$"))
                {
                    _parserStrategies.First(strat => strat.Execute(e.Current));
                }
                return e.Current ?? "";
            });
    }
}


public interface IOutputParser
{
    bool Execute(string command);
}

public class DirectoryOutput : LineParser, IOutputParser
{
    private const string PATTERN = @"dir (.*)";

    public DirectoryOutput(Filesystem fs) : base(fs, @"dir (.*)") { }

    public bool Execute(string command)
    {
        return GetMatch(command)
            .Map(match =>
            {
                _fs.MkDir(match.Groups[1].Value);
                return true;
            })
            .IsSome;
    }
}

public class FileOutput : LineParser, IOutputParser
{
    public FileOutput(Filesystem fs) : base(fs, @"(\d+) (.*)") { }

    public bool Execute(string command)
    {
        return GetMatch(command)
            .Map(match =>
            {
                _fs.Touch(match.Groups[2].Value, int.Parse(match.Groups[1].Value));
                return true;
            })
            .IsSome;
    }
}
