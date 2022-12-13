using System.Numerics;

namespace AdventOfCode.Day11;

public class Solver
{
    private readonly IEnumerable<string> _chunks = File.ReadAllText(@"./Day11/testInput.txt")
        .Split("\n\n");

    public int Part1()
    {
        var monkeyCounts = _chunks
            .Select(section => new MonkeyCount(new Monkey(section, 3), 0))
            .ToArray();

        for (int i = 0; i < 20; i++)
        {
            RunRound(monkeyCounts);
        }

        return monkeyCounts
            .Select(x => x.Count)
            .OrderByDescending(x => x)
            .Take(2)
            .Aggregate((product, x) => product * x);
    }

    public int Part2()
    {
        var monkeyCounts = _chunks
            .Select(section => new MonkeyCount(new Monkey(section, 1), 0))
            .ToArray();

        for (int i = 0; i < 1000; i++)
        {
            RunRound(monkeyCounts);
        }

        var foo = monkeyCounts
            .Select(x => x.Count)
            .OrderByDescending(x => x)
            .ToList();

        return foo.Take(2).Aggregate((product, x) => product * x);
    }

    private void RunRound(MonkeyCount[] monkeyCounts)
    {
        for (int srcMonkey = 0; srcMonkey < monkeyCounts.Length; srcMonkey++)
        {
            var monkeyCount = monkeyCounts[srcMonkey];
            var items = monkeyCount.Monkey.HandleStartingItems();
            monkeyCounts[srcMonkey] = monkeyCount with
            {
                Count = monkeyCount.Count + items.Count()
            };
            foreach (var item in items)
            {
                monkeyCounts[item.Monkey].Monkey.AddItem(item.Item);
            }
        }
    }
}

public record MonkeyCount
(
    Monkey Monkey,
    int Count
);