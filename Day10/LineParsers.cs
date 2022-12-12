using LanguageExt;

namespace AdventOfCode.Day10;

public interface IParser
{
    Option<InstructionResult> Execute(string command, int x);
}

public class NoopParser : LineParser, IParser
{
    public NoopParser() : base(@"^noop$") {}

    public Option<InstructionResult> Execute(string command, int x)
    {
        return GetMatch(command)
            .Map(_ => new InstructionResult(1, x));
    }
}

public class AddXParser : LineParser, IParser
{
    public AddXParser() : base(@"addx (-?\d+)") {}
    public Option<InstructionResult> Execute(string command, int x)
    {
        return GetMatch(command)
            .Map(match => new InstructionResult(2, x + int.Parse(match.Groups[1].Value)));
    }
}
