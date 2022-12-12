using System.Text.RegularExpressions;

namespace AdventOfCode.Day10;

public class Solver
{
    private readonly IEnumerable<string> _lines = File.ReadLines(@"./Day10/input.txt");

    public int Part1()
    {
        var debug = _lines
            .Select(ToInstruction)
            .SelectWithPrevResult((state, instruction) => state with
            {
                Cycle = state.Cycle + instruction.Cycle,
                Register = state.Register + instruction.Register
            }, new Instruction(0, 1))
            .ToList();

        return 0;
        // var e = _lines.GetEnumerator();

        // var communicator = new Communicator();
        // return new[] { 20, 60, 100, 140, 180, 220 }
        //     .Select(cycle => communicator.RunToCycle(cycle, e) * cycle)
        //     .Sum();
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
}

public record Instruction
(
    int Cycle,
    int Register
);