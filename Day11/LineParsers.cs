using LanguageExt;

namespace AdventOfCode.Day11;

public class StartingItemsParser : LineParser
{
    public StartingItemsParser() : base(@"Starting items: (?:(\d+),?\s*)+") { }

    public Option<List<int>> Execute(string line)
    {
        return GetMatch(line)
            .Map(match => Enumerable.Range(0, match.Groups[1].Captures.Count)
                .Select(i => int.Parse(match.Groups[1].Captures[i].Value))
                .ToList());
    }
}

public class OperationParser : LineParser
{
    public OperationParser() : base(@"Operation: new = old ([+*]) (\d+|old)") { }

    public Option<Func<int, int>> Execute(string line)
    {
        return GetMatch(line)
            .Map(match =>
            {
                var secondTerm = match.Groups[2].Value switch
                {
                    "old" => Old,
                    string s => Num(s),
                    _ => throw new Exception("Not reachable")
                };

                return match.Groups[1].Value switch
                {
                    "+" => Operation(Add, secondTerm),
                    "*" => Operation(Multiply, secondTerm),
                    _ => throw new Exception("Invalid operation")
                };
            });
    }

    private int Old(int x) => x;
    private Func<int, int> Num(string s)
    {
        return _ => int.Parse(s);
    } 

    // need these to get the Map body to compile for some reason
    private int Add(int x, int y) => x + y;
    private int Multiply(int x, int y) => x * y;
    private Func<int, int> Operation(Func<int, int, int> operation, Func<int, int> secondTerm)
    {
        return x => operation(x, secondTerm(x));
    }
}

public class TestParser : LineParser
{
    public TestParser() : base(@"Test: divisible by (\d+)") { }

    public Option<int> Execute(string line)
    {
        return GetMatch(line)
            .Map(match => int.Parse(match.Groups[1].Value));
    }
}

public class ResultParser : LineParser
{
    public ResultParser() : base(@"If (?:true|false): throw to monkey (\d+)") { }

    public Option<int> Execute(string line)
    {
        return GetMatch(line)
            .Map(match => int.Parse(match.Groups[1].Value));
    }
}
