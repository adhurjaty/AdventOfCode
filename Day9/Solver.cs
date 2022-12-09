namespace AdventOfCode.Day9;

public class Solver
{
    private readonly IEnumerable<Instruction> _instructions;
    private readonly RopeState _state = new RopeState();

    public Solver()
    {
        // var lines = File.ReadLines(@"./Day9/input.txt");
        var lines = @"R 4
                    U 4
                    L 3
                    D 1
                    R 4
                    D 1
                    L 5
                    R 2".Split("\n").Select(x => x.Trim());
        _instructions = lines
            .Select(line => 
            {
                var tokens = line.Split(' ');
                return new Instruction(tokens[0], int.Parse(tokens[1]));
            });
    }

    public int Part1()
    {

        foreach (var instruction in _instructions)
        {
            _state.MoveHead(instruction);
        }

        return _state.TailVisited();
    }

}