using System.Collections.Generic;
using System.Threading.Tasks;
using AdventOfCode;
using AoCHelper;
using NUnit.Framework;

namespace AdventOfTest;

public class TestDays
{
    public static IEnumerable<TestCaseData> AdventOfCodeDays
    {
        get
        {
            yield return new TestCaseData(new Day_01(), "1557", "1608");
            yield return new TestCaseData(new Day_02(), "1882980", "1971232560");
            yield return new TestCaseData(new Day_03(), "3277364", "5736383");
            yield return new TestCaseData(new Day_04(), "63424", "23541");
            yield return new TestCaseData(new Day_05(), "6564", "19172");
        }
    }
    [TestCaseSource(nameof(AdventOfCodeDays))]
    public async Task TestDay(BaseDay day, string solution1, string solution2)
    {
        Assert.AreEqual(solution1, await day.Solve_1());
        Assert.AreEqual(solution2, await day.Solve_2());
    }
}