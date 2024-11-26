using System.Diagnostics;
using System.Text;
using Core;

namespace EverybodyCodesSolutions2024;

[Solution(2)]
public class Day2
{
    [Task(1)]
    public static void Task1(string input)
    {
        var lines = input.Split('\n');
        var runicWords = lines[0].Split(':')[1].Trim().Split(',');
        input = lines[2];

        int runes = 0;
        foreach (string runicWord in runicWords)
        {
            for (int i = 0; i < input.Length - runicWord.Length; i++)
            {
                if (input.Substring(i, runicWord.Length) == runicWord)
                {
                    // Console.WriteLine($"Found {runicWord} at index {i}");
                    runes++;
                }
            }
        }

        Console.WriteLine($@"Runes found: {runes}");
    }

    [Task(2)]
    public static void Task2(string input)
    {
        var lines = input.Split('\n');
        var runicWords = lines[0].Split(':')[1].Trim().Split(',');
        Console.WriteLine($@"Runic words: {string.Join(", ", runicWords)}");

        int runes = 0;
        for (int i = 2; i < lines.Length; i++)
        {
            // This sucks, but here we are
            HashSet<int> runicIndexes = new();
            string line = lines[i].Trim();

            foreach (string runicWord in runicWords)
            {
                string reversedRunicWord = new string(runicWord.Reverse().ToArray());

                // First, lets look for the runic word forward.
                // for (int j = 0; j < line.Length - runicWord.Length; j++)
                // {
                //     if (line.Substring(j, runicWord.Length) == runicWord)
                //     {
                //         // Okay, we found one. Let's add all the indicies of this runic word to the hashset.
                //         for (int k = j; k < j + runicWord.Length; k++)
                //             runicIndexes.Add(k);
                //     }
                // }

                // // Now, let's look for the runic word backwards.
                // for (int j = line.Length; j >= runicWord.Length; j--)
                // {
                //     if (new string(line.Substring(j - runicWord.Length, runicWord.Length).Reverse().ToArray()) ==
                //         runicWord)
                //     {
                //         // Okay, we found one. Let's add all the indices of this runic word to the hashset.
                //         for (int k = j - runicWord.Length; k < j; k++)
                //             runicIndexes.Add(k);
                //     }
                // }

                // void CheckLine(string theLine, bool reversed = false)
                // {
                //     for (int j = 0; j < theLine.Length - runicWord.Length; j++)
                //     {
                //         if (theLine.Substring(j, runicWord.Length) == runicWord)
                //         {
                //             // Okay, we found one. Let's add all the indices of this runic word to the hashset.
                //             for (int k = j; k < j + runicWord.Length; k++)
                //                 runicIndexes.Add(reversed ? line.Length - k - 1 : k);
                //         }
                //     }
                // }
                //
                // CheckLine(line);
                // CheckLine(new string(line.Reverse().ToArray()), true);

                bool CheckStringEquals(string str, int index, string to)
                {
                    for (int i = 0; i < to.Length; i++)
                    {
                        if (str[(index + i) % str.Length] != to[i])
                            return false;
                    }

                    return true;
                }

                for (int j = 0; j < (line.Length + 1) - runicWord.Length; j++)
                {
                    if (CheckStringEquals(line, j, runicWord))
                    {
                        for (int k = j; k < j + runicWord.Length; k++)
                            runicIndexes.Add(k % line.Length);
                    }

                    if (CheckStringEquals(line, j, reversedRunicWord))
                    {
                        for (int k = j; k < j + reversedRunicWord.Length; k++)
                            runicIndexes.Add(k % line.Length);
                    }
                }
            }

            for (int ci = 0; ci < line.Length; ci++)
            {
                if (runicIndexes.Contains(ci))
                    Console.BackgroundColor = ConsoleColor.Red;
                Console.Write(line[ci]);
                Console.ResetColor();
            }

            Console.WriteLine();

            runes += runicIndexes.Count;
        }

        Console.WriteLine($@"Runes found: {runes}");
    }

    [Task(3)]
    public static void Task3(string input)
    {
        Stopwatch sw = new();
        sw.Start();
        var lines = input.Split('\n');
        var runicWords = lines[0].Split(':')[1].Trim().Split(',');
        Console.WriteLine($@"Runic words: {string.Join(", ", runicWords)}");
        lines = lines.Skip(2).ToArray();

        int runes = 0;
        HashSet<(int, int)> runicIndexes = new();

        void PrintGrid()
        {
            for (int li = 0; li < lines.Length; li++)
            {
                for (int ci = 0; ci < lines[li].Length; ci++)
                {
                    if (runicIndexes.Contains((ci, li)))
                        Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write(lines[li][ci]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
        
        void ProcessLines(string[] lines, bool vertical)
        {
            if (vertical)
            {
                string[] newLines = new string[lines[0].Trim().Length];
                for (int i = 0; i < lines[0].Trim().Length; i++)
                {
                    StringBuilder builder = new();
                    for (int j = 0; j < lines.Length; j++)
                    {
                        builder.Append(lines[j][i]);
                    }

                    newLines[i] = builder.ToString();
                }

                lines = newLines;
            }

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                foreach (string runicWord in runicWords)
                {
                    string reversedRunicWord = new string(runicWord.Reverse().ToArray());

                    bool CheckStringEquals(string str, int index, string to, bool wrap)
                    {
                        for (int i = 0; i < to.Length; i++)
                        {
                            if (!wrap && (index + i) >= str.Length)
                                return false;
                            
                            if (str[(index + i) % str.Length] != to[i])
                                return false;
                        }

                        return true;
                    }

                    for (int j = 0; j < line.Length; j++)
                    {
                        if (CheckStringEquals(line, j, runicWord, !vertical))
                        {
                            for (int k = j; k < j + runicWord.Length; k++)
                            {
                                runicIndexes.Add(vertical ? (i, k % line.Length) : (k % line.Length, i));
                            }
                            // Console.WriteLine($"Found {runicWord}");
                            // PrintGrid();
                        }

                        if (CheckStringEquals(line, j, reversedRunicWord, !vertical))
                        {
                            for (int k = j; k < j + reversedRunicWord.Length; k++)
                            {
                                runicIndexes.Add(vertical ? (i, k % line.Length) : (k % line.Length, i));
                            }
                            // Console.WriteLine($"Found {runicWord}");
                            // PrintGrid();
                        }
                    }
                }
            }
        }

        ProcessLines(lines, false);
        ProcessLines(lines, true);
        
        runes += runicIndexes.Count;
        sw.Stop();

        Console.WriteLine($@"Runes found: {runes}");
        Console.WriteLine($@"Took {sw.ElapsedMilliseconds}ms");
    }
}
