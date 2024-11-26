using Core;

namespace EverybodyCodesSolutions2024;

[Solution(1)]
public class Day1
{
    [Task(1)]
    public static void Task1(string input)
    {
        int potions = 0;
        foreach (char c in input)
        {
            switch (c)
            {
                case 'A':
                    break;
                case 'B':
                    potions++;
                    break;
                case 'C':
                    potions += 3;
                    break;
            }
        }
        Console.WriteLine($@"Potions needed: {potions}");
    }

    [Task(2)]
    public static void Task2(string input)
    {
        int potions = 0;
        
        for (int i = 0; i < input.Length - 1; i += 2)
        {
            bool AddPotions(char c)
            {
                switch (c)
                {
                    case 'x':
                        return false;
                    case 'A':
                        return true;
                    case 'B':
                        potions++;
                        return true;
                    case 'C':
                        potions += 3;
                        return true;
                    case 'D':
                        potions += 5;
                        return true;
                    default:
                        throw new Exception("Invalid enemy");
                }
            }
            
            var left = AddPotions(input[i]);
            var right = AddPotions(input[i + 1]);
            if (left && right)
                potions += 2;
        }
        
        Console.WriteLine($@"Potions needed: {potions}");
    }

    [Task(3)]
    public static void Task3(string input)
    {
        int potions = 0;

        int currentPotions = 0;
        int currentSpots = 0;
        int currentEnemyCount = 0;

        void AddCurrent()
        {
            potions += currentPotions;
            if (currentEnemyCount > 1)
                potions += currentEnemyCount * (currentEnemyCount - 1);
            currentPotions = 0;
            currentEnemyCount = 0;
            currentSpots = 0;
        }
        
        foreach (char c in input)
        {
            currentEnemyCount++;
            currentSpots++;
            switch (c)
            {
                case 'x':
                    currentEnemyCount--;
                    break;
                case 'A':
                    break;
                case 'B':
                    currentPotions += 1;
                    break;
                case 'C':
                    currentPotions += 3;
                    break;
                case 'D':
                    currentPotions += 5;
                    break;
            }
            
            if (currentSpots == 3)
                AddCurrent();
        }

        AddCurrent();
        
        Console.WriteLine($@"Potions needed: {potions}");
    }
}
