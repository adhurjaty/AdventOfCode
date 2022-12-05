using System.Text.RegularExpressions;

namespace AdventOfCode.Day5;

public class Solver
{
    private const int STACK_WIDTH = 4;

    private readonly Stack<string>[] _stacks;
    private readonly IEnumerable<Instruction> _instructions;

    public Solver()
    {
        var lines = File.ReadLines(@"./Day5/input.txt");
        var numStacks = lines
            .Select(line => Regex.Matches(line, @"\d+"))
            .Where(matches => matches.Any())
            .Select(matches => int.Parse(matches.Last().Value))
            .First();

        _stacks = ConstructStacks(lines, numStacks);
        _instructions = lines
            .Select(line => Regex.Match(line, @"move (\d+) from (\d+) to (\d+)"))
            .Where(match => match.Success)
            .Select(match => new Instruction
            (
                Number: int.Parse(match.Groups[1].Value),
                SrcStack: int.Parse(match.Groups[2].Value),
                DestStack: int.Parse(match.Groups[3].Value)
            ));
    }

    private Stack<string>[] ConstructStacks(IEnumerable<string> lines, int numStacks)
    {
        var stacks = Enumerable.Range(0, numStacks)
            .Select(_ => new Stack<string>())
            .ToArray();

        var stackInput = lines
            .Where(line => Regex.IsMatch(line, @"^\s*\["))
            .Reverse();

        foreach (var row in stackInput)
        {
            var matches = Regex.Matches(row, @"\[([A-Z])\]");
            foreach (var match in matches.ToList())
            {
                stacks[match.Index / STACK_WIDTH].Push(match.Groups[1].Value);
            }
        }

        return stacks;
    }

    public string Part1()
    {
        MoveBoxes(instruction =>
        {
            return Enumerable.Range(0, instruction.Number)
                .Select(_ => _stacks[instruction.SrcStack - 1].Pop());
        });

        return GetStackTops();
    }

    public string Part2()
    {
        MoveBoxes(instruction =>
        {
            return Enumerable.Range(0, instruction.Number)
                .Select(_ => _stacks[instruction.SrcStack - 1].Pop())
                .Chunk(3)
                .SelectMany(chunk => chunk.Reverse());
        });

        return GetStackTops();
    }

    private void MoveBoxes(Func<Instruction, IEnumerable<string>> moveFn)
    {
        foreach (var instruction in _instructions)
        {
            foreach(var block in moveFn(instruction))
            {
                _stacks[instruction.DestStack - 1].Push(block);
            }
        }
    }
    
    private string GetStackTops()
    {
        return string.Join("", _stacks
            .Select(stack => stack.Peek()));
    }
}

public record Instruction
(
    int Number,
    int SrcStack,
    int DestStack
);
