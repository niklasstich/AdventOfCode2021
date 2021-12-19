namespace AdventOfCode;

public class Day07 : BaseDay
{
    private readonly string _input;

    public Day07()
    {
        _input = File.ReadAllText(InputFilePath);
    }
    
    public override ValueTask<string> Solve_1() => new(PuzzleOne());

    public override ValueTask<string> Solve_2() => new(PuzzleTwo());

    private string PuzzleOne()
    {
        return GetFuelCosts(_input, false).Min().ToString();
    }

    private string PuzzleTwo()
    {
        return GetFuelCosts(_input, true).Min().ToString();
    }

    private Dictionary<int, int> ParseInput(string input)
    {
        return input.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => Convert.ToInt32(s))
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());
    }

    public IEnumerable<int> GetFuelCosts(string input, bool cumulative)
    {
        //O(m^2) with m = unique values in input
        var positions = ParseInput(input);
        var start = positions.Min(k => k.Key);
        var count = positions.Max(k => k.Key) - start;
        return Enumerable.Range(start, count).Select(outerKey => positions.Sum(innerPair =>
        {
            var n = Math.Abs(outerKey - innerPair.Key);
            return cumulative ? n * (n + 1) / 2 * innerPair.Value : n * innerPair.Value;
        }));
    }
}