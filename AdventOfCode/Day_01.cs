namespace AdventOfCode;

public class Day_01 : BaseDay
{
    private readonly string _input;
    private readonly string[] _lines;

    public Day_01()
    {
        _input = File.ReadAllText(InputFilePath);
        _lines = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
    }

    public override ValueTask<string> Solve_1() => new(PuzzleOne());

    public override ValueTask<string> Solve_2() => new(PuzzleTwo());

    private string PuzzleOne()
    {
        var counter = 0;
        var lastDepth = 0;

        foreach (var line in _lines)
        {
            var currentDepth = Convert.ToInt32(line);
            if (currentDepth > lastDepth && lastDepth != 0) counter++;
            lastDepth = currentDepth;
        }

        return counter.ToString();
    }

    private string PuzzleTwo()
    {
        var counter = 0;
        var lastDepth = 0;

        for (int i = 0; i < _lines.Length-2; i++)
        {
            var sum = Convert.ToInt32(_lines[i]);
            sum += Convert.ToInt32(_lines[i + 1]);
            sum += Convert.ToInt32(_lines[i + 2]);

            if (sum > lastDepth && lastDepth != 0)
            {
                counter++;
            }

            lastDepth = sum;
        }

        return counter.ToString();
    }
}
