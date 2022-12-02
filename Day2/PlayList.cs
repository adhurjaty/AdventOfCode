namespace AdventOfCode.Day2;

public class PlayList
{
    private readonly Dictionary<Choice, RpsNode> _choiceIndex = new();
    private readonly RpsNode _rpsHead;

    public PlayList()
    {
        _rpsHead = ConstructLinkedList(new[] 
        {
            Choice.ROCK,
            Choice.SCISSORS,
            Choice.PAPER
        })!;
    }

    private RpsNode? ConstructLinkedList(IEnumerable<Choice> choices, RpsNode? prev = null, 
        RpsNode? first = null)
    {
        if (!choices.Any())
        {
            if (first != null)
                first.LosesTo = prev;
            return first;
        }

        var node = new RpsNode()
        {
            Choice = choices.First(),
            LosesTo = prev
        };
        _choiceIndex[choices.First()] = node;
        node.Beats = ConstructLinkedList(choices.Skip(1), node, first ?? node);
        return node;
    }

    public RpsNode GetNode(Choice choice)
    {
        return _choiceIndex[choice];
    }
}
