namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly string _input;
    private readonly string[] _lines;

    public Day05()
    {
        _input = File.ReadAllText(InputFilePath);
        _lines = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
    }

    public override ValueTask<string> Solve_1() => new(PuzzleOne());

    public override ValueTask<string> Solve_2() => new(PuzzleTwo());

    private string PuzzleOne()
    {
        return GetHydrothermalMap(false).Count(num => num > 1).ToString();
    }

    private string PuzzleTwo()
    {
        return GetHydrothermalMap(true).Count(num => num > 1).ToString();
    }

    private IEnumerable<int> GetHydrothermalMap(bool diagonals)
    {
        var field = new int[1000*1000];
        foreach (var line in _lines)
        {
            var numbers = line.Split(new []{',', ' ', '-', '>'}, StringSplitOptions.RemoveEmptyEntries);
            int x1, y1, x2, y2;
            (x1, y1, x2, y2) = (Convert.ToInt32(numbers[0]), Convert.ToInt32(numbers[1]), Convert.ToInt32(numbers[2]),
                Convert.ToInt32(numbers[3]));
            if (!(x1 == x2 || y1 == y2))
            {
                if (!diagonals) continue;
                var len = Math.Abs(x1 - x2) + 1;
                var xseq = Enumerable.Range(Math.Min(x1, x2), len).ToArray();
                var yseq = Enumerable.Range(Math.Min(y1, y2), len).ToArray();
                if (x1 < x2 && y1 > y2 || x1 > x2 && y1 < y2)
                {
                    yseq = yseq.Reverse().ToArray();
                }

                for (var i = 0; i < xseq.Length; i++)
                {
                    var x = xseq[i];
                    var y = yseq[i];
                    field[y * 1000 + x]++;
                }
                continue;
            }
            
            //swap numbers
            if (x1 > x2 || y1 > y2)
            {
                (x1, x2) = (x2, x1);
                (y1, y2) = (y2, y1);
            }

            for (var column = x1; column <= x2; column++)
            {
                for (var row = y1; row <= y2; row++)
                {
                    field[row * 1000 + column]++;
                }
            }

        }

        return field;
    }
}
