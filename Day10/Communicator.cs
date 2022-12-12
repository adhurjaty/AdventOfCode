namespace AdventOfCode.Day10;

public class Communicator
{
    private readonly IParser[] _parsers = new IParser[]
    {
        new NoopParser(),
        new AddXParser()
    };

    private int _cycle = 0;
    private int _register = 1;

    public int RunToCycle(int cycle, IEnumerator<string> e)
    {
        if (e.Current is null && !e.MoveNext()) return _register;

        do
        {
            var result = _parsers
                .Select(x => x.Execute(e.Current!, _register))
                .First(x => x.IsSome)
                .Match(res => res, () => throw new Exception("No match"));

            
            _cycle += result.Count;
            if (_cycle >= cycle)
            {
                var prevRegister = _register;
                _register = result.Register;
                e.MoveNext();
                return prevRegister;
            }
            _register = result.Register;

        } while (e.MoveNext());

        return _register;
    }
}