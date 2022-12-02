namespace AdventOfCode.Day2;

public abstract class Play : IComparable<Play>
{
    public abstract int Points { get; }

    public abstract int CompareTo(Play? other);

    public abstract Play GetComparePlay(int comparison);
}

public class Rock : Play
{
    public override int Points => 1;

    public override int CompareTo(Play? other)
    {
        if (other is Paper)
            return -1;
        if (other is Scissors)
            return 1;
        return 0;
    }

    public override Play GetComparePlay(int comparison)
    {
        if (comparison == 1)
            return new Paper();
        if (comparison == -1)
            return new Scissors();
        return new Rock();
    }
}

public class Paper : Play
{
    public override int Points => 2;

    public override int CompareTo(Play? other)
    {
        if (other is Scissors)
            return -1;
        if (other is Rock)
            return 1;
        return 0;
    }

    public override Play GetComparePlay(int comparison)
    {
        if (comparison == 1)
            return new Scissors();
        if (comparison == -1)
            return new Rock();
        return new Paper();
    }
}

public class Scissors : Play
{
    public override int Points => 3;

    public override int CompareTo(Play? other)
    {
        if (other is Rock)
            return -1;
        if (other is Paper)
            return 1;
        return 0;
    }

    public override Play GetComparePlay(int comparison)
    {
        if (comparison == 1)
            return new Rock();
        if (comparison == -1)
            return new Paper();
        return new Scissors();
    }
}