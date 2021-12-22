namespace AdventOfCode;

//heavily cheated on this one with https://github.com/encse/adventofcode/blob/master/2021/Day08/Solution.cs
public class Day08 : BaseDay
{
    private readonly string _input;
    private readonly string[] _lines;

    public Day08()
    {
        _input = File.ReadAllText(InputFilePath);
        _lines = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
    }

    public override ValueTask<string> Solve_1() => new(PuzzleOne());

    public override ValueTask<string> Solve_2() => new(PuzzleTwo());

    private string PuzzleOne()
    {
        var lookup = new[] { "cf", "bcdf", "acf", "abcdefg" }.Select(s => s.Length)
            .ToHashSet();
        return _lines.Select(l => l.Split('|')[1])
            .Select(l => l.Split(' '))
            .SelectMany(a => a)
            .Count(s => lookup.Contains(s.Length)).ToString();
    }

    private string PuzzleTwo()
    {
        var sum = 0;
        foreach (var line in _lines)
        {
            //we take left side of the separator and turn all those strings into an array of hashsets
            var patterns = line.Split('|', StringSplitOptions.RemoveEmptyEntries)[0]
                .Split(' ',StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToHashSet()).ToArray();
            //we make an array of hashsets with 10 entries
            var mapping = new HashSet<char>[10];
            var letters = new[]
            {
                "abcefg",
                "cf",
                "acdeg",
                "acdfg",
                "bcdf",
                "abdfg",
                "abdefg",
                "acf",
                "abcdefg",
                "abcdfg"
            }.Select(s => s.ToHashSet()).ToArray();
            //find the single hashset in the array of hashsets where the count is 2
            mapping[1] = patterns.Single(p => p.Count == letters[1].Count); 
            mapping[7] = patterns.Single(p => p.Count == letters[7].Count); 
            mapping[4] = patterns.Single(p => p.Count == letters[4].Count);
            mapping[8] = patterns.Single(p => p.Count == letters[8].Count);
            
            //find single hashset that intersects correctly with 1 and 4
            mapping[0] = FindIntersection(patterns, letters, mapping, 0);
            mapping[2] = FindIntersection(patterns, letters, mapping, 2);
            mapping[3] = FindIntersection(patterns, letters, mapping, 3);
            mapping[5] = FindIntersection(patterns, letters, mapping, 5);
            mapping[6] = FindIntersection(patterns, letters, mapping, 6);
            mapping[9] = FindIntersection(patterns, letters, mapping, 9);

            var decode = (string v) => 
                Array.IndexOf(mapping, mapping.Single(m => m.SetEquals(v)));
            sum += line.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(0, (i, s) => i * 10 + decode(s));
        }
        return sum.ToString();
    }

    private HashSet<char> FindIntersection(HashSet<char>[] patterns, HashSet<char>[] letters, HashSet<char>[] mapping, int digit)
    {
        return patterns.Single(p => p.Count == letters[digit].Count
                                    && p.Intersect(mapping[1]).Count() == letters[1].Intersect(letters[digit]).Count()
                                    && p.Intersect(mapping[4]).Count() == letters[4].Intersect(letters[digit]).Count());
    }
}
