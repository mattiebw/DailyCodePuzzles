using Core;

namespace AdventOfCode2024;

[Solution(11)]
public static class Day11
{
    public static void AddOrIncrementBy<TK, TV>(this Dictionary<TK, TV> dict, TK key, TV incrementBy) where TK : notnull where TV : notnull
    {
        if (!dict.TryAdd(key, incrementBy))
            dict[key] = (TV) Convert.ChangeType((dynamic) dict[key] + (dynamic) incrementBy, typeof(TV));
    }

    public static void Solution(string input, int iterations)
    {
        var startingNumbers = input.Split(' ').Select(int.Parse).ToArray();
        
        Dictionary<long, long> numbers = new();
        foreach (var num in startingNumbers)
            numbers.Add(num, 1);

        for (int i = 0; i < iterations; i++)
        {
            var prevNums = new Dictionary<long, long>(numbers);
            numbers.Clear();
            
            foreach (var (num, amount) in prevNums)
            {
                if (num == 0)
                {
                    numbers.AddOrIncrementBy(1, amount);
                    continue;
                }

                string numString = num.ToString();
                
                if (numString.Length % 2 == 0)
                {
                    numbers.AddOrIncrementBy(long.Parse(numString[..(numString.Length / 2)]), amount);
                    numbers.AddOrIncrementBy(long.Parse(numString[(numString.Length / 2)..]), amount);
                }
                else
                {
                    numbers.AddOrIncrementBy(num * 2024, amount);
                }
            }
            
            // Console.WriteLine($@"After {i + 1} blink(s):");
            // foreach (var (num, amount) in numbers)
            //     Console.Write($@"{num} (x{amount}), ");
            // Console.WriteLine();
            // Console.WriteLine();
        }

        long total = 0;
        foreach (var (_, amount) in numbers)
            total += amount;
        Console.WriteLine($@"The total is: {total}");
    }

    [Task(1, 1, false)]
    public static void Task1(string input)
    {
        Solution(input, 25);
    }
    
    [Task(2, 1, false)]
    public static void Task2(string input)
    {
        Solution(input, 75);
    }
}
