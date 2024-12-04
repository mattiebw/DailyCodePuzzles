using System.Text;
using Core;

namespace AdventOfCode2024;

[Solution(4)]
public class Day4
{
    [Task(1, 1, false)]
    public static void Task1(string input)
    {
        var lines = input.Split('\n').Select(s => s.Trim()).ToArray();
        var grid = new char[lines[0].Length, lines.Length];
        for (var index = 0; index < lines.Length; index++)
        {
            for (var i = 0; i < lines[index].Length; i++)
            {
                grid[i, index] = lines[index][i];
            }
        }

        int count = 0;

        bool CheckForString(string s, int x, int y, int dirX, int dirY)
        {
            foreach (var t in s)
            {
                if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1) || grid[x, y] != t)
                    return false;

                if (grid[x, y] != t)
                    return false;
                
                x += dirX;
                y += dirY;
            }

            return true;
        }
        
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                if (grid[x, y] == 'X')
                {
                    // Check to see if there is an XMAS tree in the vicinity.
                    if (CheckForString("XMAS", x, y, 1, 0))
                        count++;

                    if (CheckForString("XMAS", x, y, -1, 0))
                        count++;

                    if (CheckForString("XMAS", x, y, 0, 1))
                        count++;

                    if (CheckForString("XMAS", x, y, 0, -1))
                        count++;

                    if (CheckForString("XMAS", x, y, 1, 1))
                        count++;

                    if (CheckForString("XMAS", x, y, -1, -1))
                        count++;

                    if (CheckForString("XMAS", x, y, 1, -1))
                        count++;

                    if (CheckForString("XMAS", x, y, -1, 1))
                        count++;
                }
            }
        }
        
        Console.WriteLine($@"Total XMAS: {count}");
    }
    
    [Task(2, 1, false)]
    public static void Task2(string input)
    {
        var lines = input.Split('\n').Select(s => s.Trim()).ToArray();
        var grid = new char[lines[0].Length, lines.Length];
        for (var index = 0; index < lines.Length; index++)
        {
            for (var i = 0; i < lines[index].Length; i++)
            {
                grid[i, index] = lines[index][i];
            }
        }

        int count = 0;

        bool CheckForMAS(int x, int y)
        {
            // Check x/y is not on the border
            if (x == 0 || x == grid.GetLength(0) - 1 || y == 0 || y == grid.GetLength(1) - 1)
                return false;
            
            // Check our diagonals for M and S
            if (grid[x - 1, y - 1] == 'M' || grid[x - 1, y - 1] == 'S')
            {
                char other = grid[x - 1, y - 1] == 'M' ? 'S' : 'M';
                if (!(grid[x + 1, y + 1] == other || grid[x + 1, y + 1] == other))
                    return false;
            }
            else return false;

            if (grid[x - 1, y + 1] == 'M' || grid[x - 1, y + 1] == 'S')
            {
                char other = grid[x - 1, y + 1] == 'M' ? 'S' : 'M';
                if (!(grid[x + 1, y - 1] == other || grid[x + 1, y - 1] == other))
                    return false;
            }
            else return false;

            return true;
        }
        
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                if (grid[x, y] == 'A')
                {
                    bool result = CheckForMAS(x, y);
                    if (result)
                        count++;
                }
            }
        }

        // for (int y = 0; y < lines.Length; y++)
        // {
        //     for (int x = 0; x < lines[0].Length; x++)
        //     {
        //         Console.ForegroundColor = found[x, y] ? ConsoleColor.Green : ConsoleColor.Red;
        //         Console.Write(grid[x, y]);
        //     }
        //     Console.WriteLine();
        // }

        Console.ResetColor();
        Console.WriteLine($@"Total X-MAS: {count}");
    }
}
