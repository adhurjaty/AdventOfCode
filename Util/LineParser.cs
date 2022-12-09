using System.Text.RegularExpressions;
using LanguageExt;
using static LanguageExt.Prelude;

namespace AdventOfCode.Util;

public abstract class LineParser
{
    private readonly string _pattern;

    public LineParser(string pattern)
    {
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