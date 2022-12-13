using System.Numerics;

namespace AdventOfCode.Day11;

public class Monkey
{
    private readonly int _worryLevel;
    private List<int> _startingItems;
    private Func<int, int> _operation;
    private int _testDivisor;
    private int _monkeyIfTrue;
    private int _monkeyIfFalse;

    public Monkey(string description, int worryLevel)
    {
        BuildMonkey(description);
        _worryLevel = worryLevel;
    }

    private void BuildMonkey(string description)
    {
        var lines = description.Split("\n")
            .Select(x => x.Trim())
            .Skip(1)
            .ToArray();

        new StartingItemsParser().Execute(lines[0])
            .Tee(items => _startingItems = items)
            .Bind(_ => new OperationParser().Execute(lines[1]))
            .Tee(operation => _operation = operation)
            .Bind(_ => new TestParser().Execute(lines[2]))
            .Tee(divisor => _testDivisor = divisor)
            .Bind(_ => new ResultParser().Execute(lines[3]))
            .Tee(res => _monkeyIfTrue = res)
            .Bind(_ => new ResultParser().Execute(lines[4]))
            .Tee(res => _monkeyIfFalse = res)
            .Match(x => x, () => throw new Exception("Parsing error"));
    }

    public IEnumerable<ItemToMonkey> HandleStartingItems()
    {
        var items = _startingItems
            .Select(item =>
            {
                var newValue = _operation(item) / _worryLevel;
                return new ItemToMonkey
                (
                    Item: newValue,
                    Monkey: newValue % _testDivisor == 0
                        ? _monkeyIfTrue
                        : _monkeyIfFalse
                );
            });

        _startingItems = new List<int>();
        return items;
    }

    public void AddItem(int item)
    {
        _startingItems.Add(item);
    }
}

public record ItemToMonkey
(
    int Item,
    int Monkey
);