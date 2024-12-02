using Core;

namespace AdventOfCode2024;

[Solution(2)]
public class Day2
{
    [Task(1)]
    public static void Task1(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        int safeReports = 0;
        foreach (string line in lines)
        {
            var nums = line.Split(' ').Select(int.Parse).ToArray();
            bool safe = true;
            bool mode = nums[0] < nums[1];
            for (int i = 1; i < nums.Length; i++)
            {
                int diff = Math.Abs(nums[i] - nums[i - 1]);
                if (mode != nums[i - 1] < nums[i]
                    || diff < 1 || diff > 3)
                {
                    safe = false;
                    break;
                }
            }

            if (safe)
                safeReports++;
        }

        Console.WriteLine($@"Safe reports: {safeReports}");
    }

    [Task(2, 1)]
    public static void Task2(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        int safeReports = 0;
        foreach (string line in lines)
        {
            (bool safe, int firstUnsafeIndex, int numUnsafe) CheckSafety(List<int> numbers)
            {
                var mode = numbers[0] < numbers[1];
                int unsafeCount = 0, firstUnsafeIndex = -1;
                for (var i = 0; i < numbers.Count - 1; i++)
                {
                    var diff = Math.Abs(numbers[i + 1] - numbers[i]);
                    if (mode == numbers[i] < numbers[i + 1]
                        && diff is >= 1 and <= 3) continue;
                    unsafeCount++;
                    if (firstUnsafeIndex == -1)
                        firstUnsafeIndex = i;
                }

                return (unsafeCount == 0, firstUnsafeIndex, unsafeCount);
            }

            var nums = line.Split(' ').Select(int.Parse).ToList();
            var safety = CheckSafety(nums);
            if (safety.safe)
                safeReports++;
            else
            {
                // Loop over the array from 0 to the first unsafe index + 1
                // For each i, remove the element at i and check if the array is safe
                // We loop to the first unsafe index + 1, as if we are still unsafe after the first unsafe index, none 
                // of the proceeding elements will make the array safe if removed.
                for (var i = 0; i <= safety.firstUnsafeIndex + 1; i++)
                {
                    var copy = new List<int>(nums);
                    copy.RemoveAt(i);
                    if (!CheckSafety(copy).safe) continue;
                    safeReports++;
                    break;
                }
            }
        }

        Console.WriteLine($@"Safe reports: {safeReports}");
    }
}
