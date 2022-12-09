namespace AdventOfCode.Day9;

public class Instruction
{
    private readonly Dictionary<string, Func<Vec2, int, Vec2>> _instructionMap = new()
    {
        { "R", Move.Right },
        { "L", Move.Left },
        { "U", Move.Up },
        { "D", Move.Down },
    };

    public Func<Vec2, int, Vec2> Execute { get; init; }
    public int Distance { get; init; }
    
    public Instruction(string direction, int distance)
    {
        Distance = distance;
        Execute = _instructionMap[direction];
    }
}