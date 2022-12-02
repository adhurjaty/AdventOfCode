var inputFile = @"./input.txt";

var output = File.ReadAllText(inputFile)
    .Split("\n\n")
    .Select(group => group
        .Split("\n")
        .Select(line => int.Parse(line))
        .Sum())
    .Max();

Console.WriteLine(output);
