using Core;

namespace AdventOfCode2024;

[Solution(1)]
public class Day1
{
    [Task(1)]
    public static void Task1(string input)
    {
        List<long> left = new();
        List<long> right = new();
        
        foreach (var line in input.Split('\n'))
        {
            var parts = line.Trim().Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries); // Splitting on null means split on whitespace
            left.Add(long.Parse(parts[0]));
            right.Add(long.Parse(parts[1]));
        }

        left.Sort();
        right.Sort();

        long total = 0;
        for (int i = 0; i < left.Count; i++)
        {
            total += Math.Abs(left[i] - right[i]);
        }
        
        Console.WriteLine($@"Our total is: {total}");
    }
    
    [Task(2, 1)]
    public static void Task2(string input)
    {
        List<long> left = new();
        List<long> right = new();
        
        foreach (var line in input.Split('\n'))
        {
            var parts = line.Trim().Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries); // Splitting on null means split on whitespace
            left.Add(long.Parse(parts[0]));
            right.Add(long.Parse(parts[1]));
        }

        left.Sort();
        right.Sort();

        long total = 0;
        foreach (var t in left)
            total += t * right.Count(l => l == t);

        Console.WriteLine($@"Our total is: {total}");
    }
}
