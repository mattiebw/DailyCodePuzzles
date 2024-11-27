using Core;

namespace EverybodyCodesSolutions2024;

[Solution(4)]
public class Day4
{
    public static void Task1And2(string input)
    {
        var lengths = input.Split('\n').Select(s => Convert.ToInt32(s.Trim())).ToArray();

        int smallest = lengths[0];
        foreach (var length in lengths)
        {
            if (length < smallest)
                smallest = length;
        }

        int totalDiff = 0;
        foreach (var length in lengths) 
            totalDiff += length - smallest;
        
        Console.WriteLine($@"Total hits needed: {totalDiff}");
    }
    
    [Task(1)]
    public static void Task1(string input)
    {
        Task1And2(input);
    }
    
    [Task(2)]
    public static void Task2(string input)
    {
        Task1And2(input);
    }

    [Task(3)]
    public static void Task3(string input)
    {
        var lengths = input.Split('\n').Select(s => Convert.ToInt32(s.Trim())).ToList();

        // long sum = 0;
        // foreach (var length in lengths) 
        //     sum += length;
        // long avg = sum / lengths.Length;

        lengths.Sort();
        int med = lengths[lengths.Count / 2];

        long totalDiff = 0;
        foreach (var length in lengths) 
            totalDiff += Math.Abs(med - length);
        
        Console.WriteLine($@"Total hits needed: {totalDiff}");
    }
}
