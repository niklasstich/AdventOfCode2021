namespace AdventOfCode;

public class Day_04 : BaseDay
{
    private readonly string _input;
    private readonly string[] _blocks;
    private readonly string bingoNumbers;
    private readonly List<BingoBoard> bingoBoards;

    record Cell(int number, bool marked = false);
    private class BingoBoard
    {
        public Cell[] Numbers { get;  set; }
        
        public int Score { get; private set; }

        protected internal BingoBoard(string el)
        {
            Numbers = el.Split(new []{ ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(num => new Cell(Convert.ToInt32(num))).ToArray();
        }

        private IEnumerable<int> GetNumInRow(int row) =>
            Enumerable.Range(0, 5).Select(column => Numbers[row * 5 + column].number);

        private IEnumerable<int> GetNumInColumn(int column) =>
            Enumerable.Range(0, 5).Select(row => Numbers[row * 5 + column].number);
        
        private bool IsRowCompleted(int row) =>
            Enumerable.Range(0, 5).Select(column => Numbers[row * 5 + column].marked).All(el => el);

        private bool IsColumnCompleted(int column) =>
            Enumerable.Range(0, 5).Select(row => Numbers[row * 5 + column].marked).All(el => el);

        public int MarkNumber(int num)
        {
            var index = Array.FindIndex(Numbers, cell => cell.number == num);
            
            if (index == -1) return -1;
            
            Numbers[index] = Numbers[index] with { marked = true };
            var row = index / 5;
            var column = index % 5;
            
            if (!IsRowCompleted(row) && !IsColumnCompleted(column)) return -1;
            
            var unmarkedNumbers = from cell in Numbers where !cell.marked select cell.number;

            return num * unmarkedNumbers.Sum();

        }
    }

    public Day_04()
    {
        _input = File.ReadAllText(InputFilePath);
        _blocks = _input.Split("\n\n");
        bingoNumbers = _blocks[0];
        bingoBoards = _blocks.Skip(1).Select(el => new BingoBoard(el)).ToList();
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
        foreach (var num in bingoNumbers.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            foreach (var board in bingoBoards.ToList())
            {
                var score = board.MarkNumber(Convert.ToInt32(num));
                if (score == -1) continue;
                bingoBoards.Remove(board);
                yield return score;
            }
        }
    }
}
