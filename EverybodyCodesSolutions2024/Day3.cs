using Core;

namespace EverybodyCodesSolutions2024;

[Solution(3)]
public class Day3
{
    public static void Task1And2(string input)
    {
        int fieldWidth = input.IndexOf('\n') - 1;
        int fieldHeight = input.Length / (fieldWidth + 1);
        input = input.Replace("\n", "");
        input = input.Replace("\r", "");
        int[,] field = new int[fieldWidth, fieldHeight];
        
        for (int i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case '.':
                    field[i % fieldWidth, i / fieldWidth] = -1;
                    break;
                case '#':
                    field[i % fieldWidth, i / fieldWidth] = 0;
                    break;
                default:
                    field[i % fieldWidth, i / fieldWidth] = Convert.ToInt32(input[i]);
                    break;
            }
        }

        void PrintField()
        {
            for (int y = 0; y < fieldHeight; y++)
            {
                for (int x = 0; x < fieldWidth; x++)
                {
                    if (field[x, y] == -1)
                        Console.Write('.');
                    else Console.Write(field[x, y]);
                }
                Console.WriteLine();
            }
        }

        int edits = 0;
        int totalEdits = 0;
        do
        {
            edits = 0;
            
            for (int y = 0; y < fieldHeight; y++)
            {
                for (int x = 0; x < fieldWidth; x++)
                {
                    if (field[x, y] == -1)
                        continue;
                    
                    int LowestNearby(int x, int y)
                    {
                        int lowest = Int32.MaxValue;
                        if (field[x + 1, y] < lowest)
                            lowest = field[x + 1, y];
                        if (field[x - 1, y] < lowest)
                            lowest = field[x - 1, y];
                        if (field[x, y + 1] < lowest)
                            lowest = field[x, y + 1];
                        if (field[x, y - 1] < lowest)
                            lowest = field[x, y - 1];
                        return Math.Max(0, lowest);
                    }

                    int lowest = LowestNearby(x, y);
                    if (field[x, y] <= lowest)
                    {
                        field[x, y]++;
                        edits++;
                    }
                }
            }

            totalEdits += edits;
        } while (edits > 0);
        
        Console.WriteLine($@"Total edits: {totalEdits}");
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
        int fieldWidth = input.IndexOf('\n') - 1;
        int fieldHeight = input.Length / (fieldWidth + 1);
        input = input.Replace("\n", "");
        input = input.Replace("\r", "");
        int[,] field = new int[fieldWidth, fieldHeight];
        
        for (int i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case '.':
                    field[i % fieldWidth, i / fieldWidth] = -1;
                    break;
                case '#':
                    field[i % fieldWidth, i / fieldWidth] = 0;
                    break;
                default:
                    field[i % fieldWidth, i / fieldWidth] = Convert.ToInt32(input[i]);
                    break;
            }
        }

        void PrintField()
        {
            for (int y = 0; y < fieldHeight; y++)
            {
                for (int x = 0; x < fieldWidth; x++)
                {
                    if (field[x, y] == -1)
                        Console.Write('.');
                    else Console.Write(field[x, y]);
                }
                Console.WriteLine();
            }
        }
        
        PrintField();

        int edits = 0;
        int totalEdits = 0;
        do
        {
            edits = 0;
            
            for (int y = 0; y < fieldHeight; y++)
            {
                for (int x = 0; x < fieldWidth; x++)
                {
                    if (field[x, y] == -1)
                        continue;
                    
                    int LowestNearby(int x, int y)
                    {
                        int lowest = Int32.MaxValue;
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            for (int dy = -1; dy <= 1; dy++)
                            {
                                if (dx == 0 && dy == 0)
                                    continue;

                                if (x + dx < 0 || x + dx >= fieldWidth || y + dy < 0 || y + dy >= fieldHeight)
                                {
                                    lowest = 0;
                                    continue;
                                }
                                
                                lowest = Math.Min(lowest, field[x + dx, y + dy]);
                            }
                        }
                        return Math.Max(0, lowest);
                    }

                    int lowest = LowestNearby(x, y);
                    if (field[x, y] <= lowest)
                    {
                        field[x, y]++;
                        edits++;
                    }
                }
            }

            totalEdits += edits;
        } while (edits > 0);
        
        Console.WriteLine($@"Total edits: {totalEdits}");   
    }
}
