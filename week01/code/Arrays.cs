public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN (step-by-step):
        // 1) Create a double array with exactly 'length' slots.
        // 2) For each index i from 0 to length-1:
        //      - the (i+1)th multiple of 'number' is number * (i+1)
        //      - store that value into the array at position i
        // 3) Return the filled array.

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN (step-by-step) using list slicing (GetRange) and rebuilding the same list:
        // 1) If amount == data.Count, rotating doesn't change anything, so we can return.
        // 2) Figure out where the "cut" happens:
        //      - the last 'amount' items will move to the front
        //      - splitIndex = data.Count - amount
        // 3) Make two slices:
        //      - tail = data.GetRange(splitIndex, amount)  (these go to the front)
        //      - head = data.GetRange(0, splitIndex)      (these go after)
        // 4) Clear the original list.
        // 5) Add tail, then add head back into the original list (so we modify it in-place).

        int n = data.Count;

        if (amount == n)
            return;

        int splitIndex = n - amount;

        List<int> tail = data.GetRange(splitIndex, amount);
        List<int> head = data.GetRange(0, splitIndex);

        data.Clear();
        data.AddRange(tail);
        data.AddRange(head);
    }
}
