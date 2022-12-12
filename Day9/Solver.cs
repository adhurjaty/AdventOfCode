namespace AdventOfCode.Day9;

public class Solver
{
    private readonly Dictionary<string, Func<Vec2, Vec2>> _instructionMap = new()
    {
        { "R", Move.Right },
        { "L", Move.Left },
        { "U", Move.Up },
        { "D", Move.Down },
    };

    private readonly IEnumerable<Instruction> _instructions;

    public Solver()
    {
        var lines = File.ReadLines(@"./Day9/input.txt");
        _instructions = lines
            .Select(line => 
            {
                var tokens = line.Split(' ');
                return new Instruction(_instructionMap[tokens[0]], int.Parse(tokens[1]));
            });
    }

    public int Part1()
    {
        var state = new RopeState();
        foreach (var instruction in _instructions)
        {
            state.MoveHead(instruction);
        }

        return state.TailVisited();
    }

    public int Part2()
    {
        var state = new RopeState(9);
        foreach (var instruction in _instructions)
        {
            state.MoveHead(instruction);
        }

        return state.TailVisited();
    }
}