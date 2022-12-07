namespace AdventOfCode.Day7;

public record TreeFile
(
    string Name,
    int Size,
    TreeDirectory Parent
);