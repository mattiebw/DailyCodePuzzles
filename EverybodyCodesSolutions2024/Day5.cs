using System.Text;
using Core;

namespace EverybodyCodesSolutions2024;

[Solution(5)]
public class Day5
{
    [Task(1)]
    public static void Task1(string input)
    {
        const int columns = 4;
        List<int>[] seats = new List<int>[columns];
        for (int i = 0; i < columns; i++)
            seats[i] = new List<int>();
        // Load initial state
        var rows = input.Split('\n');
        for (var rowIndex = 0; rowIndex < rows.Length; rowIndex++)
        {
            var row = rows[rowIndex];
            var seatList = row.Split(' ');
            for (var seatIndex = 0; seatIndex < columns; seatIndex++)
            {
                seats[seatIndex].Add(int.Parse(seatList[seatIndex].Trim()));
            }
        }
        
        // Simulate
        const int rounds = 10;
        string lastShoutedNumber = "";
        for (int round = 0; round < rounds; round++)
        {
            // Pop off our current clapper
            int currentCol = round % columns;
            int num = seats[currentCol][0];
            seats[currentCol].RemoveAt(0);
            
            // Add our clapper to the next column in their respective position
            currentCol = (currentCol + 1) % columns;
            // Find their index:
            int periodLength = 2 * seats[currentCol].Count + 2;
            int pos = (num - 1) % periodLength;
            if (pos > seats[currentCol].Count)
                pos = seats[currentCol].Count - ((pos - 1) - seats[currentCol].Count);
            seats[currentCol].Insert(pos, num);
            // This is apparently correct, and my brain has no idea how I got it right, but I'm happy.
            // Next time, I need some paper to draw this out, cause my brain hurty

            // Shout the number
            StringBuilder builder = new();
            for (int i = 0; i < columns; i++) 
                builder.Append(seats[i][0]);
            lastShoutedNumber = builder.ToString();
            
            Console.WriteLine($@"Round {round + 1}: {lastShoutedNumber}");
        }
        
        Console.WriteLine($@"Last shouted number: {lastShoutedNumber}");
    }
    
    [Task(2)]
    public static void Task2(string input)
    {
        const int columns = 4;
        List<int>[] seats = new List<int>[columns];
        for (int i = 0; i < columns; i++)
            seats[i] = new List<int>();
        // Load initial state
        var rows = input.Split('\n');
        for (var rowIndex = 0; rowIndex < rows.Length; rowIndex++)
        {
            var row = rows[rowIndex];
            var seatList = row.Split(' ');
            for (var seatIndex = 0; seatIndex < columns; seatIndex++)
            {
                seats[seatIndex].Add(int.Parse(seatList[seatIndex].Trim()));
            }
        }
        
        // Simulate
        Dictionary<long, int> timesShouted = new();
        int round = 0;
        while (true)
        {
            // Pop off our current clapper
            int currentCol = round % columns;
            int num = seats[currentCol][0];
            seats[currentCol].RemoveAt(0);
            
            // Add our clapper to the next column in their respective position
            currentCol = (currentCol + 1) % columns;
            // Find their index:
            // int periodLength = 2 * seats[currentCol].Count;
            // int pos = (num - 1) % periodLength;
            // if (pos > seats[currentCol].Count)
            //     pos = seats[currentCol].Count - ((pos - 1) - seats[currentCol].Count);
            
            int pos = Math.Abs((num % (seats[currentCol].Count * 2)) - 1);
            if (pos > seats[currentCol].Count)
                pos = (seats[currentCol].Count * 2) - pos;
            seats[currentCol].Insert(pos, num);
            
            // Shout the number
            StringBuilder builder = new();
            for (int i = 0; i < columns; i++) 
                builder.Append(seats[i][0]);
            long shoutedNumber = long.Parse(builder.ToString());

            if (round < 100)
            {
                Console.WriteLine($@"Inserting {num} at {pos} in column {currentCol}");
                Console.WriteLine($@"On round {round + 1}, we shouted {shoutedNumber}.");
            }
            
            if (timesShouted.ContainsKey(shoutedNumber))
            {
                timesShouted[shoutedNumber]++;
                if (timesShouted[shoutedNumber] == 2024)
                {
                    Console.WriteLine($@"On round {round + 1}, we shouted {shoutedNumber} for the 2024th time. Our answer is: {(round + 1) * shoutedNumber}");
                    break;
                }
            } else timesShouted.Add(shoutedNumber, 1);
            
            round++;
        }
    }
    
    [Task(3)]
    public static void Task3(string input)
    {
        const int columns = 4;
        List<int>[] seats = new List<int>[columns];
        for (int i = 0; i < columns; i++)
            seats[i] = new List<int>();
        // Load initial state
        var rows = input.Split('\n');
        for (var rowIndex = 0; rowIndex < rows.Length; rowIndex++)
        {
            var row = rows[rowIndex];
            var seatList = row.Split(' ');
            for (var seatIndex = 0; seatIndex < columns; seatIndex++)
            {
                seats[seatIndex].Add(int.Parse(seatList[seatIndex].Trim()));
            }
        }
        
        // Simulate
        HashSet<string> seenStates = new();
        int round = 0;
        long max = 0;
        while (true)
        {
            // Pop off our current clapper
            int currentCol = round % columns;
            int num = seats[currentCol][0];
            seats[currentCol].RemoveAt(0);
            
            // Add our clapper to the next column in their respective position
            currentCol = (currentCol + 1) % columns;
            int pos = Math.Abs((num % (seats[currentCol].Count * 2)) - 1);
            if (pos > seats[currentCol].Count)
                pos = (seats[currentCol].Count * 2) - pos;
            seats[currentCol].Insert(pos, num);
            
            StringBuilder stateBuilder = new();
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < seats[i].Count; j++)
                    stateBuilder.Append(seats[i][j]);
                stateBuilder.Append('|');
            }
            string state = stateBuilder.ToString();
            if (seenStates.Contains(state))
            {
                Console.WriteLine($@"We've found a previous state. Therefore, the maximum shouted number is: {max}");
                break;
            } 
            seenStates.Add(state);
            
            // Shout the number
            StringBuilder builder = new();
            for (int i = 0; i < columns; i++) 
                builder.Append(seats[i][0]);
            long shoutedNumber = long.Parse(builder.ToString());
            max = Math.Max(max, shoutedNumber);

            round++;
        }
    }
}
