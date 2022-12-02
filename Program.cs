namespace AdventOfCode;

public class Program
{
    public static void Main(string[] args)
    {
        var inputText = File.ReadAllText(@"./input.txt");
        var solution = Day1.Part2(inputText);
        Console.WriteLine(solution);
    }
}
