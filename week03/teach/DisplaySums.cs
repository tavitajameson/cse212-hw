using System;
using System.Collections.Generic;

public class DisplaySums
{
    // Finds and displays all pairs that sum to 10 in O(n) time using a set.
    // Assumption from assignment: numbers contains no duplicates.
    public static void DisplaySumPairs(List<int> numbers)
    {
        var seen = new HashSet<int>();

        foreach (int x in numbers)
        {
            int need = 10 - x;

            if (seen.Contains(need))
            {
                // Print the pair once (e.g., "3 + 7 = 10")
                Console.WriteLine($"{need} + {x} = 10");
            }

            seen.Add(x);
        }
    }

    // If your project expects a Run() entry, include this:
    public static void Run()
    {
        var numbers = new List<int> { 1, 9, 3, 7, 5, 2, 8, 4, 6 };
        DisplaySumPairs(numbers);
    }
}
