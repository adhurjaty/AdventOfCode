namespace AdventOfCode.Day7;

public record TreeDirectory
{
    public string Name { get; init; }
    public List<TreeDirectory> Directories { get; set; } = new List<TreeDirectory>();
    public List<TreeFile> Files { get; set; } = new List<TreeFile>();
    public TreeDirectory? Parent { get; set; }

    private int _cachedSize = 0;
    private int _cachedItemsCount = 0;
    public int Size
    {
        get
        {
            if(_cachedItemsCount == Directories.Count + Files.Count)
                return _cachedSize;
            _cachedSize = GetSize();
            _cachedItemsCount = Directories.Count + Files.Count;
            return _cachedSize;
        }
    }

    private int GetSize()
    {
        return Files.Sum(x => x.Size) + Directories.Sum(x => x.Size);
    }
}