using Core;

namespace AdventOfCode2024;

[Solution(5)]
public class Day5
{
    [Task(1, 1, false)]
    public static void Task1(string input)
    {
        Dictionary<int, List<int>> mustBeBefore = new();

        var lines = input.Split('\n').Select(s => s.Trim()).ToArray();
        int i = 0;
        while (lines[i] != "")
        {
            var nums = lines[i].Split('|').Select(int.Parse).ToArray();
            if (mustBeBefore.ContainsKey(nums[0]))
                mustBeBefore[nums[0]].Add(nums[1]);
            else
                mustBeBefore.Add(nums[0], [nums[1]]);
            i++;
        }

        i++;

        List<List<int>> lists = new();

        for (int j = i; i < lines.Length; i++)
        {
            lists.Add(lines[i].Split(',').Select(int.Parse).ToList());
        }

        List<List<int>> valid = new();

        foreach (var list in lists)
        {
            bool isValid = true;
            for (int numIndex = 0; numIndex < list.Count; numIndex++)
            {
                if (!mustBeBefore.TryGetValue(list[numIndex], out var requirements))
                    continue;

                for (int j = 0; j < numIndex; j++)
                {
                    if (requirements.Contains(list[j]))
                        isValid = false;
                }
            }
            
            if (isValid)
                valid.Add(list);
        }
        
        Console.WriteLine($@"Found {valid.Count} (out of {lists.Count}) valid lists.");

        int medianSum = 0;
        foreach (var list in valid)
            medianSum += list[list.Count / 2];
        Console.WriteLine($@"Median: {medianSum}");
    }
    
    [Task(2, 1, false)]
    public static void Task2(string input)
    {
        Dictionary<int, List<int>> mustBeBefore = new();

        var lines = input.Split('\n').Select(s => s.Trim()).ToArray();
        int i = 0;
        while (lines[i] != "")
        {
            var nums = lines[i].Split('|').Select(int.Parse).ToArray();
            if (mustBeBefore.ContainsKey(nums[0]))
                mustBeBefore[nums[0]].Add(nums[1]);
            else
                mustBeBefore.Add(nums[0], [nums[1]]);
            i++;
        }

        i++;

        List<List<int>> lists = new();

        for (int j = i; i < lines.Length; i++)
        {
            lists.Add(lines[i].Split(',').Select(int.Parse).ToList());
        }

        List<List<int>> invalid = new();

        bool IsValidIndex(List<int> list, int index)
        {
            if (!mustBeBefore.TryGetValue(list[index], out var requirements))
                return true;

            for (int j = 0; j < index; j++)
            {
                if (requirements.Contains(list[j]))
                    return false;
            }

            return true;
        }
        
        foreach (var list in lists)
        {
            bool isValid = true;
            for (int numIndex = 0; numIndex < list.Count; numIndex++)
            {
                if (!IsValidIndex(list, numIndex))
                    isValid = false;
            }
            
            if (!isValid)
                invalid.Add(list);
        }
        
        Console.WriteLine($@"Found {invalid.Count} (out of {lists.Count}) invalid lists.");
        
        // Solve to make all invalid lists valid.
        foreach (var list in invalid)
        {
            int invalids = 1;
            while (invalids > 0)
            {
                invalids = 0;
                for (int index = 0; index < list.Count; index++)
                {
                    if (!IsValidIndex(list, index))
                    {
                        int num = list[index];
                        list.RemoveAt(index);
                        list.Insert(index - 1, num);
                        invalids++;
                    }
                }
            }
        }
        
        // And find our median again.
        int medianSum = 0;
        foreach (var list in invalid)
            medianSum += list[list.Count / 2];
        Console.WriteLine($@"Median: {medianSum}");
    }
}