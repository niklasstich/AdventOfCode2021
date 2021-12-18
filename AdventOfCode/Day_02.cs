namespace AdventOfCode;

public class Day_02 : BaseDay
{
    private readonly string _input;
    private readonly string[] _lines;

    public Day_02()
    {
        _input = File.ReadAllText(InputFilePath);
        _lines = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
    }
    public override ValueTask<string> Solve_1() => new(PuzzleOne());


    public override ValueTask<string> Solve_2() => new(PuzzleTwo());

    private string PuzzleOne()
    {
        var depth = 0;
        var horizontalPos = 0;

        foreach (var line in _lines)
        {
            var X = Convert.ToInt32(line.Split(null)[1]);
            if (line.StartsWith("forward "))
            { 
                horizontalPos += X;
            } else if (line.StartsWith("down "))
            {
                depth += X;
            } else if (line.StartsWith("up "))
            {
                depth -= X;
            }
        }

        return (depth * horizontalPos).ToString();
    }
    
    private string PuzzleTwo()
    {
        var depth = 0;
        var horizontalPos = 0;
        var aim = 0;

        foreach (var line in _lines)
        {
            var X = Convert.ToInt32(line.Split(null)[1]);
            if (line.StartsWith("forward "))
            { 
                horizontalPos += X;
                depth += aim * X;
            } else if (line.StartsWith("down "))
            {
                aim += X;
            } else if (line.StartsWith("up "))
            {
                aim -= X;
            }
        }

        return (depth * horizontalPos).ToString();
    }
}