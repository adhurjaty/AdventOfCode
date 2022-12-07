namespace AdventOfCode.Day7;

public class Filesystem
{
    public TreeDirectory Root { get; private set; }
    public TreeDirectory CurrentDirectory { get; private set; }

    public Filesystem(TreeDirectory root)
    {
        Root = root;
        CurrentDirectory = Root;
    }

    public void Cd(string name)
    {
        if (name == "..")
            CurrentDirectory = CurrentDirectory.Parent;
        else
            CurrentDirectory = CurrentDirectory.Directories.First(x => x.Name == name);
            
        if (CurrentDirectory is null) throw new Exception($"null {name} directory");
    }

    public void MkDir(string name)
    {
        CurrentDirectory.Directories.Add(new TreeDirectory()
        {
            Name = name,
            Parent = CurrentDirectory
        });
    }

    public void Touch(string name, int size)
    {
        CurrentDirectory.Files.Add(new TreeFile
        (
            Name: name,
            Size: size,
            Parent: CurrentDirectory
        ));
    }
}