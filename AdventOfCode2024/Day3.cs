using System.Text.RegularExpressions;
using Core;

namespace AdventOfCode2024;

[Solution(3)]
public class Day3
{
    [Task(1)]
    public static void Task1(string input)
    {
        var total = 0;
        var matches = Regex.Match(input, "mul\\(\\d+,\\d+\\)");
        while (matches.Success)
        {
            var parms = matches.Value.Split('(')[1];
            parms = parms.TrimEnd(')');
            var nums = parms.Split(',');
            var result = int.Parse(nums[0]) * int.Parse(nums[1]);
            total += result;
            matches = matches.NextMatch();
        }
        Console.WriteLine($@"Total: {total}");
    }
    
    [Task(2, 1, false)]
    public static void Task2(string input)
    {
        var total = 0;
        Regex regex = new("(do\\(\\))|(don't\\(\\))|(mul\\(\\d+,\\d+\\))");
        var matches = regex.Matches(input);
        bool enabled = true;
        foreach (Match match in matches)
        {
            if (match.Value == "do()")
            {
                enabled = true;
                continue;
            }
            
            if (match.Value == "don't()")
            {
                enabled = false;
                continue;
            }

            if (!enabled)
                continue;
            
            var parms = match.Value.Split('(')[1];
            parms = parms.TrimEnd(')');
            var nums = parms.Split(',');
            var result = int.Parse(nums[0]) * int.Parse(nums[1]);
            total += result;
        }
        Console.WriteLine($@"Total: {total}");
    }
}
