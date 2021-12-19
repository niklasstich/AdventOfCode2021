using System.Numerics;

namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly string _input;

    public Day06()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    private Dictionary<int, BigInteger> ParseInput()
    {
        var d = Enumerable.Range(0, 9).ToDictionary(x => x, _ => BigInteger.Zero);
        foreach (var num in _input.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            d[Convert.ToInt32(num)]++;
        }

        return d;
    }

    public override ValueTask<string> Solve_1() => new(PuzzleOne());

    public override ValueTask<string> Solve_2() => new(PuzzleTwo());

    private string PuzzleOne()
    {
        return GetFishCountAfterRounds(80);
    }

    private string PuzzleTwo()
    {
        return GetFishCountAfterRounds(256);
    }

    private string GetFishCountAfterRounds(int rounds)
    {
        var fishSim = ParseInput();
        for (var round = 0; round < rounds; round++)
        {
            var clone = new Dictionary<int, BigInteger>(fishSim)
            {
                [0] = fishSim[1],
                [1] = fishSim[2],
                [2] = fishSim[3],
                [3] = fishSim[4],
                [4] = fishSim[5],
                [5] = fishSim[6],
                [6] = fishSim[7]+fishSim[0],
                [7] = fishSim[8],
                [8] = fishSim[0],
            };
            fishSim = clone;
        }

        return fishSim.Aggregate(BigInteger.Zero, (current, p) => current + p.Value)
            .ToString();
    }
}