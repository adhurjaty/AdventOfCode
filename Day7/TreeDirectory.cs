namespace AdventOfCode.Day7;

public record TreeDirectory
{
    public string Name { get; init; }
    public List<TreeDirectory> Directories { get; set; } = new List<TreeDirectory>();
    public List<TreeFile> Files { get; set; } = new List<TreeFile>();
    public TreeDirectory? Parent { get; set; }
}