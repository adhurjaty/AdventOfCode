namespace AdventOfCode.Day9;

public record Instruction
(
    Func<Vec2, Vec2> MoveFn,
    int Distance
);