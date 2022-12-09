using System.Text.RegularExpressions;
using LanguageExt;
using static LanguageExt.Prelude;

namespace AdventOfCode.Day7;

public interface ICommandParser
{
    Option<string> Execute(string command, IEnumerator<string> e);
}

public class CdCommand : LineParser, ICommandParser
{
    private readonly Filesystem _fs;
    public CdCommand(Filesystem fs) : base(@"\$ cd (.*)")
    { 
        _fs = fs;
    }

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
    private readonly Filesystem _fs;
    private readonly List<IOutputParser> _parserStrategies;

    public LsCommand(Filesystem fs) : base(@"\$ ls") 
    { 
        _fs = fs;
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
    private readonly Filesystem _fs;

    public DirectoryOutput(Filesystem fs) : base(@"dir (.*)") 
    {
        _fs = fs;
    }

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
    private readonly Filesystem _fs;
    public FileOutput(Filesystem fs) : base(@"(\d+) (.*)")
    {
        _fs = fs;
    }

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
