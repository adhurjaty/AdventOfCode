using System.Text.RegularExpressions;

namespace AdventOfCode.Day10;

public class Solver
{
    private readonly IEnumerable<string> _lines = File.ReadLines(@"./Day10/input.txt");

    public int Part1()
    {
        var significantCycles = new[] { 20, 60, 100, 140, 180, 220 };
        return _lines
            .Select(ToInstruction)
            .SelectWithPrevResult((state, instruction) => state with
            {
                Cycle = state.Cycle + instruction.Cycle,
                Register = state.Register + instruction.Register
            }, new Instruction(0, 1))
            .SlidingBuffer(2)
            .Select(x => x.ToTuple())
            .Select(steps => significantCycles
                .FirstOption(cycle => steps.Item1.Cycle < cycle && steps.Item2.Cycle >= cycle)
                .Match(sig => sig * steps.Item1.Register, () => 0))
            .Sum();
    }

    public string Part2()
    {
        return string.Join("\n", _lines
            .Select(ToInstruction)
            .SelectWithPrevResult((state, instruction) => state with
            {
                Cycle = state.Cycle + instruction.Cycle,
                Register = state.Register + instruction.Register
            }, new Instruction(0, 1))
            .SlidingBuffer(2)
            .Select(x => x.ToTuple())
            .SelectMany(states =>
            {
                var (start, end) = states;
                var stepDuration = end.Cycle - start.Cycle;

                return Enumerable.Range(start.Cycle, stepDuration)
                    .Select(cycle => GetMark(start.Register, cycle % 40));
            })
            .Chunk(40)
            .Select(x => string.Join("", x)));
    }

    private Instruction ToInstruction(string command)
    {
        if (command == "noop")
            return new Instruction(1, 0);
        var match = Regex.Match(command, @"addx (-?\d+)");
        if (match.Success)
            return new Instruction(2, int.Parse(match.Groups[1].Value));
        throw new Exception("Invalid command");
    }

    private string GetMark(int register, int cycle)
    {
        return cycle >= register - 1 && cycle <= register + 1
            ? "#"
            : " ";
    }
}

public record Instruction
(
    int Cycle,
    int Register
);