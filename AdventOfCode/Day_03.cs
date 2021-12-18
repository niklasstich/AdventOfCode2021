namespace AdventOfCode;

public class Day_03 : BaseDay
{
    private readonly string _input;
    private readonly string[] _lines;
    private readonly int _bitwidth = 12;

    public Day_03()
    {
        _input = File.ReadAllText(InputFilePath);
        _lines = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
    }
    public override ValueTask<string> Solve_1() => new(PuzzleOne());


    public override ValueTask<string> Solve_2() => new(PuzzleTwo());

    private string PuzzleOne()
    {
        var gamma = "";
        for (int i = 0; i < _bitwidth; i++)
        {
            gamma += IsOneMostCommon(_lines, i) ? '1' : '0';
        }

        var gammaDec = Convert.ToInt32(gamma, 2);
        var epsilonDec = 0b111111111111 ^ gammaDec;
        return (gammaDec * epsilonDec).ToString();
    }
    
    private string PuzzleTwo()
    {
        var oxygen = GetOxygenRating(_lines);
        var co2 = GetCo2ScrubberRating(_lines);

        return (oxygen * co2).ToString();
    }

    private bool IsOneMostCommon(IEnumerable<string> lines, int i)
    {
        return lines.Count(line => line[i] == '1') >= lines.Count() / 2;
    }

    private int GetOxygenRating(IEnumerable<string> lines)
    {
        return GetRating(lines, IsOneMostCommon);
    }

    private int GetCo2ScrubberRating(IList<string> lines)
    {
        return GetRating(lines, (e, i) => !IsOneMostCommon(e, i));
    }

    private static int GetRating(IEnumerable<string> lines, Func<IEnumerable<string>, int, bool> bitFunc)
    {
        var i = 0;
        while (lines.Count() > 1)
        {
            var bit = bitFunc(lines, i) ? '1' : '0';
            lines = lines.Where(line => line[i] == bit).ToList();
            i++;
        }

        return Convert.ToInt32(lines.First(), 2);
    }
}
