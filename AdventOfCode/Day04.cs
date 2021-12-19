namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string _bingoNumbers;
    private readonly List<BingoBoard> _bingoBoards;

    record Cell(int Number, bool Marked = false);
    private class BingoBoard
    {
        public Cell[] Numbers { get; }

        protected internal BingoBoard(string el)
        {
            Numbers = el.Split(new []{ ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(num => new Cell(Convert.ToInt32(num))).ToArray();
        }

        private bool IsRowCompleted(int row) =>
            Enumerable.Range(0, 5).Select(column => Numbers[row * 5 + column].Marked).All(el => el);

        private bool IsColumnCompleted(int column) =>
            Enumerable.Range(0, 5).Select(row => Numbers[row * 5 + column].Marked).All(el => el);

        public int MarkNumber(int num)
        {
            var index = Array.FindIndex(Numbers, cell => cell.Number == num);
            
            if (index == -1) return -1;
            
            Numbers[index] = Numbers[index] with { Marked = true };
            var row = index / 5;
            var column = index % 5;
            
            if (!IsRowCompleted(row) && !IsColumnCompleted(column)) return -1;
            
            var unmarkedNumbers = from cell in Numbers where !cell.Marked select cell.Number;

            return num * unmarkedNumbers.Sum();

        }
    }

    public Day04()
    {
        var input = File.ReadAllText(InputFilePath);
        var blocks = input.Split("\n\n");
        _bingoNumbers = blocks[0];
        _bingoBoards = blocks.Skip(1).Select(el => new BingoBoard(el)).ToList();
    }

    public override ValueTask<string> Solve_1() => new(PuzzleOne());

    public override ValueTask<string> Solve_2() => new(PuzzleTwo());

    private string PuzzleOne()
    {
        return SolveBoards().First().ToString();
    }

    private string PuzzleTwo()
    {
        return SolveBoards().Last().ToString();
    }

    private IEnumerable<int> SolveBoards()
    {
        foreach (var num in _bingoNumbers.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            foreach (var board in _bingoBoards.ToList())
            {
                var score = board.MarkNumber(Convert.ToInt32(num));
                if (score == -1) continue;
                _bingoBoards.Remove(board);
                yield return score;
            }
        }
    }
}
