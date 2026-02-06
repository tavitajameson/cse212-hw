using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Base case
        if (n <= 0)
            return 0;

        // Recursive case: n^2 + sum of squares up to (n-1)
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    ///
    /// In mathematics, we can calculate the number of permutations
    /// using the formula: len(letters)! / (len(letters) - size)!
    ///
    /// For example, if letters was [A,B,C] and size was 2 then
    /// the following would the contents of the results array after the function ran: AB, AC, BA, BC, CA, CB (might be in 
    /// a different order).
    ///
    /// You can assume that the size specified is always valid (between 1 
    /// and the length of the letters list).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Base case: built a word of desired length
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Choose each letter, remove it from the remaining pool, recurse
        for (int i = 0; i < letters.Length; i++)
        {
            char chosen = letters[i];
            string remaining = letters.Remove(i, 1);
            PermutationsChoose(results, remaining, size, word + chosen);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Count ways to climb using 1,2,3 steps, with memoization.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Base Cases
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // Initialize memo dictionary if needed
        remember ??= new Dictionary<int, decimal>();

        // If already computed, return it
        if (remember.TryGetValue(s, out decimal cached))
            return cached;

        // Compute recursively (but pass memo!)
        decimal ways =
            CountWaysToClimb(s - 1, remember) +
            CountWaysToClimb(s - 2, remember) +
            CountWaysToClimb(s - 3, remember);

        // Store and return
        remember[s] = ways;
        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// Expand wildcard pattern (*) into all binary strings.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // Find first wildcard
        int idx = pattern.IndexOf('*');

        // Base case: no wildcards left
        if (idx == -1)
        {
            results.Add(pattern);
            return;
        }

        // Replace that wildcard with 0 and 1, recurse
        string prefix = pattern[..idx];
        string suffix = pattern[(idx + 1)..];

        WildcardBinary(prefix + "0" + suffix, results);
        WildcardBinary(prefix + "1" + suffix, results);
    }

    /// <summary>
    /// #############
    /// # Problem 5 #
    /// #############
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // Initialize path on first call
        if (currPath == null)
            currPath = new List<ValueTuple<int, int>>();

        // Add current position to path
        currPath.Add((x, y));

        // If we're at the end, store the path and backtrack
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        // Explore neighbors in a consistent order:
        // (Right, Down, Left, Up) — order doesn't matter because tests sort results,
        // but being consistent avoids surprises.
        var moves = new (int dx, int dy)[]
        {
            (1, 0),
            (0, 1),
            (-1, 0),
            (0, -1)
        };

        foreach (var (dx, dy) in moves)
        {
            int nx = x + dx;
            int ny = y + dy;

            if (maze.IsValidMove(currPath, nx, ny))
            {
                SolveMaze(results, maze, nx, ny, currPath);
            }
        }

        // Backtrack: remove current position before returning to caller
        currPath.RemoveAt(currPath.Count - 1);
    }
}
