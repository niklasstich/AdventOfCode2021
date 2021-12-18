namespace AdventOfCode;

public static class Program
{
    private static void Main(string[] args)
    {
        try
        {
            switch (args.Length)
            {
                case 0:
                    Solver.SolveLast(new SolverConfiguration { ClearConsole = false });
                    break;
                case 1 when args[0].Contains("all", StringComparison.CurrentCultureIgnoreCase):
                    Solver.SolveAll(new SolverConfiguration
                        { ShowConstructorElapsedTime = true, ShowTotalElapsedTimePerDay = true });
                    break;
                default:
                {
                    var indexes = args.Select(arg => uint.TryParse(arg, out var index) ? index : uint.MaxValue);

                    Solver.Solve(indexes.Where(i => i < uint.MaxValue));
                    break;
                }
            }

        }
        catch (Exception ex)
        {
            Environment.FailFast("Solver execution failed", ex);
        }
    }
}
