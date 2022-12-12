using System.Text.RegularExpressions;

namespace AdventOfCode.Day10;

public class Solver
{
    private readonly IEnumerable<string> _lines = File.ReadLines(@"./Day10/input.txt");
    private readonly int[] _significantCycles = new[] { 20, 60, 100, 140, 180, 220 };

    public int Part1()
    {
        return _lines
            .Select(ToInstruction)
            .SelectWithPrevResult((state, instruction) => state with
            {
                Cycle = state.Cycle + instruction.Cycle,
                Register = state.Register + instruction.Register
            }, new Instruction(0, 1))
            .SlidingBuffer(2)
            .Select(steps =>_significantCycles
                .FirstOption(cycle => steps[0].Cycle < cycle && steps[1].Cycle >= cycle)
                .Match(sig => sig * steps[0].Register, () => 0))
            .Sum();
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

    private bool IsSignificant(Instruction[] steps, IEnumerable<int> significantCycles)
    {
        return significantCycles
            .Where(cycle => steps[0].Cycle < cycle && steps[1].Cycle >= cycle)
            .Any();
    }
}

public record Instruction
(
    int Cycle,
    int Register
);